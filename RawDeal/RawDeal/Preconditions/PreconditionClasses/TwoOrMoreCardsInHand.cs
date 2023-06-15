using RawDealView.Options;

namespace RawDeal.Preconditions.PreconditionClasses;

public class TwoOrMoreCardsInHand : Precondition
{
    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard,
        string askedFromDeskOrHand)
    {
        return playerTryingToPlayCard.CheckIfPlayerHasMoreThanOneCard(CardSet.Hand);
    }
}