using RawDeal.Bonus;
using RawDeal.Effects;
using RawDeal.Preconditions;
using RawDeal.ReversalCards;
using RawDeal.Reversals;
using RawDealView;

namespace RawDeal;

public class ReversalManager
{
    private View _view;
    private Player _playerPlayingRound;
    private Player _playerNotPlayingRound;
    private BonusManager _bonusManager;
    private LastPlay _lastPlayInstance;

    public ReversalManager(View view, Player playerPlayingRound, Player playerNotPlayingRound, BonusManager bonusManager, LastPlay lastPlayInstance)
    {
        _view = view;
        _playerPlayingRound = playerPlayingRound;
        _playerNotPlayingRound = playerNotPlayingRound;
        _lastPlayInstance = lastPlayInstance;
        _bonusManager = bonusManager;
    }
    

    private void PerformEffect(Play reversalPlay)
    {
        Card cardThatIsReversingTurn = reversalPlay.Card;
        List<Effect> cardEffects = cardThatIsReversingTurn.EffectObject;
        foreach (Effect effect in cardEffects)
        {
            effect.Apply(reversalPlay, _playerNotPlayingRound, _playerPlayingRound);
        }
        MoveCardUsedForReversingTurn(reversalPlay);
    }

    private void MoveCardUsedForReversingTurn(Play reversalPlay)
    {
        if (reversalPlay.PlayedAs == "REVERSAL HAND")
        {
            _playerNotPlayingRound.MoveCardFromHandToRingArea(reversalPlay.Card);
        }
        else if (reversalPlay.PlayedAs == "REVERSAL DECK")
        {
            _playerNotPlayingRound.MoveArsenalTopCardToRingside();
        }
    }

    private bool CheckIfCardMeetsPrecondition(Card cardThatCanPossibleReverse, string askedFromDeckOrHand)
    {
        Precondition cardPrecondition = cardThatCanPossibleReverse.Precondition;
        return cardPrecondition.DoesMeetPrecondition(_playerNotPlayingRound, askedFromDeckOrHand);
    }
    
    public void TryToReversePlayFromHand(Play playOpponentIsTryingToMake)
    {
        List<Card> reversalCardsThatPlayerCanPlay = _playerNotPlayingRound.GetReversalCardsThatPlayerCanPlay(_bonusManager, 
                                                                                            playOpponentIsTryingToMake.Card);
        List<Card> reversalCardsThatPlayerCanPlayOnThisCard = GetReversalCardsThatPlayerCanPlayOnThisCard(reversalCardsThatPlayerCanPlay, 
                                                                                                            playOpponentIsTryingToMake);
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
        Play actualLastPlay = _lastPlayInstance.LastPlayPlayed;
        int actualDamage = _lastPlayInstance.ActualDamageMade;
        _lastPlayInstance.LastPlayPlayed = playOpponentIsTryingToMake;
        _lastPlayInstance.ActualDamageMade =
            ReversalUtils.ManageCardDamage(playOpponentIsTryingToMake, _playerNotPlayingRound, _bonusManager);
        if (actualLastPlay != null)
        {
            Card lastCard = actualLastPlay.Card;
            _lastPlayInstance.WasThisLastPlayAManeuverPlayedAfterIrishWhip =
                playOpponentIsTryingToMake.PlayedAs == "MANEUVER" && lastCard.Title == "Irish Whip";
        }
        List<Card> reversalCardsThatPlayerCanPlayOnThisCard = reversalCardsThatPlayerCanPlay
            .Where(cardThatCanPossibleReverse => CheckIfCardMeetsPrecondition(cardThatCanPossibleReverse,"Hand")).ToList();
        _lastPlayInstance.LastPlayPlayed = actualLastPlay;
        _lastPlayInstance.ActualDamageMade = actualDamage;
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
        _view.SayThatPlayerReversedTheCard(_playerNotPlayingRound.GetSuperstarName(), reversalSelected.ToString());
        reversalSelected.PlayedAs = "REVERSAL HAND";
        _lastPlayInstance.LastPlayPlayed = opponentPlay;
        _playerPlayingRound.MoveCardFromHandToRingside(opponentPlay.Card);
        ReversalUtils.SetDamageThatReversalShouldMake(_playerNotPlayingRound, opponentPlay, _bonusManager, reversalCardSelected);
        PerformEffect(reversalSelected);
        _lastPlayInstance.LastPlayPlayed = reversalSelected;
        _lastPlayInstance.WasItPlayedOnSameTurnThanActualPlay = false;
        
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
        Play reversalPlay = new Play(cardThatWasTurnedOver, "REVERSAL DECK");
        _playerPlayingRound.MoveCardFromHandToRingArea(cardPlayed);
        PerformEffect(reversalPlay);
        _view.SayThatCardWasReversedByDeck(_playerNotPlayingRound.GetSuperstarName());
        if (amountOfDamageReceivedAtMoment < totalCardDamage)
            ReversalUtils.PlayerDrawCardsStunValueEffect(cardPlayed, _view, _playerPlayingRound);
        _lastPlayInstance.LastPlayPlayed = reversalPlay;
        throw new CardWasReversedException(cardThatWasTurnedOver.Title);
    }

    private bool CheckIfCardThatWasTurnedOverCanReverse(Card cardThatWasTurnedOver, Play playPlayedByOpponent)
    {
        bool cardIsReversal = ReversalUtils.CheckIfCardIsReversal(cardThatWasTurnedOver);
        bool playerHasHigherFortitudeThanCard = CheckIfPlayerHasHigherFortitudeThanCard(cardThatWasTurnedOver, playPlayedByOpponent.Card);
        Play actualLastPlay = _lastPlayInstance.LastPlayPlayed;
        int actualDamage = _lastPlayInstance.ActualDamageMade;
        _lastPlayInstance.LastPlayPlayed = playPlayedByOpponent;
        _lastPlayInstance.ActualDamageMade =
            ReversalUtils.ManageCardDamage(playPlayedByOpponent, _playerNotPlayingRound, _bonusManager);
        if (cardIsReversal && playerHasHigherFortitudeThanCard)
        {
            bool doesItMeetPrecondition = CheckIfCardMeetsPrecondition(cardThatWasTurnedOver, "Deck");
            _lastPlayInstance.LastPlayPlayed = actualLastPlay;
            return doesItMeetPrecondition;
        }
        _lastPlayInstance.LastPlayPlayed = actualLastPlay;
        _lastPlayInstance.ActualDamageMade = actualDamage;
        return false;
    }

    private bool CheckIfPlayerHasHigherFortitudeThanCard(Card cardThatCouldReverseManeuver, Card cardThatCanBeReversed)
    {
        int fortitudeOfCardTryingToReverse = _bonusManager.GetPlayFortitude(cardThatCanBeReversed, cardThatCouldReverseManeuver);
        return _playerNotPlayingRound.CheckIfHasHigherFortitudeThanGiven(fortitudeOfCardTryingToReverse);
    }
}