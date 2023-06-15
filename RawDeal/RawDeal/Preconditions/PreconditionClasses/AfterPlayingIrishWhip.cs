namespace RawDeal.Preconditions.PreconditionClasses;

public class AfterPlayingIrishWhip : Precondition
{
    private readonly LastPlay _lastPlayInstance;

    public AfterPlayingIrishWhip(LastPlay lastPlayInstance)
    {
        _lastPlayInstance = lastPlayInstance;
    }

    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard,
        string askedFromDeskOrHand)
    {
        if (_lastPlayInstance.LastPlayPlayed is null) return false;
        if (!_lastPlayInstance.WasItPlayedOnSameTurnThanActualPlay) return false;
        var lastPlay = _lastPlayInstance.LastPlayPlayed;
        var lastCard = lastPlay.Card;
        return lastCard.Title == "Irish Whip";
    }
}