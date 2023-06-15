using RawDealView;
using RawDealView.Options;

namespace RawDeal;

internal class TheRockAbility : SuperstarAbility
{
    public TheRockAbility(View view) : base(view)
    {
    }

    public override void UseAbility(Player playerPlayingRound, Player playerNotPlayingRound)
    {
        RecoverCard(playerPlayingRound);
    }

    private void RecoverCard(Player playerThatWillRecoverCard)
    {
        var cardsObjectsToShow = playerThatWillRecoverCard.GetCardsToShow(CardSet.RingsidePile);
        var stringsOfRingsidePileCards = GetCardsToShowAsString(cardsObjectsToShow);
        var cardIndex = View.AskPlayerToSelectCardsToRecover(
            playerThatWillRecoverCard.GetSuperstarName(), 1, stringsOfRingsidePileCards);
        playerThatWillRecoverCard.MoveCardFromRingsideToArsenal(cardsObjectsToShow[cardIndex]);
    }

    private List<string> GetCardsToShowAsString(CardsList cardsObjectsToShow)
    {
        var stringsOfRingsidePileCards = new List<string>();
        foreach (var card in cardsObjectsToShow) stringsOfRingsidePileCards.Add(card.ToString());

        return stringsOfRingsidePileCards;
    }

    public override bool MeetsTheRequirementsForUsingEffect(Player player)
    {
        return player.HasCards(CardSet.RingsidePile);
    }
}