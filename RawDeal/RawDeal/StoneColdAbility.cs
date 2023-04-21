using RawDealView;
namespace RawDeal;

class StoneColdAbility : SuperstarAbility
{
    public override void UseEffect(Player playerPlayingRound, Player playerNotPlayingRound, View view)
    {
        playerPlayingRound.MovesCardFromArsenalToHandInDrawSegment();
        view.SayThatPlayerDrawCards(playerPlayingRound.Superstar.Name, 1);
        
        List<Card> handCardsObjectsToShow = playerPlayingRound.GetCardsToShow(CardSet.Hand);
        List<string> stringsOfHandCards = GetCardsToShowAsString(handCardsObjectsToShow);
        int handCardIndex = view.AskPlayerToReturnOneCardFromHisHandToHisArsenal(playerPlayingRound.Superstar.Name, stringsOfHandCards);
        playerPlayingRound.MoveCardFromHandToArsenal(handCardsObjectsToShow[handCardIndex]);   
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
        return player.HasCardsInArsenal();

    }
}