using RawDealView;
namespace RawDeal.Preconditions;

public abstract class Precondition
{
    public abstract bool DoesMeetPrecondition(Player playerTryingToPlayCard, string askedFromDeskOrHand);
}