using RawDealView;
namespace RawDeal.Preconditions;

public abstract class Precondition
{
    public abstract bool DoesMeetPrecondition(Play playThatIsBeingPlayed, string askedFromDeskOrHand, int netDamageThatWillReceive);
}