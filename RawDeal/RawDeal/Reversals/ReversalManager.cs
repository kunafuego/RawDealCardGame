using RawDeal.Preconditions;
using RawDeal.ReversalCards;
using RawDeal.Reversals;
using RawDealView;

namespace RawDeal;

public class ReversalManager
{
    private View _view;
    private Dictionary<string, ReversalCard> _effects;
    private Player _playerPlayingRound;
    private Player _playerNotPlayingRound;
    private EffectForNextMove _nextMoveEffect;

    public ReversalManager(View view, Player playerPlayingRound, Player playerNotPlayingRound, EffectForNextMove nextMoveEffect)
    {
        _view = view;
        _effects = new Dictionary<string, ReversalCard>
        {
            { "Rolling Takedown", new RollingTakedown(view) },
            { "Knee to the Gut", new KneeToTheGut(view) },
            { "Elbow to the Face", new ElbowToTheFace(view) },
            { "Manager Interferes", new ManagerInterferes(view) },
            { "Chyna Interferes", new ChynaInterferes(view) },
            { "Clean Break", new CleanBreak(view) },
            { "Jockeying for Position", new JockeyingForPosition(view) },
            { "Step Aside", new StepAside(view)},
            { "Escape Move", new EscapeMove(view)},
            { "Break the Hold", new BreakTheHold(view)},
            { "No Chance in Hell", new NoChanceInHell(view)}
        };
        _playerPlayingRound = playerPlayingRound;
        _playerNotPlayingRound = playerNotPlayingRound;
        _nextMoveEffect = nextMoveEffect;
    }
    

    private void PerformEffect(Play playThatIsBeingReversed, Card cardThatIsReversingManeuver)
    {
        _effects[cardThatIsReversingManeuver.Title].PerformEffect(playThatIsBeingReversed, cardThatIsReversingManeuver, _playerNotPlayingRound, _playerPlayingRound);
    }

    private bool CheckIfCardCanReverseThisPlay(Card cardThatCanPossibleReverse, Play playIsBeingMade, string askedFromDeckOrHand, int netDamageThatWillReceive)
    {
        Precondition cardPrecondition = cardThatCanPossibleReverse.Precondition;
        return cardPrecondition.DoesMeetPrecondition(playIsBeingMade, askedFromDeckOrHand, netDamageThatWillReceive);
    }
    
    public void TryToReversePlayFromHand(Play playOpponentIsTryingToMake)
    {
        List<Card> reversalCardsThatPlayerCanPlay = _playerNotPlayingRound.GetReversalCardsThatPlayerCanPlay(_nextMoveEffect, playOpponentIsTryingToMake.Card);
        List<Card> reversalCardsThatPlayerCanPlayOnThisCard = GetReversalCardsThatPlayerCanPlayOnThisCard(reversalCardsThatPlayerCanPlay, playOpponentIsTryingToMake);
        if (reversalCardsThatPlayerCanPlayOnThisCard.Any())
        {
            List<Play> reversalPlays = ReversalUtils.GetPlaysOfAvailablesCards(reversalCardsThatPlayerCanPlayOnThisCard);
            int choiceSelected = AskUserToSelectReversal(reversalPlays);
            if (choiceSelected != -1)
            {
                ReversePlayFromHand(playOpponentIsTryingToMake, reversalPlays[choiceSelected]);
            }
        }
    }
    private List<Card> GetReversalCardsThatPlayerCanPlayOnThisCard(List<Card> reversalCardsThatPlayerCanPlay, Play playOpponentIsTryingToMake)
    {
        Card cardOpponentIsTryingToMake = playOpponentIsTryingToMake.Card;
        List<Card> reversalCardsThatPlayerCanPlayOnThisCard = reversalCardsThatPlayerCanPlay
            .Where(cardThatCanPossibleReverse => CheckIfCardCanReverseThisPlay(cardThatCanPossibleReverse, playOpponentIsTryingToMake, "Hand", cardOpponentIsTryingToMake.GetDamage() + _nextMoveEffect.DamageChange)).ToList();
        return reversalCardsThatPlayerCanPlayOnThisCard;
    }

