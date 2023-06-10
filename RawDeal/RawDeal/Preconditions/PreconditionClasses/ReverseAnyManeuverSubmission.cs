namespace RawDeal.Preconditions.PreconditionClasses;

public class ReverseAnyManeuverSubmission : Precondition
{
    private LastPlay _lastPlayInstance;
    public ReverseAnyManeuverSubmission(LastPlay lastPlayInstance)
    {
        _lastPlayInstance = lastPlayInstance;
    }
    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard, string askedFromDeskOrHand, int netDamageThatWillReceive)
    {
        Play lastPlay = _lastPlayInstance.LastPlayPlayed;
        Card cardThatIsBeingPlayed = lastPlay.Card;
        List<string> cardSubTypes = cardThatIsBeingPlayed.SubTypes;
        return lastPlay.PlayedAs == "MANEUVER" && cardSubTypes.Contains("Submission");
    }
}