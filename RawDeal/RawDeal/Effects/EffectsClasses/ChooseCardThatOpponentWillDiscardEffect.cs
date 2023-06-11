using RawDealView;
using RawDealView.Options;
namespace RawDeal.Effects;

public class ChooseCardThatOpponentWillDiscardEffect : Effect
{
    private readonly int _amountOfCardsToDiscardInEffect;
    
    public ChooseCardThatOpponentWillDiscardEffect(int amountOfCardsToDiscardInEffect)
    { 
        _amountOfCardsToDiscardInEffect = amountOfCardsToDiscardInEffect; 
    }    
    public override void Apply(Play actualPlay, View view, Player playerThatPlayedCard, Player opponent)
    {
        for (int i = _amountOfCardsToDiscardInEffect; i > 0; i--)
        {
            DiscardOpponentsCard(i, playerThatPlayedCard, opponent, view);
        }
    }
    
    private void DiscardOpponentsCard(int amountOfCardsLeftToDiscard, Player playerThatWillChooseCard, Player playerThatHasToDiscard, View view)
    {
        List<Card> handCardsObjectsToShow = playerThatHasToDiscard.GetCardsToShow(CardSet.Hand);
        List<string> stringsOfHandCards = GetCardsToShowAsString(handCardsObjectsToShow);
        if (!stringsOfHandCards.Any()) return;
        int handCardIndex = view.AskPlayerToSelectACardToDiscard(stringsOfHandCards, playerThatHasToDiscard.GetSuperstarName(), playerThatWillChooseCard.GetSuperstarName(), amountOfCardsLeftToDiscard);
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