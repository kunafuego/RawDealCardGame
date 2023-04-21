using RawDealView;
namespace RawDeal;

class TheUndertakerAbility : SuperstarAbility
{
    public TheUndertakerAbility(View view) : base(view) {}

    public override void UseEffect(Player playerPlayingRound)
    {
        for (int i = 2; i > 0; i--)
        {
            List<Card> handCardsObjectsToShow = playerPlayingRound.GetCardsToShow(CardSet.Hand);
            List<string> stringsOfHandCards = GetCardsToShowAsString(handCardsObjectsToShow);
            int handCardIndex = View.AskPlayerToSelectACardToDiscard(stringsOfHandCards, playerPlayingRound.Superstar.Name, playerPlayingRound.Superstar.Name, i);
            playerPlayingRound.MoveCardFromHandToRingside(handCardsObjectsToShow[handCardIndex]);
        }
        
        List<Card> ringsideCardsObjectsToShow = playerPlayingRound.GetCardsToShow(CardSet.RingsidePile);
        List<string> stringsOfRingsideCards = GetCardsToShowAsString(ringsideCardsObjectsToShow);
        int ringsideCardIndex = View.AskPlayerToSelectCardsToPutInHisHand(playerPlayingRound.Superstar.Name, 1, stringsOfRingsideCards);
        playerPlayingRound.MoveCardFromRingsideToHand(ringsideCardsObjectsToShow[ringsideCardIndex]);    
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
        return player.HasMoreThanOneCard(CardSet.Hand);
    }
}