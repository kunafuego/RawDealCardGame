using RawDealView;
using RawDealView.Options;
namespace RawDeal;

public class CardPlayer
{
    private readonly View _view;
    private readonly Player _playerPlayingRound;
    private readonly Player _playerNotPlayingRound;
    private EffectForNextMove _nextMoveEffect;
    private bool _turnEnded;
    private bool _gameShouldEnd;

    public CardPlayer(View view, Player playerPlayingRound, Player playerNotPlayingRound, EffectForNextMove nextMoveEffect)
    {
        _view = view;
        _playerPlayingRound = playerPlayingRound;
        _playerNotPlayingRound = playerNotPlayingRound;
        _nextMoveEffect = nextMoveEffect;
    }

    public EffectForNextMove NextMoveEffect
    {
        get { return _nextMoveEffect; }
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
        if (chosenPlay != -1)
        {
            try
            {
                TryToPlayCard(playsToShow[chosenPlay]);
            }
            catch (CardWasReversedException error)
            {
                if (error.Message == "Jockeying for Position")
                {
                    SelectedEffect chosenOption = _view.AskUserToSelectAnEffectForJockeyForPosition(_playerNotPlayingRound.GetSuperstarName());
                    _nextMoveEffect = (chosenOption == SelectedEffect.NextGrappleIsPlus4D)
                        ? new EffectForNextMove(4, 0)
                        : new EffectForNextMove(0, 8);
                }
                else
                {
                    _nextMoveEffect = new EffectForNextMove(0, 0);
                }
                _turnEnded = true;
            }
        }
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
        if (chosenPlay.PlayedAs == "MANEUVER")
        {
            PlayManeuver(cardPlayed);
        }
        else if (chosenPlay.PlayedAs == "ACTION")
        {
            PlayAction(cardPlayed);
        }
    }

    private void TryToReversePlay(Play playOpponentIsTryingToMake)
    {
        List<Card> reversalCardsThatPlayerCanPlay = _playerNotPlayingRound.GetReversalCardsThatPlayerCanPlay(_nextMoveEffect, playOpponentIsTryingToMake.Card);
        List<Card> reversalCardsThatPlayerCanPlayOnThisCard = GetReversalCardsThatPlayerCanPlayOnThisCard(reversalCardsThatPlayerCanPlay, playOpponentIsTryingToMake);
        if (reversalCardsThatPlayerCanPlayOnThisCard.Any())
        {
            List<Play> reversalPlays = GetPlaysOfAvailablesCards(reversalCardsThatPlayerCanPlayOnThisCard);
            List<string> reversalPlaysString = reversalPlays.Select(play => play.ToString()).ToList();
            int usersChoice = _view.AskUserToSelectAReversal(_playerNotPlayingRound.GetSuperstarName(), reversalPlaysString);
            if (usersChoice != -1)
            {
                Play reversalSelected = reversalPlays[usersChoice];
                Card reversalCardSelected = reversalSelected.Card;
                playOpponentIsTryingToMake.PlayedAs = "Reversed From Hand";
                _view.SayThatPlayerReversedTheCard(_playerNotPlayingRound.GetSuperstarName(), reversalSelected.ToString());
                _playerPlayingRound.MoveCardFromHandToRingside(playOpponentIsTryingToMake.Card);
                SetDamageThatReversalShouldMake(reversalCardSelected, playOpponentIsTryingToMake.Card);
                ReversalManager reversalManager = new ReversalManager(_view);
                reversalManager.PerformEffect(playOpponentIsTryingToMake, reversalSelected.Card, _playerNotPlayingRound, _playerPlayingRound);
                throw new CardWasReversedException(reversalCardSelected.Title);
            }
        }
    }
    private List<Card> GetReversalCardsThatPlayerCanPlayOnThisCard(List<Card> reversalCardsThatPlayerCanPlay, Play playOpponentIsTryingToMake)
    {
        Card cardOpponentIsTryingToMake = playOpponentIsTryingToMake.Card;
        ReversalManager reversalManager = new ReversalManager(_view);
        List<Card> reversalCardsThatPlayerCanPlayOnThisCard = reversalCardsThatPlayerCanPlay
            .Where(cardThatCanPossibleReverse => reversalManager.CheckIfCanReverseThisPlay(cardThatCanPossibleReverse, playOpponentIsTryingToMake, "Hand", cardOpponentIsTryingToMake.GetDamage() + _nextMoveEffect.DamageChange)).ToList();
        return reversalCardsThatPlayerCanPlayOnThisCard;
    }

    private List<Play> GetPlaysOfAvailablesCards(List<Card> reversalCardThatPlayerCanPlay)
    {
        List<Play> playsToReturn = new();
        foreach (Card card in reversalCardThatPlayerCanPlay)
        {
            Play play = new Play(card, "REVERSAL");
            playsToReturn.Add(play);
        }

        return playsToReturn;
    }

    private void SetDamageThatReversalShouldMake(Card reversalCardSelected, Card cardOpponentWasTryingToPlay)
    {
        if (reversalCardSelected.Title == "Rolling Takedown" || reversalCardSelected.Title == "Knee to the Gut")
        {
            cardOpponentWasTryingToPlay.ReversalDamage = cardOpponentWasTryingToPlay.GetDamage() + _nextMoveEffect.DamageChange;
        }
    }

    private void PlayManeuver(Card cardPlayed)
    {
        ManeuverPlayer maneuverPlayer = new ManeuverPlayer(_view, _playerPlayingRound, _playerNotPlayingRound, _nextMoveEffect);
        maneuverPlayer.PlayManeuver(cardPlayed);
        _nextMoveEffect = new EffectForNextMove(0, 0);
        _turnEnded = maneuverPlayer.TurnEnded;
        _gameShouldEnd = maneuverPlayer.GameShouldEnd;
    }

    private void PlayAction(Card cardPlayed)
    {
        if (cardPlayed.Title == "Jockeying for Position")
        {
            SelectedEffect chosenOption = _view.AskUserToSelectAnEffectForJockeyForPosition(_playerPlayingRound.GetSuperstarName());
            _nextMoveEffect = (chosenOption == SelectedEffect.NextGrappleIsPlus4D)
                ? new EffectForNextMove(4, 0)
                : new EffectForNextMove(0, 8);
            _playerPlayingRound.MoveCardFromHandToRingArea(cardPlayed);
        }
        else
        {
            _playerPlayingRound.DrawSingleCard();
            _view.SayThatPlayerMustDiscardThisCard(_playerPlayingRound.GetSuperstarName(), cardPlayed.Title);
            _view.SayThatPlayerDrawCards(_playerPlayingRound.GetSuperstarName(), 1);
            _playerPlayingRound.MoveCardFromHandToRingside(cardPlayed);
        }
    }

}