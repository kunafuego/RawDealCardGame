namespace RawDeal.Preconditions.PreconditionClasses;

public class CrossBodyBlock : Precondition
{
    private LastPlay _lastPlayInstance;
    
    public CrossBodyBlock(LastPlay lastPlayInstance)
    {
        _lastPlayInstance = lastPlayInstance;
    }
    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard, string askedFromDeskOrHand)
    {
        Console.WriteLine($"QUE VERGA {askedFromDeskOrHand}");
        if (askedFromDeskOrHand == "Checking To Play Action")
        {
            AfterPlayingIrishWhip precondition = new AfterPlayingIrishWhip(_lastPlayInstance);
            return precondition.DoesMeetPrecondition(playerTryingToPlayCard, askedFromDeskOrHand);
        }
        else
        {
            ReverseManeuverPlayAfterIrishWhip precondition =
                new ReverseManeuverPlayAfterIrishWhip(_lastPlayInstance, false);
            return precondition.DoesMeetPrecondition(playerTryingToPlayCard, askedFromDeskOrHand);
        }
    }

}