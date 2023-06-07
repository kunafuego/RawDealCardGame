namespace RawDeal.Preconditions.PreconditionClasses;

public class NoPrecondition : Precondition
{
    public override bool DoesMeetPrecondition(Play playThatIsBeingPlayed, string askedFromDeskOrHand, int netDamageThatWillReceive)
    {
        return true;
    }

}