namespace RawDeal.Preconditions.PreconditionClasses;

public class ReverseJockeyingFromHand : Precondition
{
    private readonly LastPlay _lastPlayInstance;

    public ReverseJockeyingFromHand(LastPlay lastPlayInstance)
    {
        _lastPlayInstance = lastPlayInstance;
    }

    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard,
        string askedFromDeskOrHand)
    {
        if (_lastPlayInstance.LastPlayPlayed is null) return true;
        if (_lastPlayInstance.LastPlayPlayed.Card.Title is "Tree of Woe" or "Austin Elbow Smash"
            or "Leaping Knee to the Face") return false;
        if (askedFromDeskOrHand == "Checking To Play Action") return true;
        var possibleLastPlay = _lastPlayInstance.LastPlayPlayed;
        var cardThatIsBeingPlayed = possibleLastPlay.Card;
        if (askedFromDeskOrHand == "Hand")
            return cardThatIsBeingPlayed.Title == "Jockeying for Position";
        return false;
    }
}