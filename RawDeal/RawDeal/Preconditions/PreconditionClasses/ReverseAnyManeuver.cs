namespace RawDeal.Preconditions.PreconditionClasses;

public class ReverseAnyManeuver : Precondition
{
    private LastPlay _lastPlayInstance;
    public ReverseAnyManeuver(LastPlay lastPlayInstance)
    {
        _lastPlayInstance = lastPlayInstance;
    }
    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard, string askedFromDeskOrHand)
    {
        Play lastPlay = _lastPlayInstance.LastPlayPlayed;
        return lastPlay.PlayedAs == "MANEUVER";
    }

}