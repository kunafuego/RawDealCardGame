namespace RawDeal.Preconditions.PreconditionClasses;

public class ManeuverWithLessThan7 : Precondition
{
    private LastPlay _lastPlayInstance;
    public ManeuverWithLessThan7(LastPlay lastPlayInstance)
    {
        _lastPlayInstance = lastPlayInstance;
    }
    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard, string askedFromDeskOrHand)
    {
        Play lastPlay = _lastPlayInstance.LastPlayPlayed;
        if (_lastPlayInstance.ActualDamageMade <= 7 && lastPlay.PlayedAs == "MANEUVER")
        {
            return true;
        }
        return false;
    }
}