namespace RawDeal.Preconditions.PreconditionClasses;

public class ReverseAnyManeuverSubmission : Precondition
{
    private readonly LastPlay _lastPlayInstance;

    public ReverseAnyManeuverSubmission(LastPlay lastPlayInstance)
    {
        _lastPlayInstance = lastPlayInstance;
    }

    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard,
        string askedFromDeskOrHand)
    {
        if (_lastPlayInstance.LastPlayPlayed is null) return true;
        if (_lastPlayInstance.LastPlayPlayed.Card.Title is "Tree of Woe" or "Austin Elbow Smash"
            or "Leaping Knee to the Face") return false;

        var lastPlay = _lastPlayInstance.LastPlayPlayed;
        var cardThatIsBeingPlayed = lastPlay.Card;
        var cardSubTypes = cardThatIsBeingPlayed.SubTypes;
        return lastPlay.PlayedAs == "MANEUVER" && cardSubTypes.Contains("Submission");
    }
}