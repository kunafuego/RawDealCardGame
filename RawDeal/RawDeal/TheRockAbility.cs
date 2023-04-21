using RawDealView;
namespace RawDeal;

class TheRockAbility : SuperstarAbility
{
    public TheRockAbility(Player player1, Player player2, View view) : base(player1, player2, view) {}

    public override void UseEffect(Player playerPlayingRound, Player playerNotPlayingRound, View view)
    {
        List<Card> cardsObjectsToShow = playerPlayingRound.GetCardsToShow(CardSet.RingsidePile);
        List<string> stringsOfRingsidePileCards = GetRCardsToShowAsString(cardsObjectsToShow);
        int cardIndex = view.AskPlayerToSelectCardsToRecover(playerPlayingRound.Superstar.Name, 1, stringsOfRingsidePileCards);
        playerPlayingRound.MoveCardFromRingsideToArsenal(cardsObjectsToShow[cardIndex]);
    }

    private List<string> GetRCardsToShowAsString(List<Card> cardsObjectsToShow)
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