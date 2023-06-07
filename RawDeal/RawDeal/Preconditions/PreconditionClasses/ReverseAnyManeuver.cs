namespace RawDeal.Preconditions.PreconditionClasses;

public class ReverseAnyManeuver : Precondition
{
    public override bool DoesMeetPrecondition(Play playThatIsBeingPlayed, string askedFromDeskOrHand, int netDamageThatWillReceive)
    {
        return playThatIsBeingPlayed.PlayedAs == "MANEUVER";
    }

}