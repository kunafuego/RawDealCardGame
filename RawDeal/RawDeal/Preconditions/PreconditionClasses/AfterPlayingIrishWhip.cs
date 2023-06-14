namespace RawDeal.Preconditions.PreconditionClasses;

public class AfterPlayingIrishWhip : Precondition
{
    private LastPlay _lastPlayInstance;
    public AfterPlayingIrishWhip(LastPlay lastPlayInstance)
    {
        _lastPlayInstance = lastPlayInstance;
    }
    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard, string askedFromDeskOrHand)
    {
        if (_lastPlayInstance.LastPlayPlayed is null) return false;
        if (!_lastPlayInstance.WasItPlayedOnSameTurnThanActualPlay) return false;
        Play lastPlay = _lastPlayInstance.LastPlayPlayed;
        Card lastCard = lastPlay.Card;
        return lastCard.Title == "Irish Whip";
    }
}