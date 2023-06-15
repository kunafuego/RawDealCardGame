using RawDealView;
using RawDealView.Options;
namespace RawDeal.Effects;

public class ChooseCardThatOpponentWillDiscardEffect : Effect
{
    private readonly int _amountOfCardsToDiscardInEffect;
    private View _view;
    
    public ChooseCardThatOpponentWillDiscardEffect(int amountOfCardsToDiscardInEffect, View view)
    { 
        _amountOfCardsToDiscardInEffect = amountOfCardsToDiscardInEffect;
        _view = view;
    }    
    public override void Apply(Play actualPlay, Player playerThatPlayedCard, Player opponent)
    {
        for (int i = _amountOfCardsToDiscardInEffect; i > 0; i--)
        {
            DiscardOpponentsCard(i, playerThatPlayedCard, opponent);
        }
    }
    
    private void DiscardOpponentsCard(int amountOfCardsLeftToDiscard, Player playerThatWillChooseCard, Player playerThatHasToDiscard)
    {
        List<Card> handCardsObjectsToShow = playerThatHasToDiscard.GetCardsToShow(CardSet.Hand);
        List<string> stringsOfHandCards = GetCardsToShowAsString(handCardsObjectsToShow);
        if (!stringsOfHandCards.Any()) return;
        int handCardIndex = _view.AskPlayerToSelectACardToDiscard(stringsOfHandCards, playerThatHasToDiscard.GetSuperstarName(), playerThatWillChooseCard.GetSuperstarName(), amountOfCardsLeftToDiscard);
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