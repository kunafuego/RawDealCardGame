namespace RawDeal.Preconditions.PreconditionClasses;

public class CrossBodyBlock : Precondition
{
    private readonly LastPlay _lastPlayInstance;

    public CrossBodyBlock(LastPlay lastPlayInstance)
    {
        _lastPlayInstance = lastPlayInstance;
    }

    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard,
        string askedFromDeskOrHand)
    {
        if (askedFromDeskOrHand == "Checking To Play Action")
        {
            var precondition = new AfterPlayingIrishWhip(_lastPlayInstance);
            return precondition.DoesMeetPrecondition(playerTryingToPlayCard, askedFromDeskOrHand);
        }
        else
        {
            var precondition =
                new ReverseManeuverPlayAfterIrishWhip(_lastPlayInstance, false);
            return precondition.DoesMeetPrecondition(playerTryingToPlayCard, askedFromDeskOrHand);
        }
    }
}