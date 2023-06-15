using RawDeal.Bonus;
using RawDeal.Reversals;
using RawDealView;

namespace RawDeal;

public class ReversalManager
{
    private readonly BonusManager _bonusManager;
    private readonly LastPlay _lastPlayInstance;
    private readonly Player _playerNotPlayingRound;
    private readonly Player _playerPlayingRound;
    private readonly View _view;

    public ReversalManager(View view, Player playerPlayingRound, Player playerNotPlayingRound,
        BonusManager bonusManager, LastPlay lastPlayInstance)
    {
        _view = view;
        _playerPlayingRound = playerPlayingRound;
        _playerNotPlayingRound = playerNotPlayingRound;
        _lastPlayInstance = lastPlayInstance;
        _bonusManager = bonusManager;
    }


    private void PerformEffect(Play reversalPlay)
    {
        var cardThatIsReversingTurn = reversalPlay.Card;
        var cardEffects = cardThatIsReversingTurn.EffectObject;
        foreach (var effect in cardEffects)
            effect.Apply(reversalPlay,
                _playerNotPlayingRound,
                _playerPlayingRound);
        MoveCardUsedForReversingTurn(reversalPlay);
    }

    private void MoveCardUsedForReversingTurn(Play reversalPlay)
    {
        if (reversalPlay.PlayedAs == "REVERSAL HAND")
            _playerNotPlayingRound.MoveCardFromHandToRingArea(reversalPlay.Card);
        else if (reversalPlay.PlayedAs == "REVERSAL DECK")
            _playerNotPlayingRound.MoveArsenalTopCardToRingside();
    }

    private bool CheckIfCardMeetsPrecondition(Card cardThatCanPossibleReverse,
        string askedFromDeckOrHand)
    {
        var cardPrecondition = cardThatCanPossibleReverse.Precondition;
        return cardPrecondition.DoesMeetPrecondition(_playerNotPlayingRound, askedFromDeckOrHand);
    }

    public void TryToReversePlayFromHand(Play playOpponentIsTryingToMake)
    {
        var reversalCardsThatPlayerCanPlay =
            _playerNotPlayingRound.GetReversalCardsThatPlayerCanPlay(_bonusManager,
                playOpponentIsTryingToMake.Card);
        var reversalCardsThatPlayerCanPlayOnThisCard =
            GetReversalCardsThatPlayerCanPlayOnThisCard(reversalCardsThatPlayerCanPlay,
                playOpponentIsTryingToMake);
        if (reversalCardsThatPlayerCanPlayOnThisCard.Any())
        {
            var reversalPlays =
                ReversalUtils.GetPlaysOfAvailablesCards(reversalCardsThatPlayerCanPlayOnThisCard);
            var choiceSelected = AskUserToSelectReversal(reversalPlays);
            if (choiceSelected != -1)
                ReversePlayFromHand(playOpponentIsTryingToMake, reversalPlays[choiceSelected]);
        }
    }

    private CardsList GetReversalCardsThatPlayerCanPlayOnThisCard(
        CardsList reversalCardsThatPlayerCanPlay, Play playOpponentIsTryingToMake)
    {
        var actualLastPlay = _lastPlayInstance.LastPlayPlayed;
        var actualDamage = _lastPlayInstance.ActualDamageMade;
        UpdateLastPlayInstance(playOpponentIsTryingToMake);
        if (actualLastPlay != null)
            UpdateLastPlayInstanceAfterIrishWhip(playOpponentIsTryingToMake, actualLastPlay);
        var reversalCardsThatPlayerCanPlayOnThisCard = FilterPlayableReversalCards(reversalCardsThatPlayerCanPlay);
        RestoreLastPlayInstance(actualLastPlay, actualDamage);
        return reversalCardsThatPlayerCanPlayOnThisCard;
    }

    private void UpdateLastPlayInstance(Play play)
    {
        _lastPlayInstance.LastPlayPlayed = play;
        _lastPlayInstance.ActualDamageMade =
            ReversalUtils.ManageCardDamage(play, _playerNotPlayingRound, _bonusManager);
    }

    private void UpdateLastPlayInstanceAfterIrishWhip(Play currentPlay, Play lastPlay)
    {
        var lastCard = lastPlay.Card;
        _lastPlayInstance.WasThisLastPlayAManeuverPlayedAfterIrishWhip =
            currentPlay.PlayedAs == "MANEUVER" && lastCard.Title == "Irish Whip";
    }

    private CardsList FilterPlayableReversalCards(CardsList reversalCards)
    {
        return reversalCards
            .FilterCards(card => CheckIfCardMeetsPrecondition(card, "Hand"));
    }

    private void RestoreLastPlayInstance(Play lastPlay, int actualDamage)
    {
        _lastPlayInstance.LastPlayPlayed = lastPlay;
        _lastPlayInstance.ActualDamageMade = actualDamage;
    }


