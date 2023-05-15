namespace RawDeal.ReversalCards;

public class BreakTheHold: ReversalCard
{
    public override void PerformEffect(string typeOfReversal, Player playerThatReversePlay, Player playerThatWasReversed)
    { }
    
    public override bool CheckIfCanReversePlay(Play playThatIsBeingPlayed)
    {
        Card cardThatIsBeingPlayed = playThatIsBeingPlayed.Card;
        List<string> cardSubTypes = cardThatIsBeingPlayed.SubTypes;
        if (playThatIsBeingPlayed.PlayedAs == "MANEUVER" && cardSubTypes.Contains("Submission"))
        {
            return true;
        }

        return false;
    }
}