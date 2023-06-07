namespace RawDeal.Preconditions.PreconditionClasses;

public class ManeuverWithLessThan7 : Precondition
{
    public override bool DoesMeetPrecondition(Play playThatIsBeingPlayed, string askedFromDeskOrHand, int netDamageThatWillReceive)
    {
        if (netDamageThatWillReceive <= 7 && playThatIsBeingPlayed.PlayedAs == "MANEUVER")
        {
            return true;
        }
        return false;
    }
}