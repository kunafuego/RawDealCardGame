namespace RawDeal.Preconditions.PreconditionClasses;

public class ReverseManeuverPlayAfterIrishWhip : Precondition
{
    private readonly bool _handNecessary;
    private readonly LastPlay _lastPlayInstance;

    public ReverseManeuverPlayAfterIrishWhip(LastPlay lastPlayInstance, bool handNecessary)
    {
        _lastPlayInstance = lastPlayInstance;
        _handNecessary = handNecessary;
    }

    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard,
        string askedFromDeskOrHand)
    {
        if (_lastPlayInstance.LastPlayPlayed is null) return false;
        if (askedFromDeskOrHand == "Checking To Play Action") return true;
        if (_lastPlayInstance.LastPlayPlayed.Card.Title is "Tree of Woe" or "Austin Elbow Smash"
            or "Leaping Knee to the Face") return false;
        if (_lastPlayInstance.WasThisLastPlayAManeuverPlayedAfterIrishWhip)
            return !_handNecessary || askedFromDeskOrHand == "Hand";

        return false;
    }
}