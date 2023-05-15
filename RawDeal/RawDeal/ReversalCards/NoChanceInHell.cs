namespace RawDeal.ReversalCards;

public class NoChanceInHell: ReversalCard
{
    public override void PerformEffect(string typeOfReversal, Player playerThatReversePlay, Player playerThatWasReversed)
    { }
    
    public override bool CheckIfCanReversePlay(Play playThatIsBeingPlayed)
    {
        return playThatIsBeingPlayed.PlayedAs == "ACTION";
    }
}