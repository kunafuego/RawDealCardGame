namespace RawDeal.Preconditions.PreconditionClasses;

public class NoPrecondition : Precondition
{
    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard, string askedFromDeskOrHand, int netDamageThatWillReceive)
    {
        return true;
    }

}