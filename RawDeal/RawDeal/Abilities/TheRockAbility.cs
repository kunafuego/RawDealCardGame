using RawDealView.Options;
using RawDealView;
namespace RawDeal;

class TheRockAbility : SuperstarAbility
{
    public TheRockAbility(View view) : base(view) {}

    public override void UseAbility(Player playerPlayingRound, Player playerNotPlayingRound)
    {
        RecoverCard(playerPlayingRound);
    }

    private void RecoverCard(Player playerThatWillRecoverCard)
    {
        List<Card> cardsObjectsToShow = playerThatWillRecoverCard.GetCardsToShow(CardSet.RingsidePile);
        List<string> stringsOfRingsidePileCards = GetCardsToShowAsString(cardsObjectsToShow);
        int cardIndex = View.AskPlayerToSelectCardsToRecover(playerThatWillRecoverCard.GetSuperstarName(), 1, stringsOfRingsidePileCards);
        playerThatWillRecoverCard.MoveCardFromRingsideToArsenal(cardsObjectsToShow[cardIndex]);
    }

    private List<string> GetCardsToShowAsString(List<Card> cardsObjectsToShow)
    {
        List<string> stringsOfRingsidePileCards = new List<string>();
        foreach (Card card in cardsObjectsToShow)
        {
            stringsOfRingsidePileCards.Add(card.ToString());
        }

        return stringsOfRingsidePileCards;
    }

    public override bool MeetsTheRequirementsForUsingEffect(Player player)
    {
        return player.HasCards(CardSet.RingsidePile);
    }

}