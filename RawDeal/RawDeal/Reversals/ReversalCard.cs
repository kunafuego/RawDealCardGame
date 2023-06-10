using RawDealView;
namespace RawDeal.ReversalCards;

public abstract class ReversalCard
{
    protected View View;
    protected ReversalCard (View view)
    {
        View = view;
    }
    public abstract void PerformEffect(Play typeOfReversal, Card cardObject, Player playerThatReversePlay, Player playerThatWasReversed);

    public abstract bool CheckIfCanReversePlay(Player playerTryingToPlayCard, string askedFromDeskOrHand, int netDamageThatWillReceive);
}