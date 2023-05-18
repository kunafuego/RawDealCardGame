using RawDealView;
namespace RawDeal;

public class ManeuverPlayer
{
    private readonly View _view;
    private readonly Player _playerPlayingRound;
    private readonly Player _playerNotPlayingRound;
    private bool _gameShouldEnd;
    private bool _turnEnded;
    private readonly EffectForNextMove _effectForThisMove;
    public ManeuverPlayer(View view, Player playerPlayingRound, Player playerNotPlayingRound, EffectForNextMove effectForThisMove)
    {
        _view = view;
        _playerPlayingRound = playerPlayingRound;
        _playerNotPlayingRound = playerNotPlayingRound;
        _gameShouldEnd = false;
        _turnEnded = false;
        _effectForThisMove = effectForThisMove;
    }

    public bool GameShouldEnd
    {
        get { return _gameShouldEnd; }
    }
    
    public bool TurnEnded
    {
        get { return _turnEnded; }
    }
    
    public void PlayManeuver(Card cardPlayed)
    {
        int cardTotalDamage = ManageCardDamage(cardPlayed);
        if (cardTotalDamage > 0)
        {
            _view.SayThatOpponentWillTakeSomeDamage(_playerNotPlayingRound.GetSuperstarName(), cardTotalDamage);
            CheckIfGameAndTurnShouldEndWhileReceivingDamage();
        }
        for (int i = 1; i <= cardTotalDamage && !_turnEnded; i++)
        {
            SayThatCardWasOverturned(i, cardTotalDamage);
            CheckIfManeuverCanBeReversedFromDeck(i, cardTotalDamage,cardPlayed);
            DealSingleCardDamage(i, cardTotalDamage);
        }
        _playerPlayingRound.MoveCardFromHandToRingArea(cardPlayed);
    }

    public void PlayReversalAsManeuver(Card cardPlayed)
    {
        int cardTotalDamage = ManageCardDamage(cardPlayed);
        if (cardTotalDamage > 0)
        {
            _view.SayThatOpponentWillTakeSomeDamage(_playerNotPlayingRound.GetSuperstarName(), cardTotalDamage);
            CheckIfGameAndTurnShouldEndWhileReceivingDamage();
        }
        for (int i = 1; i <= cardTotalDamage && !_turnEnded; i++)
        {
            SayThatCardWasOverturned(i, cardTotalDamage);
            DealSingleCardDamage(i, cardTotalDamage);
        }
        _playerPlayingRound.MoveCardFromHandToRingArea(cardPlayed);
    }
    
    private int ManageCardDamage(Card cardPlayed)
    {
        int initialDamage = cardPlayed.GetDamage();
        if (AbilitiesManager.CheckIfHasAbilityWhenReceivingDamage(_playerNotPlayingRound))
        {
            initialDamage -= 1;
        }
        int extraDamage = (cardPlayed.CheckIfSubtypesContain("Grapple")) ? _effectForThisMove.DamageChange : 0;
        return initialDamage + extraDamage;
    }
    
    private void CheckIfGameAndTurnShouldEndWhileReceivingDamage()
    {
        _gameShouldEnd = !_playerNotPlayingRound.HasCardsInArsenal();
        _turnEnded = _gameShouldEnd;
    }
    
    private void SayThatCardWasOverturned(int amountOfDamageReceivedAtMoment, int totalCardDamage)
    {
        Card cardThatWillGoToRingside = _playerNotPlayingRound.GetCardOnTopOfArsenal();
        _view.ShowCardOverturnByTakingDamage(cardThatWillGoToRingside.ToString(), amountOfDamageReceivedAtMoment, totalCardDamage);
    }
    
    private void CheckIfManeuverCanBeReversedFromDeck(int amountOfDamageReceivedAtMoment, int totalCardDamage, Card cardPlayed)
    {
        Card cardThatWasTurnedOver = _playerNotPlayingRound.GetCardOnTopOfArsenal();
        bool cardCanReverseReceivingDamage = CheckIfCardCanReverseManeuver(cardThatWasTurnedOver, new Play(cardPlayed, "MANEUVER"));
        if (cardCanReverseReceivingDamage)
        {
            Play playThatIsBeingReversed = new Play(cardPlayed, "Reversed From Deck");
            _playerPlayingRound.MoveCardFromHandToRingArea(cardPlayed);
            ReversalManager reversalManager = new ReversalManager(_view);
            reversalManager.PerformEffect(playThatIsBeingReversed, cardThatWasTurnedOver, _playerNotPlayingRound, _playerPlayingRound);
            _view.SayThatCardWasReversedByDeck(_playerNotPlayingRound.GetSuperstarName());
            if (amountOfDamageReceivedAtMoment < totalCardDamage) PlayerDrawCardsStunValueEffect(cardPlayed);
            throw new CardWasReversedException(cardThatWasTurnedOver.Title);
        }
    }

    private bool CheckIfCardCanReverseManeuver(Card cardThatWasTurnedOver, Play playPlayedByOpponent)
    {
        bool cardIsReversal = CheckIfCardIsReversal(cardThatWasTurnedOver);
        bool playerHasHigherFortitudeThanCard = CheckIfPlayerHasHigherFortitudeThanCard(cardThatWasTurnedOver, playPlayedByOpponent.Card);
        if (cardIsReversal && playerHasHigherFortitudeThanCard)
        {
            ReversalManager reversalManager = new ReversalManager(_view);
            return reversalManager.CheckIfCanReverseThisPlay(cardThatWasTurnedOver, playPlayedByOpponent, "Deck", ManageCardDamage(playPlayedByOpponent.Card));
        }
        return false;
    }
    
    private bool CheckIfCardIsReversal(Card card)
    {
        List<string> cardTypes = card.Types;
        return cardTypes.Contains("Reversal");
    }

    private bool CheckIfPlayerHasHigherFortitudeThanCard(Card cardThatCouldReverseManeuver, Card cardThatCanBeReversed)
    {
        int extraFortitude = (cardThatCanBeReversed.CheckIfSubtypesContain("Grapple")) ? _effectForThisMove.FortitudeChange : 0;
        return _playerNotPlayingRound.CheckIfHasHigherFortitudeThanGiven(cardThatCouldReverseManeuver.Fortitude + extraFortitude);
    }
    
    private void PlayerDrawCardsStunValueEffect(Card card)
    {
        int amountOfCardsToDraw = 0;
        if (card.StunValue > 0)
        { 
            amountOfCardsToDraw =
                _view.AskHowManyCardsToDrawBecauseOfStunValue(_playerPlayingRound.GetSuperstarName(), card.StunValue);
        }
        if (amountOfCardsToDraw > 0) _view.SayThatPlayerDrawCards(_playerPlayingRound.GetSuperstarName(), amountOfCardsToDraw);
        for (int i = 0; i < amountOfCardsToDraw; i++)
        {
            _playerPlayingRound.DrawSingleCard();
        }
    }
    
    private void DealSingleCardDamage(int cardIndex, int cardDamage)
    {
        _playerNotPlayingRound.MoveArsenalTopCardToRingside();
        if (cardIndex < cardDamage)
        {
            CheckIfGameAndTurnShouldEndWhileReceivingDamage();
        }
    }
}