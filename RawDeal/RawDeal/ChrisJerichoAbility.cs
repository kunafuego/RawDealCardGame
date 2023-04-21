using RawDealView;
namespace RawDeal;

class ChrisJerichoAbility : SuperstarAbility
{
    public override void UseEffect(Player playerPlayingRound, Player playerNotPlayingRound, View view)
    {
        List<Player> playersIterable = new List<Player>() { playerPlayingRound, playerNotPlayingRound };
        foreach (Player player in playersIterable)
        {
            List<Card> handCardsObjectsToShow = player.GetCardsToShow(CardSet.Hand);
            List<string> stringsOfHandCards = GetCardsToShowAsString(handCardsObjectsToShow);
            int handCardIndex = view.AskPlayerToSelectACardToDiscard(stringsOfHandCards, player.Superstar.Name, player.Superstar.Name, 1);
            player.MoveCardFromHandToRingside(handCardsObjectsToShow[handCardIndex]);
        }
    }

    private List<string> GetCardsToShowAsString(List<Card> cardsObjectsToShow)
    {
        List<string> stringsOfCards = new List<string>();
        foreach (Card card in cardsObjectsToShow)
        {
            stringsOfCards.Add(card.ToString());
        }
        
        return stringsOfCards;
    }

    public override bool MeetsTheRequirementsForUsingEffect(Player player)
    {
        return player.HasCards(CardSet.Hand);
    }
}