namespace RawDeal.ReversalCards;

public class RollingTakedown: ReversalCard
{
    public override void PerformEffect(string typeOfReversal, Player playerThatReversePlay, Player playerThatWasReversed)
    {
        
    }
    
    public override bool CheckIfCanReversePlay(Play playThatIsBeingPlayed)
    {
        Card cardThatIsBeingPlayed = playThatIsBeingPlayed.Card;
        List<string> cardsSubtypes = cardThatIsBeingPlayed.Subtypes;
        int cardDamage = cardThatIsBeingPlayed.GetDamage();
        if (cardDamage <= 7 && cardsSubtypes.Contains("Grapple"))
        {
            return true;
        }

        return false;
    }
}