namespace RawDeal.Preconditions.PreconditionClasses;

public class ReverseGrappleWithLessThan7 : Precondition
{
    private LastPlay _lastPlayInstance;
    public ReverseGrappleWithLessThan7(LastPlay lastPlayInstance)
    {
        _lastPlayInstance = lastPlayInstance;
    }
    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard, string askedFromDeskOrHand)
    {
        if (_lastPlayInstance.LastPlayPlayed is null) return true;
        if (_lastPlayInstance.LastPlayPlayed.Card.Title is "Tree of Woe" or "Austin Elbow Smash"
            or "Leaping Knee to the Face") return false;

        Play possibleLastPlay = _lastPlayInstance.LastPlayPlayed;
        Card cardThatIsBeingPlayed = possibleLastPlay.Card;
        List<string> cardsSubtypes = cardThatIsBeingPlayed.Subtypes;
        if (_lastPlayInstance.ActualDamageMade <= 7 && possibleLastPlay.PlayedAs == "MANEUVER" && cardsSubtypes.Contains("Grapple"))
        {
            return true;
        }

        return false;
    }
}