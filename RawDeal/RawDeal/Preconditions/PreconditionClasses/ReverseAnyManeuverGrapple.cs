namespace RawDeal.Preconditions.PreconditionClasses;

public class ReverseAnyManeuverGrapple : Precondition
{
    public override bool DoesMeetPrecondition(Play playThatIsBeingPlayed, string askedFromDeskOrHand, int netDamageThatWillReceive)
    {
        Card cardThatIsBeingPlayed = playThatIsBeingPlayed.Card;
        List<string> cardSubTypes = cardThatIsBeingPlayed.SubTypes;
        if (playThatIsBeingPlayed.PlayedAs == "MANEUVER" && cardSubTypes.Contains("Grapple"))
        {
            return true;
        }

        return false;
    }

}