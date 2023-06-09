namespace RawDeal.Preconditions.PreconditionClasses;

public class TwoOrMoreCardsInHand : Precondition
{
    public override bool DoesMeetPrecondition(Play playThatIsBeingPlayed, string askedFromDeskOrHand, int netDamageThatWillReceive)
    {
        return false;
    }
}