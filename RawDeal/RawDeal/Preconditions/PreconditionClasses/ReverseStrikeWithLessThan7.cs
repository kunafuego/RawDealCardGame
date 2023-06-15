namespace RawDeal.Preconditions.PreconditionClasses;

public class ReverseStrikeWithLessThan7 : Precondition
{
    private readonly LastPlay _lastPlayInstance;

    public ReverseStrikeWithLessThan7(LastPlay lastPlayInstance)
    {
        _lastPlayInstance = lastPlayInstance;
    }

    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard,
        string askedFromDeskOrHand)
    {
        if (_lastPlayInstance.LastPlayPlayed is null) return true;
        var possibleLastPlay = _lastPlayInstance.LastPlayPlayed;
        var cardThatIsBeingPlayed = possibleLastPlay.Card;
        var cardsSubtypes = cardThatIsBeingPlayed.Subtypes;
        if (_lastPlayInstance.ActualDamageMade <= 7 && possibleLastPlay.PlayedAs == "MANEUVER" &&
            cardsSubtypes.Contains("Strike")) return true;

        return false;
    }
}