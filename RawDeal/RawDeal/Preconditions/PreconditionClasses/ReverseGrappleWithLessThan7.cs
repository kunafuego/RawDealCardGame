namespace RawDeal.Preconditions.PreconditionClasses;

public class ReverseGrappleWithLessThan7 : Precondition
{
    private LastPlay _lastPlayInstance;
    public ReverseGrappleWithLessThan7(LastPlay lastPlayInstance)
    {
        _lastPlayInstance = lastPlayInstance;
    }
    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard, string askedFromDeskOrHand, int netDamageThatWillReceive)
    {
        Play possibleLastPlay = _lastPlayInstance.LastPlayPlayed;
        Card cardThatIsBeingPlayed = possibleLastPlay.Card;
        List<string> cardsSubtypes = cardThatIsBeingPlayed.Subtypes;
        if (netDamageThatWillReceive <= 7 && possibleLastPlay.PlayedAs == "MANEUVER" && cardsSubtypes.Contains("Grapple"))
        {
            return true;
        }

        return false;
    }
}