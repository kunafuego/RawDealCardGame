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
        if (_lastPlayInstance.LastPlayPlayed is null) return true;
        if (_lastPlayInstance.LastPlayPlayed.Card.Title is "Tree of Woe" or "Austin Elbow Smash"
            or "Leaping Knee to the Face") return false;
        // Console.WriteLine("\n");
        // Console.WriteLine(_lastPlayInstance.ActualDamageMade);
        // Console.WriteLine(_lastPlayInstance.LastPlayPlayed.PlayedAs);
        // Console.WriteLine("\n");
        Play lastPlay = _lastPlayInstance.LastPlayPlayed;
        if (_lastPlayInstance.ActualDamageMade <= 7 && lastPlay.PlayedAs is "MANEUVER")
        {
            return true;
        }
        return false;
    }
}