    private int AskUserToSelectReversal(PlaysList reversalPlays)
    {
        var reversalPlaysString = reversalPlays.Select(play => play.ToString()).ToList();
        var usersChoice = _view.AskUserToSelectAReversal(_playerNotPlayingRound.GetSuperstarName(),
            reversalPlaysString);
        return usersChoice;
    }

    private void ReversePlayFromHand(Play opponentPlay, Play reversalSelected)
    {
        var reversalCardSelected = reversalSelected.Card;
        NotifyPlayerOfReversal(reversalSelected);
        ReversalUtils.MarkReversalAsPlayedFromHand(reversalSelected);
        ReversalUtils.UpdateLastPlayOnly(_lastPlayInstance, opponentPlay);
        ReversalUtils.MoveCardFromHandToRingside(_playerPlayingRound, opponentPlay.Card);
        SetReversalDamage(opponentPlay, reversalCardSelected);
        PerformEffect(reversalSelected);
        UpdateLastPlayOnly(reversalSelected);
        ReversalUtils.ResetWasItPlayedOnSameTurnFlag(_lastPlayInstance);

        throw new CardWasReversedException(reversalCardSelected.Title);
    }

    private void NotifyPlayerOfReversal(Play reversalSelected)
    {
        _view.SayThatPlayerReversedTheCard(_playerNotPlayingRound.GetSuperstarName(), reversalSelected.ToString());
    }

    private void UpdateLastPlayOnly(Play play)
    {
        _lastPlayInstance.LastPlayPlayed = play;
    }
    private void SetReversalDamage(Play opponentPlay, Card reversalCard)
    {
        if (reversalCard.Title != "Rolling Takedown" &&
            reversalCard.Title != "Knee to the Gut") return;
        ReversalUtils.SetDamageThatReversalShouldMake(_playerNotPlayingRound, opponentPlay,
                                                    _bonusManager);
    }
    
    public void CheckIfManeuverCanBeReversedFromDeckWithASpecificCard(
        int amountOfDamageReceivedAtMoment, int totalCardDamage, Card cardPlayed)
    {
        var cardThatWasTurnedOver = _playerNotPlayingRound.GetCardOnTopOfArsenal();
        var cardCanReverseReceivingDamage =
            CheckIfCardThatWasTurnedOverCanReverse(cardThatWasTurnedOver,
                new Play(cardPlayed, "MANEUVER"));
        if (cardCanReverseReceivingDamage)
            ReversePlayFromDeck(amountOfDamageReceivedAtMoment, totalCardDamage, cardPlayed,
                cardThatWasTurnedOver);
    }

    private void ReversePlayFromDeck(int amountOfDamageReceivedAtMoment, int totalCardDamage,
        Card cardPlayed,
        Card cardThatWasTurnedOver)
    {
        var reversalPlay = new Play(cardThatWasTurnedOver, "REVERSAL DECK");
        _playerPlayingRound.MoveCardFromHandToRingArea(cardPlayed);
        PerformEffect(reversalPlay);
        _view.SayThatCardWasReversedByDeck(_playerNotPlayingRound.GetSuperstarName());
        if (amountOfDamageReceivedAtMoment < totalCardDamage)
            ReversalUtils.PlayerDrawCardsStunValueEffect(cardPlayed, _view, _playerPlayingRound);
        _lastPlayInstance.LastPlayPlayed = reversalPlay;
        throw new CardWasReversedException(cardThatWasTurnedOver.Title);
    }

private bool CheckIfCardThatWasTurnedOverCanReverse(Card cardThatWasTurnedOver, 
                                                    Play playPlayedByOpponent)
{
    var cardIsReversal = ReversalUtils.CheckIfCardIsReversal(cardThatWasTurnedOver);
    var playerHasHigherFortitudeThanCard = CheckIfPlayerHasHigherFortitudeThanCard(
        cardThatWasTurnedOver, playPlayedByOpponent.Card);
    var actualLastPlay = _lastPlayInstance.LastPlayPlayed;
    var actualDamage = _lastPlayInstance.ActualDamageMade;
    UpdateLastPlayInstance(playPlayedByOpponent);
    if (cardIsReversal && playerHasHigherFortitudeThanCard)
    {
        var doesItMeetPrecondition = CheckIfCardMeetsPrecondition(cardThatWasTurnedOver, 
                                                                    "Deck");
        _lastPlayInstance.LastPlayPlayed = actualLastPlay;
        return doesItMeetPrecondition;
    }
    RestoreLastPlayInstance(actualLastPlay, actualDamage);
    return false;
}

    private bool CheckIfPlayerHasHigherFortitudeThanCard(Card cardThatCouldReverseManeuver,
        Card cardThatCanBeReversed)
    {
        var fortitudeOfCardTryingToReverse =
            _bonusManager.GetPlayFortitude(cardThatCanBeReversed, cardThatCouldReverseManeuver);
        return _playerNotPlayingRound.CheckIfHasHigherFortitudeThanGiven(
            fortitudeOfCardTryingToReverse);
    }
}