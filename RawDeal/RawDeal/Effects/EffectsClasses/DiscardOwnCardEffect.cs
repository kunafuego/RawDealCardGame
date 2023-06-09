using RawDealView;
using RawDealView.Options;
namespace RawDeal.Effects;

public class DiscardOwnCardEffect : Effect
{
    private readonly int _amountOfCardsToDiscardInEffect;
    
    public DiscardOwnCardEffect(int amountOfCardsToDiscardInEffect)
    { 
        _amountOfCardsToDiscardInEffect = amountOfCardsToDiscardInEffect; 
    }    
    public override void Apply(Play playThatIsBeingReversedOrPlayed, View view, Player playerThatPlayedCard, Player opponent)
    {
        for (int i = _amountOfCardsToDiscardInEffect; i > 0; i--)
        {
            DiscardCard(i, playerThatPlayedCard, view, playThatIsBeingReversedOrPlayed.Card);
        }
    }
    
    private void DiscardCard(int amountOfCardsLeftToDiscard, Player playerThatHasToDiscard, View view, Card cardThatIsBeingPlayed)
    {
        List<Card> handCardsObjectsToShow = playerThatHasToDiscard.GetCardsToShow(CardSet.Hand);
        handCardsObjectsToShow.Remove(cardThatIsBeingPlayed);
        List<string> stringsOfHandCards = GetCardsToShowAsString(handCardsObjectsToShow);
        if (!stringsOfHandCards.Any()) return;
        int handCardIndex = view.AskPlayerToSelectACardToDiscard(stringsOfHandCards, playerThatHasToDiscard.GetSuperstarName(), playerThatHasToDiscard.GetSuperstarName(), amountOfCardsLeftToDiscard);
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
}