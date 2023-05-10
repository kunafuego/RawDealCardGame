using RawDealView.Options;
using RawDealView;
namespace RawDeal;

class TheUndertakerAbility : SuperstarAbility
{
    public TheUndertakerAbility(View view) : base(view) {}

    public override void UseEffect(Player playerPlayingRound)
    {
        DiscardTwoCards(playerPlayingRound);
        GetCardFromRingSidePile(playerPlayingRound);
    }

    private void DiscardTwoCards(Player playerThatWillDiscardCards)
    {
        for (int numberOfCardsLeftToDiscard = 2; numberOfCardsLeftToDiscard > 0; numberOfCardsLeftToDiscard--)
        {
            DiscardCard(numberOfCardsLeftToDiscard, playerThatWillDiscardCards);
        }
    }

    private void DiscardCard(int numberOfCardDiscarding, Player playerThatWillDiscardCard)
    {
        List<Card> handCardsObjectsToShow = playerThatWillDiscardCard.GetCardsToShow(CardSet.Hand);
        List<string> stringsOfHandCards = GetCardsToShowAsString(handCardsObjectsToShow);
        int handCardIndex = View.AskPlayerToSelectACardToDiscard(stringsOfHandCards, playerThatWillDiscardCard.Superstar.Name, playerThatWillDiscardCard.Superstar.Name, numberOfCardDiscarding);
        playerThatWillDiscardCard.MoveCardFromHandToRingside(handCardsObjectsToShow[handCardIndex]);
    }

    private void GetCardFromRingSidePile(Player playerThatWillGetCardBack)
    {
        List<Card> ringsideCardsObjectsToShow = playerThatWillGetCardBack.GetCardsToShow(CardSet.RingsidePile);
        List<string> stringsOfRingsideCards = GetCardsToShowAsString(ringsideCardsObjectsToShow);
        int ringsideCardIndex = View.AskPlayerToSelectCardsToPutInHisHand(playerThatWillGetCardBack.Superstar.Name, 1, stringsOfRingsideCards);
        playerThatWillGetCardBack.MoveCardFromRingsideToHand(ringsideCardsObjectsToShow[ringsideCardIndex]); 
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