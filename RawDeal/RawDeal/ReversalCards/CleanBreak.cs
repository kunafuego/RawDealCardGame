using RawDealView;
using RawDealView.Options;
namespace RawDeal.ReversalCards;

public class CleanBreak: ReversalCard
{
    private readonly int _amountOfCardsToDrawInEffect;
    private readonly int _amountOfCardsToDiscardInEffect;
    public CleanBreak(View view) : base(view)
    {
        _amountOfCardsToDrawInEffect = 1;
        _amountOfCardsToDiscardInEffect = 4;
    }

    public override void PerformEffect(Play playThatWasReversed,  Card cardObject, Player playerThatReversePlay, Player playerThatWasReversed)
    {
        for (int i = _amountOfCardsToDiscardInEffect; i > 0; i--)
        {
            DiscardCard(i, playerThatWasReversed);
        }
        View.SayThatPlayerDrawCards(playerThatReversePlay.GetSuperstarName(), _amountOfCardsToDrawInEffect);
        for (int i = 0; i < _amountOfCardsToDrawInEffect; i++)
        {
            playerThatReversePlay.DrawSingleCard();
        }
        playerThatReversePlay.MoveCardFromHandToRingArea(cardObject);
    }
    
    private void DiscardCard(int amountOfCardsLeftToDiscard, Player playerThatHasToDiscard)
    {
        List<Card> handCardsObjectsToShow = playerThatHasToDiscard.GetCardsToShow(CardSet.Hand);
        List<string> stringsOfHandCards = GetCardsToShowAsString(handCardsObjectsToShow);
        int handCardIndex = View.AskPlayerToSelectACardToDiscard(stringsOfHandCards, playerThatHasToDiscard.GetSuperstarName(), playerThatHasToDiscard.Superstar.Name, amountOfCardsLeftToDiscard);
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
    
    public override bool CheckIfCanReversePlay(Play playThatIsBeingPlayed, string askedFromDeskOrHand, int netDamageThatWillReceive)
    {
        Card cardThatIsBeingPlayed = playThatIsBeingPlayed.Card;
        if(askedFromDeskOrHand == "Hand") return cardThatIsBeingPlayed.Title == "Jockeying for Position";
        return false;
    }
}