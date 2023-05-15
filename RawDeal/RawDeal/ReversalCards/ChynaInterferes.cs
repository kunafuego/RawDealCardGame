namespace RawDeal.ReversalCards;

public class ChynaInterferes: ReversalCard
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