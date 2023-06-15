using RawDealView;
using RawDealView.Options;

namespace RawDeal.Effects;

public class ChooseCardThatOpponentWillDiscardEffect : Effect
{
    private readonly int _amountOfCardsToDiscardInEffect;
    private readonly View _view;

    public ChooseCardThatOpponentWillDiscardEffect(int amountOfCardsToDiscardInEffect, View view)
    {
        _amountOfCardsToDiscardInEffect = amountOfCardsToDiscardInEffect;
        _view = view;
    }

    public override void Apply(Play actualPlay, Player playerThatPlayedCard, Player opponent)
    {
        for (var i = _amountOfCardsToDiscardInEffect; i > 0; i--)
            DiscardOpponentsCard(i, playerThatPlayedCard, opponent);
    }

    private void DiscardOpponentsCard(int amountOfCardsLeftToDiscard,
        Player playerThatWillChooseCard, Player playerThatHasToDiscard)
    {
        var handCardsObjectsToShow = playerThatHasToDiscard.GetCardsToShow(CardSet.Hand);
        var stringsOfHandCards = GetCardsToShowAsString(handCardsObjectsToShow);
        if (!stringsOfHandCards.Any()) return;
        var handCardIndex = _view.AskPlayerToSelectACardToDiscard(stringsOfHandCards,
            playerThatHasToDiscard.GetSuperstarName(), playerThatWillChooseCard.GetSuperstarName(),
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