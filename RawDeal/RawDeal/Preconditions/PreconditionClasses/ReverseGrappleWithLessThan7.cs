namespace RawDeal.Preconditions.PreconditionClasses;

public class ReverseGrappleWithLessThan7 : Precondition
{
    public override bool DoesMeetPrecondition(Play playThatIsBeingPlayed, string askedFromDeskOrHand, int netDamageThatWillReceive)
    {
        Card cardThatIsBeingPlayed = playThatIsBeingPlayed.Card;
        List<string> cardsSubtypes = cardThatIsBeingPlayed.Subtypes;
        if (netDamageThatWillReceive <= 7 && playThatIsBeingPlayed.PlayedAs == "MANEUVER" && cardsSubtypes.Contains("Grapple"))
        {
            return true;
        }

        return false;
    }
}