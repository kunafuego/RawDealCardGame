namespace RawDeal.Preconditions.PreconditionClasses;

public class ReverseAnyManeuverGrapple : Precondition
{
    private LastPlay _lastPlayInstance;
    public ReverseAnyManeuverGrapple(LastPlay lastPlayInstance)
    {
        _lastPlayInstance = lastPlayInstance;
    }
    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard, string askedFromDeskOrHand)
    {
        if (_lastPlayInstance.LastPlayPlayed is null) return true;

        Play lastPlay = _lastPlayInstance.LastPlayPlayed;
        Card cardThatIsBeingPlayed = lastPlay.Card;
        List<string> cardSubTypes = cardThatIsBeingPlayed.SubTypes;
        return lastPlay.PlayedAs == "MANEUVER" && cardSubTypes.Contains("Grapple");
    }

}