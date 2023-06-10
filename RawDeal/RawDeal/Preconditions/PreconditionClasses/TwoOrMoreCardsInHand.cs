namespace RawDeal.Preconditions.PreconditionClasses;

public class TwoOrMoreCardsInHand : Precondition
{
    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard, string askedFromDeskOrHand, int netDamageThatWillReceive)
    {
        return false;
    }
}