namespace RawDeal.ReversalCards;

public abstract class ReversalCard
{
    public abstract void PerformEffect(string typeOfReversal, Player playerThatReversePlay, Player playerThatWasReversed);
    public abstract bool CheckIfCanReversePlay(Play playThatIsBeingPlayed);
}