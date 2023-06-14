using RawDeal.Bonus;
using RawDeal.Bonus.BonusClasses;
using RawDeal.Effects;
using RawDeal.Preconditions;
using RawDealView;
using RawDealView.Options;
namespace RawDeal;

public class CardPlayer
{
    private readonly View _view;
    private readonly Player _playerPlayingRound;
    private readonly Player _playerNotPlayingRound;
    private BonusManager _bonusManager;
    private LastPlay _lastPlayInstance;
    private bool _turnEnded;
    private bool _gameShouldEnd;

    public CardPlayer(View view, Player playerPlayingRound, Player playerNotPlayingRound, LastPlay lastPlayInstance, BonusManager bonusManager)
    {
        _view = view;
        _playerPlayingRound = playerPlayingRound;
        _playerNotPlayingRound = playerNotPlayingRound;
        _lastPlayInstance = lastPlayInstance;
        _bonusManager = bonusManager;
    }



    public bool TurnEnded
    {
        get { return _turnEnded; }
    }
    
    public bool GameShouldEnd
    {
        get { return _gameShouldEnd; }
    }
    public void ManagePlayingCards()
    {
        List<Play> playsToShow = _playerPlayingRound.GetAvailablePlays();
        List<string> availablePlaysInStringFormat = GetStringsOfPlays(playsToShow);
        int chosenPlay = _view.AskUserToSelectAPlay(availablePlaysInStringFormat);
        if (chosenPlay == -1) return;
        try
        {
            TryToPlayCard(playsToShow[chosenPlay]);
            ManageLastPlay(playsToShow[chosenPlay]);
        }
        catch (CardWasReversedException error)
        {
            ManageReversalError(error);
            _lastPlayInstance.WasItASuccesfulReversal = true;

        }
        catch (GameEndedBecauseOfCollateralDamage error)
        {
            _turnEnded = true;
            _gameShouldEnd = true;
        }
    }

    private void ManageLastPlay(Play playThatWasJustPlayed)
    {
        Play lastPlay = _lastPlayInstance.LastPlayPlayed;
        _lastPlayInstance.WasThisLastPlayAManeuverPlayedAfterIrishWhip = false;
        if (lastPlay != null)
        {
            Card lastCard = lastPlay.Card;
            _lastPlayInstance.WasThisLastPlayAManeuverPlayedAfterIrishWhip =
                playThatWasJustPlayed.PlayedAs == "MANEUVER" && lastCard.Title == "Irish Whip";
        }
        _lastPlayInstance.LastPlayPlayed = playThatWasJustPlayed;
        _lastPlayInstance.WasItPlayedOnSameTurnThanActualPlay = true;
        _lastPlayInstance.WasItASuccesfulReversal = false;
    }

    private void ManageReversalError(CardWasReversedException error)
    {
        if (error.Message != "Jockeying for Position" && error.Message != "Irish Whip")
        {
            _bonusManager.CheckIfFortitudeBonusExpire();
            _bonusManager.CheckIfBonusExpire(ExpireOptions.EndOfTurn);
        }
        _turnEnded = true;
    }

    private List<string> GetStringsOfPlays(List<Play> availablePlays)
    {
        List<string> playsToShow = new List<string>();
        foreach (Play playObject in availablePlays)
        {
            playsToShow.Add(playObject.ToString());
        }
        return playsToShow;
    }

    private void TryToPlayCard(Play chosenPlay)
    {
        Card cardPlayed = chosenPlay.Card;
        _view.SayThatPlayerIsTryingToPlayThisCard(_playerPlayingRound.GetSuperstarName(), chosenPlay.ToString());
        TryToReversePlay(chosenPlay);
        _view.SayThatPlayerSuccessfullyPlayedACard();
        ManageCardEffect(chosenPlay);
        switch (chosenPlay.PlayedAs)
        {
            case "MANEUVER":
                PlayManeuver(cardPlayed);
                break;
            case "ACTION":
                MoveActionCard(cardPlayed);
                break;
        }
    }

    private void TryToReversePlay(Play playOpponentIsTryingToMake)
    {
        ReversalManager reversalPerformer = new ReversalManager(_view, _playerPlayingRound, _playerNotPlayingRound, _bonusManager, _lastPlayInstance);
        reversalPerformer.TryToReversePlayFromHand(playOpponentIsTryingToMake);
    }

    private void ManageCardEffect(Play chosenPlay)
    {
        Card cardToPlay = chosenPlay.Card;
        Precondition cardEffectPrecondition = cardToPlay.Precondition;
        bool meetsPrecondition = cardEffectPrecondition.DoesMeetPrecondition(_playerPlayingRound, "Checking To Play Action");
        if (meetsPrecondition)
        {
            PerformCardEffect(chosenPlay);
        }
    }

    private void PerformCardEffect(Play playSuccesfulyPlayed)
    {
        Card cardPlayed = playSuccesfulyPlayed.Card;
        List<Effect> cardEffects = cardPlayed.EffectObject;
        foreach (Effect effect in cardEffects)
        {
            effect.Apply(playSuccesfulyPlayed, _view, _playerPlayingRound, _playerNotPlayingRound);
        }
    }

    private void PlayManeuver(Card cardPlayed)
    {
        ManeuverPlayer maneuverPlayer = new ManeuverPlayer(_view, _playerPlayingRound, 
            _playerNotPlayingRound, _lastPlayInstance, _bonusManager);
        maneuverPlayer.PlayManeuver(cardPlayed);
        _turnEnded = maneuverPlayer.TurnEnded;
        _gameShouldEnd = maneuverPlayer.GameShouldEnd;
    }

    private void MoveActionCard(Card cardPlayed)
    {
        List<Effect> cardEffects = cardPlayed.EffectObject;
        bool alreadyMoveCard = cardEffects.Any(obj => obj is DiscardCardToDrawOne);
        if (cardPlayed.Title == "Jockeying for Position" || !alreadyMoveCard)
        {
            _playerPlayingRound.MoveCardFromHandToRingArea(cardPlayed);
        }
        if (cardPlayed.Title != "Jockeying for Position" && cardPlayed.Title != "Irish Whip")
        {
            _bonusManager.CheckIfFortitudeBonusExpire();
            _bonusManager.CheckIfBonusExpire(ExpireOptions.OneMoreCardWasPlayed);
        }

        _lastPlayInstance.ActualDamageMade = 0;
    }

}