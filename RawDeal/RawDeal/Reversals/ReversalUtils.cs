using RawDeal.Bonus;
using RawDealView;

namespace RawDeal.Reversals;

public static class ReversalUtils
{
    public static List<Play> GetPlaysOfAvailablesCards(List<Card> reversalCardThatPlayerCanPlay)
    {
        List<Play> playsToReturn = new();
        foreach (Card card in reversalCardThatPlayerCanPlay)
        {
            Play play = new Play(card, "REVERSAL");
            playsToReturn.Add(play);
        }

        return playsToReturn;
    }

    public static void SetDamageThatReversalShouldMake(Player playerNotPlayingRound, Play playOpponentWasTryingToPlay,
        BonusManager bonusManager, Card reversalCardSelected)
    {
        Card cardOpponentWasTryingToPlay = playOpponentWasTryingToPlay.Card;
        if (reversalCardSelected.Title != "Rolling Takedown" && reversalCardSelected.Title != "Knee to the Gut") return;
        int damageWithBonus = bonusManager.GetPlayDamage(playOpponentWasTryingToPlay, playerNotPlayingRound);
        if (playerNotPlayingRound.GetSuperstarName() == "MANKIND") damageWithBonus += 1;
        cardOpponentWasTryingToPlay.ReversalDamage = damageWithBonus;
    }
    
    public static bool CheckIfCardIsReversal(Card card)
    {
        List<string> cardTypes = card.Types;
        return cardTypes.Contains("Reversal");
    }
    
    public static void PlayerDrawCardsStunValueEffect(Card card, View view, Player playerPlayingRound)
    {
        int amountOfCardsToDraw = 0;
        if (card.StunValue > 0)
        { 
            amountOfCardsToDraw =
                view.AskHowManyCardsToDrawBecauseOfStunValue(playerPlayingRound.GetSuperstarName(), card.StunValue);
        }
        if (amountOfCardsToDraw > 0) view.SayThatPlayerDrawCards(playerPlayingRound.GetSuperstarName(), amountOfCardsToDraw);
        for (int i = 0; i < amountOfCardsToDraw; i++)
        {
            playerPlayingRound.DrawSingleCard();
        }
    }
    
    public static int ManageCardDamage(Play play, Player playerNotPlayingRound, BonusManager bonusManager)
    {
        return bonusManager.GetPlayDamage(play, playerNotPlayingRound);
    }
}