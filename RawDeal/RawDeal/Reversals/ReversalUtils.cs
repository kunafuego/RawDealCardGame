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
    
    public static void SetDamageThatReversalShouldMake(Card reversalCardSelected, Card cardOpponentWasTryingToPlay, EffectForNextMove nextMoveEffect)
    {
        if (reversalCardSelected.Title == "Rolling Takedown" || reversalCardSelected.Title == "Knee to the Gut")
        {
            cardOpponentWasTryingToPlay.ReversalDamage = cardOpponentWasTryingToPlay.GetDamage() + nextMoveEffect.DamageChange;
        }
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
    
    public static int ManageCardDamage(Card cardPlayed, Player playerNotPlayingRound, EffectForNextMove nextMoveEffect)
    {
        int initialDamage = cardPlayed.GetDamage();
        if (AbilitiesManager.CheckIfHasAbilityWhenReceivingDamage(playerNotPlayingRound))
        {
            initialDamage -= 1;
        }
        int extraDamage = (cardPlayed.CheckIfSubtypesContain("Grapple")) ? nextMoveEffect.DamageChange : 0;
        return initialDamage + extraDamage;
    }
}