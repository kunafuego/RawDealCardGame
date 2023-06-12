namespace RawDeal.Preconditions.PreconditionClasses;

public class ReverseAnyManeuverStrike : Precondition
{
    private LastPlay _lastPlayInstance;
    public ReverseAnyManeuverStrike(LastPlay lastPlayInstance)
    {
        _lastPlayInstance = lastPlayInstance;
    }
    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard, string askedFromDeskOrHand)
    {
        if (_lastPlayInstance.LastPlayPlayed is null) return true;

        Play lastPlay = _lastPlayInstance.LastPlayPlayed;
        Card cardThatIsBeingPlayed = lastPlay.Card;
        List<string> cardSubTypes = cardThatIsBeingPlayed.SubTypes;
        return lastPlay.PlayedAs == "MANEUVER" && cardSubTypes.Contains("Strike");
    }

}