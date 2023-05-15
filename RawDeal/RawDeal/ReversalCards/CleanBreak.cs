namespace RawDeal.ReversalCards;

public class CleanBreak: ReversalCard
{
    public override void PerformEffect(string typeOfReversal, Player playerThatReversePlay, Player playerThatWasReversed)
    {
        throw new NotImplementedException();
    }
    
    public override bool CheckIfCanReversePlay(Play playThatIsBeingPlayed)
    {
        return playThatIsBeingPlayed.PlayedAs == "ACTION";
    }
}