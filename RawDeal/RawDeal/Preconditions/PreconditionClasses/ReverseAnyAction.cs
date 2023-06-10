namespace RawDeal.Preconditions.PreconditionClasses;

public class ReverseAnyAction : Precondition
{
    private LastPlay _lastPlayInstance;
    public ReverseAnyAction(LastPlay lastPlayInstance)
    {
        _lastPlayInstance = lastPlayInstance;
    }
    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard, string askedFromDeskOrHand, int netDamageThatWillReceive)
    {
        Play lastPlay = _lastPlayInstance.LastPlayPlayed;
        return lastPlay.PlayedAs == "ACTION";
    }

}