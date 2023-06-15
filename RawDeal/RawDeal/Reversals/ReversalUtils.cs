using RawDeal.Bonus;
using RawDealView;

namespace RawDeal.Reversals;

public static class ReversalUtils
{
    public static List<Play> GetPlaysOfAvailablesCards(CardsList reversalCardThatPlayerCanPlay)
    {
        List<Play> playsToReturn = new();
        foreach (var card in reversalCardThatPlayerCanPlay)
        {
            var play = new Play(card, "REVERSAL");
            playsToReturn.Add(play);
        }

        return playsToReturn;
    }

    public static void SetDamageThatReversalShouldMake(Player playerNotPlayingRound,
        Play playOpponentWasTryingToPlay,
        BonusManager bonusManager)
    {
        var cardOpponentWasTryingToPlay = playOpponentWasTryingToPlay.Card;
        var damageWithBonus =
            bonusManager.GetPlayDamage(playOpponentWasTryingToPlay, playerNotPlayingRound);
        if (playerNotPlayingRound.GetSuperstarName() == "MANKIND") damageWithBonus += 1;
        cardOpponentWasTryingToPlay.ReversalDamage = damageWithBonus;
    }

    public static bool CheckIfCardIsReversal(Card card)
    {
        var cardTypes = card.Types;
        return cardTypes.Contains("Reversal");
    }

    public static void PlayerDrawCardsStunValueEffect(Card card, View view,
        Player playerPlayingRound)
    {
        var amountOfCardsToDraw = 0;
        if (card.StunValue > 0)
            amountOfCardsToDraw =
                view.AskHowManyCardsToDrawBecauseOfStunValue(playerPlayingRound.GetSuperstarName(),
                    card.StunValue);
        if (amountOfCardsToDraw > 0)
            view.SayThatPlayerDrawCards(playerPlayingRound.GetSuperstarName(), amountOfCardsToDraw);
        for (var i = 0; i < amountOfCardsToDraw; i++) playerPlayingRound.DrawSingleCard();
    }

    public static int ManageCardDamage(Play play, Player playerNotPlayingRound,
        BonusManager bonusManager)
    {
        return bonusManager.GetPlayDamage(play, playerNotPlayingRound);
    }

    public static void MarkReversalAsPlayedFromHand(Play reversalSelected)
    {
        reversalSelected.PlayedAs = "REVERSAL HAND";
    }

    public static void UpdateLastPlayOnly(LastPlay lastPlayInstance, Play play)
    {
        lastPlayInstance.LastPlayPlayed = play;
    }

    public static void MoveCardFromHandToRingside(Player playerPlayingRound,
        Card card)
    {
        playerPlayingRound.MoveCardFromHandToRingside(card);
    }

    public static void ResetWasItPlayedOnSameTurnFlag(LastPlay lastPlayInstance)
    {
        lastPlayInstance.WasItPlayedOnSameTurnThanActualPlay = false;
    }
}