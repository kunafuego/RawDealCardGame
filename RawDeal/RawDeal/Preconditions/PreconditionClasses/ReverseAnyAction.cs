namespace RawDeal.Preconditions.PreconditionClasses;

public class ReverseAnyAction : Precondition
{
    public override bool DoesMeetPrecondition(Play playThatIsBeingPlayed, string askedFromDeskOrHand, int netDamageThatWillReceive)
    {
        return playThatIsBeingPlayed.PlayedAs == "ACTION";
    }

}