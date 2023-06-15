using RawDealView;
using RawDealView.Options;
namespace RawDeal.Effects;

public class DiscardOwnCardEffect : Effect
{
    private readonly int _amountOfCardsToDiscardInEffect;
    private View _view;
    
    public DiscardOwnCardEffect(int amountOfCardsToDiscardInEffect, View view)
    { 
        _amountOfCardsToDiscardInEffect = amountOfCardsToDiscardInEffect;
        _view = view;
    }    
    public override void Apply(Play playThatIsBeingPlayed, Player playerThatPlayedCard, Player opponent)
    {
        for (int i = _amountOfCardsToDiscardInEffect; i > 0; i--)
        {
            DiscardCard(i, playerThatPlayedCard, playThatIsBeingPlayed.Card);
        }
    }
    
    private void DiscardCard(int amountOfCardsLeftToDiscard, Player playerThatHasToDiscard, Card cardThatIsBeingPlayed)
    {
        List<Card> handCardsObjectsToShow = playerThatHasToDiscard.GetCardsToShow(CardSet.Hand);
        handCardsObjectsToShow.Remove(cardThatIsBeingPlayed);
        List<string> stringsOfHandCards = GetCardsToShowAsString(handCardsObjectsToShow);
        if (!stringsOfHandCards.Any()) return;
        int handCardIndex = _view.AskPlayerToSelectACardToDiscard(stringsOfHandCards, playerThatHasToDiscard.GetSuperstarName(), playerThatHasToDiscard.GetSuperstarName(), amountOfCardsLeftToDiscard);
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