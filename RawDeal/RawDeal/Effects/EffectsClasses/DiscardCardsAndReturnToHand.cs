using RawDealView;
using RawDealView.Options;

namespace RawDeal.Effects.EffectsClasses;

public class DiscardCardsAndReturnToHand : Effect
{
    private const int AmountOfCardsToDiscardInEffect = 2;
    private readonly View _view;

    public DiscardCardsAndReturnToHand(View view)
    {
        _view = view;
    }

    public override void Apply(Play playThatIsBeingPlayed, Player playerThatPlayedCard,
        Player opponent)
    {
        var amountOfCards = DiscardUpToCards(playerThatPlayedCard);
        if (amountOfCards != 0)
        {
            var effect = new DiscardOwnCardEffect(amountOfCards, _view);
            effect.Apply(playThatIsBeingPlayed, playerThatPlayedCard, opponent);
            GetCardsFromRingSidePile(playerThatPlayedCard, amountOfCards);
        }
    }

    private int DiscardUpToCards(Player playerThatWillDiscardCards)
    {
        var handCardsObjectsToShow = playerThatWillDiscardCards.GetCardsToShow(CardSet.Hand);
        if (!handCardsObjectsToShow.Any()) return 0;
        var maxCardsToDiscard =
            Math.Min(handCardsObjectsToShow.Count() - 1, AmountOfCardsToDiscardInEffect);
        var amountChosen =
            _view.AskHowManyCardsToDiscard(playerThatWillDiscardCards.GetSuperstarName(),
                maxCardsToDiscard);
        return amountChosen;
    }

    private void GetCardsFromRingSidePile(Player playerThatWillGetCardBack, int amountOfCards)
    {
        for (var i = amountOfCards; i > 0; i--)
        {
            var ringsideCardsObjectsToShow =
                playerThatWillGetCardBack.GetCardsToShow(CardSet.RingsidePile);
            var stringsOfRingsideCards = GetCardsToShowAsString(ringsideCardsObjectsToShow);
            var ringsideCardIndex = _view.AskPlayerToSelectCardsToPutInHisHand(
                playerThatWillGetCardBack.GetSuperstarName(), i, stringsOfRingsideCards);
            playerThatWillGetCardBack.MoveCardFromRingsideToHand(
                ringsideCardsObjectsToShow[ringsideCardIndex]);
        }
    }

    private List<string> GetCardsToShowAsString(List<Card> cardsObjectsToShow)
    {
        var stringsOfCards = new List<string>();
        foreach (var card in cardsObjectsToShow) stringsOfCards.Add(card.ToString());

        return stringsOfCards;
    }
}