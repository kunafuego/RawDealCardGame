using RawDealView.Options;
using RawDealView;
namespace RawDeal;

class ChrisJerichoAbility : SuperstarAbility
{
    public ChrisJerichoAbility(View view) : base(view) {}

    public override void UseAbility(Player playerPlayingRound, Player playerNotPlayingRound)
    {
        DiscardCard(playerPlayingRound);
        DiscardCard(playerNotPlayingRound);
    }

    private void DiscardCard(Player playerThatHasToDiscard)
    {
        List<Card> handCardsObjectsToShow = playerThatHasToDiscard.GetCardsToShow(CardSet.Hand);
        List<string> stringsOfHandCards = GetCardsToShowAsString(handCardsObjectsToShow);
        int handCardIndex = View.AskPlayerToSelectACardToDiscard(stringsOfHandCards, playerThatHasToDiscard.Superstar.Name, playerThatHasToDiscard.Superstar.Name, 1);
        playerThatHasToDiscard.MoveCardFromHandToRingside(handCardsObjectsToShow[handCardIndex]);
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