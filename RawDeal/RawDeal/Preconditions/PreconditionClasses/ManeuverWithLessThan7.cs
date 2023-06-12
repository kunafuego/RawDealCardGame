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
        // Console.WriteLine("\n");
        // Console.WriteLine(_lastPlayInstance.ActualDamageMade);
        // Console.WriteLine(_lastPlayInstance.LastPlayPlayed.PlayedAs);
        // Console.WriteLine("\n");
        if (_lastPlayInstance.LastPlayPlayed is null) return true;
        Play lastPlay = _lastPlayInstance.LastPlayPlayed;
        if (_lastPlayInstance.ActualDamageMade <= 7 && lastPlay.PlayedAs is "MANEUVER")
        {
            return true;
        }
        return false;
    }
}