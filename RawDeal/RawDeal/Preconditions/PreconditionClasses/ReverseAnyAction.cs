namespace RawDeal.Preconditions.PreconditionClasses;

public class ReverseAnyAction : Precondition
{
    private LastPlay _lastPlayInstance;
    public ReverseAnyAction(LastPlay lastPlayInstance)
    {
        _lastPlayInstance = lastPlayInstance;
    }
    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard, string askedFromDeskOrHand)
    {
        if (_lastPlayInstance.LastPlayPlayed is null) return true;
        if (_lastPlayInstance.LastPlayPlayed.Card.Title is "Tree of Woe" or "Austin Elbow Smash"
            or "Leaping Knee to the Face") return false;
        Play lastPlay = _lastPlayInstance.LastPlayPlayed;
        return lastPlay.PlayedAs == "ACTION";
    }

}