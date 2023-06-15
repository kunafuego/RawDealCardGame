using RawDeal.Bonus;
using RawDealView;

namespace RawDeal;

public class CardPlayer
{
    private readonly Player _playerNotPlayingRound;
    private readonly Player _playerPlayingRound;
    private readonly View _view;
    private readonly BonusManager _bonusManager;
    private readonly LastPlay _lastPlayInstance;

    public CardPlayer(View view, Player playerPlayingRound, Player playerNotPlayingRound,
        LastPlay lastPlayInstance, BonusManager bonusManager)
    {
        _view = view;
        _playerPlayingRound = playerPlayingRound;
        _playerNotPlayingRound = playerNotPlayingRound;
        _lastPlayInstance = lastPlayInstance;
        _bonusManager = bonusManager;
    }


    public bool TurnEnded { get; private set; }

    public bool GameShouldEnd { get; private set; }

    public void ManagePlayingCards()
    {
        var playsToShow = _playerPlayingRound.GetAvailablePlays();
        var availablePlaysInStringFormat = GetStringsOfPlays(playsToShow);
        var chosenPlay = _view.AskUserToSelectAPlay(availablePlaysInStringFormat);
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
            TurnEnded = true;
            GameShouldEnd = true;
        }
    }

    private void ManageLastPlay(Play playThatWasJustPlayed)
    {
        var lastPlay = _lastPlayInstance.LastPlayPlayed;
        _lastPlayInstance.WasThisLastPlayAManeuverPlayedAfterIrishWhip = false;
        if (lastPlay != null)
        {
            var lastCard = lastPlay.Card;
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

        TurnEnded = true;
    }

    private List<string> GetStringsOfPlays(List<Play> availablePlays)
    {
        var playsToShow = new List<string>();
        foreach (var playObject in availablePlays) playsToShow.Add(playObject.ToString());
        return playsToShow;
    }

    private void TryToPlayCard(Play chosenPlay)
    {
        var cardPlayed = chosenPlay.Card;
        _view.SayThatPlayerIsTryingToPlayThisCard(_playerPlayingRound.GetSuperstarName(),
            chosenPlay.ToString());
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
        var reversalPerformer = new ReversalManager(_view, _playerPlayingRound,
            _playerNotPlayingRound, _bonusManager, _lastPlayInstance);
        reversalPerformer.TryToReversePlayFromHand(playOpponentIsTryingToMake);
    }

    private void ManageCardEffect(Play chosenPlay)
    {
        var cardToPlay = chosenPlay.Card;
        var cardEffectPrecondition = cardToPlay.Precondition;
        var meetsPrecondition =
            cardEffectPrecondition.DoesMeetPrecondition(_playerPlayingRound,
                "Checking To Play Action");
        if (meetsPrecondition) PerformCardEffect(chosenPlay);
    }

    private void PerformCardEffect(Play playSuccesfulyPlayed)
    {
        var cardPlayed = playSuccesfulyPlayed.Card;
        var cardEffects = cardPlayed.EffectObject;
        foreach (var effect in cardEffects)
            effect.Apply(playSuccesfulyPlayed, _playerPlayingRound, _playerNotPlayingRound);
    }

    private void PlayManeuver(Card cardPlayed)
    {
        var maneuverPlayer = new ManeuverPlayer(_view, _playerPlayingRound,
            _playerNotPlayingRound, _lastPlayInstance, _bonusManager);
        maneuverPlayer.PlayManeuver(cardPlayed);
        TurnEnded = maneuverPlayer.TurnEnded;
        GameShouldEnd = maneuverPlayer.GameShouldEnd;
    }

    private void MoveActionCard(Card cardPlayed)
    {
        var cardEffects = cardPlayed.EffectObject;
        var alreadyMoveCard = cardEffects.Any(obj => obj is DiscardCardToDrawOne);
        if (cardPlayed.Title == "Jockeying for Position" || !alreadyMoveCard)
            _playerPlayingRound.MoveCardFromHandToRingArea(cardPlayed);
        if (cardPlayed.Title != "Jockeying for Position" && cardPlayed.Title != "Irish Whip")
        {
            _bonusManager.CheckIfFortitudeBonusExpire();
            _bonusManager.CheckIfBonusExpire(ExpireOptions.OneMoreCardWasPlayed);
        }

        _lastPlayInstance.ActualDamageMade = 0;
    }
}