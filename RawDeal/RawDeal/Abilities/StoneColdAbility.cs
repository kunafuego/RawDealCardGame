using RawDealView;
using RawDealView.Options;

namespace RawDeal;

internal class StoneColdAbility : SuperstarAbility
{
    public StoneColdAbility(View view) : base(view)
    {
    }

    public override void UseAbility(Player playerPlayingRound, Player playerNotPlayingRound)
    {
        DrawCardFromArsenalToHand(playerPlayingRound);
        ChooseAndMoveCardFromHandToArsenal(playerPlayingRound);
    }

    private void DrawCardFromArsenalToHand(Player playerDrawingCard)
    {
        playerDrawingCard.DrawSingleCard();
        View.SayThatPlayerDrawCards(playerDrawingCard.GetSuperstarName(), 1);
    }

    private void ChooseAndMoveCardFromHandToArsenal(Player playerGettingCardBack)
    {
        var handCardsObjectsToShow = playerGettingCardBack.GetCardsToShow(CardSet.Hand);
        var stringsOfHandCards = GetCardsToShowAsString(handCardsObjectsToShow);
        var handCardIndex =
            View.AskPlayerToReturnOneCardFromHisHandToHisArsenal(
                playerGettingCardBack.GetSuperstarName(), stringsOfHandCards);
        playerGettingCardBack.MoveCardFromHandToArsenal(handCardsObjectsToShow[handCardIndex]);
    }

    private List<string> GetCardsToShowAsString(CardsList cardsObjectsToShow)
    {
        var stringsOfCards = new List<string>();
        foreach (var card in cardsObjectsToShow) stringsOfCards.Add(card.ToString());

        return stringsOfCards;
    }

    public override bool MeetsTheRequirementsForUsingEffect(Player player)
    {
        return player.HasCardsInArsenal();
    }
}