    private int AskUserToSelectReversal(List<Play> reversalPlays)
    {
        List<string> reversalPlaysString = reversalPlays.Select(play => play.ToString()).ToList();
        int usersChoice = _view.AskUserToSelectAReversal(_playerNotPlayingRound.GetSuperstarName(), reversalPlaysString);
        return usersChoice;
    }

    private void ReversePlayFromHand(Play opponentPlay, Play reversalSelected)
    {
        Card reversalCardSelected = reversalSelected.Card;
        opponentPlay.PlayedAs = "Reversed From Hand";
        opponentPlay.CardThatWasReversedBy = reversalCardSelected;
        _view.SayThatPlayerReversedTheCard(_playerNotPlayingRound.GetSuperstarName(), reversalSelected.ToString());
        _playerPlayingRound.MoveCardFromHandToRingside(opponentPlay.Card);
        ReversalUtils.SetDamageThatReversalShouldMake(reversalCardSelected, opponentPlay.Card, _nextMoveEffect);
        PerformEffect(opponentPlay, reversalCardSelected);
        throw new CardWasReversedException(reversalCardSelected.Title);
    }

    public void CheckIfManeuverCanBeReversedFromDeckWithASpecificCard(int amountOfDamageReceivedAtMoment, int totalCardDamage, Card cardPlayed)
    {
        Card cardThatWasTurnedOver = _playerNotPlayingRound.GetCardOnTopOfArsenal();
        bool cardCanReverseReceivingDamage = CheckIfCardThatWasTurnedOverCanReverse(cardThatWasTurnedOver, new Play(cardPlayed, "MANEUVER"));
        if (cardCanReverseReceivingDamage)
        {
            ReversePlayFromDeck(amountOfDamageReceivedAtMoment, totalCardDamage, cardPlayed, cardThatWasTurnedOver);
        }
    }

    private void ReversePlayFromDeck(int amountOfDamageReceivedAtMoment, int totalCardDamage, Card cardPlayed,
        Card cardThatWasTurnedOver)
    {
        Play playThatIsBeingReversed = new Play(cardPlayed, "Reversed From Deck");
        playThatIsBeingReversed.CardThatWasReversedBy = cardThatWasTurnedOver;
        _playerPlayingRound.MoveCardFromHandToRingArea(cardPlayed);
        PerformEffect(playThatIsBeingReversed, cardThatWasTurnedOver);
        _view.SayThatCardWasReversedByDeck(_playerNotPlayingRound.GetSuperstarName());
        if (amountOfDamageReceivedAtMoment < totalCardDamage)
            ReversalUtils.PlayerDrawCardsStunValueEffect(cardPlayed, _view, _playerPlayingRound);
        throw new CardWasReversedException(cardThatWasTurnedOver.Title);
    }

    private bool CheckIfCardThatWasTurnedOverCanReverse(Card cardThatWasTurnedOver, Play playPlayedByOpponent)
    {
        bool cardIsReversal = ReversalUtils.CheckIfCardIsReversal(cardThatWasTurnedOver);
        bool playerHasHigherFortitudeThanCard = CheckIfPlayerHasHigherFortitudeThanCard(cardThatWasTurnedOver, playPlayedByOpponent.Card);
        if (cardIsReversal && playerHasHigherFortitudeThanCard)
        {
            return CheckIfCardCanReverseThisPlay(cardThatWasTurnedOver, playPlayedByOpponent, "Deck", ReversalUtils.ManageCardDamage(playPlayedByOpponent.Card, _playerNotPlayingRound, _nextMoveEffect));
        }
        return false;
    }

    private bool CheckIfPlayerHasHigherFortitudeThanCard(Card cardThatCouldReverseManeuver, Card cardThatCanBeReversed)
    {
        int extraFortitude = (cardThatCanBeReversed.CheckIfSubtypesContain("Grapple")) ? _nextMoveEffect.FortitudeChange : 0;
        return _playerNotPlayingRound.CheckIfHasHigherFortitudeThanGiven(cardThatCouldReverseManeuver.Fortitude + extraFortitude);
    }
}