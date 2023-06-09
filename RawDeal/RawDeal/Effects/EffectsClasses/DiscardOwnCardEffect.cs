using RawDealView;
using RawDealView.Options;

namespace RawDeal.Effects;

public class DiscardOwnCardEffect : Effect
{
    private readonly int _amountOfCardsToDiscardInEffect;
    private readonly View _view;

    public DiscardOwnCardEffect(int amountOfCardsToDiscardInEffect, View view)
    {
        _amountOfCardsToDiscardInEffect = amountOfCardsToDiscardInEffect;
        _view = view;
    }

    public override void Apply(Play playThatIsBeingPlayed, Player playerThatPlayedCard,
        Player opponent)
    {
        for (var i = _amountOfCardsToDiscardInEffect; i > 0; i--)
            DiscardCard(i, playerThatPlayedCard, playThatIsBeingPlayed.Card);
    }

    private void DiscardCard(int amountOfCardsLeftToDiscard, Player playerThatHasToDiscard,
        Card cardThatIsBeingPlayed)
    {
        var handCardsObjectsToShow = playerThatHasToDiscard.GetCardsToShow(CardSet.Hand);
        handCardsObjectsToShow.Remove(cardThatIsBeingPlayed);
        var stringsOfHandCards = GetCardsToShowAsString(handCardsObjectsToShow);
        if (!stringsOfHandCards.Any()) return;
        var handCardIndex = _view.AskPlayerToSelectACardToDiscard(stringsOfHandCards,
            playerThatHasToDiscard.GetSuperstarName(), playerThatHasToDiscard.GetSuperstarName(),
            amountOfCardsLeftToDiscard);
        playerThatHasToDiscard.MoveCardFromHandToRingside(handCardsObjectsToShow[handCardIndex]);
    }

    private List<string> GetCardsToShowAsString(CardsList cardsObjectsToShow)
    {
        var stringsOfCards = new List<string>();
        foreach (var card in cardsObjectsToShow) stringsOfCards.Add(card.ToString());

        return stringsOfCards;
    }
}