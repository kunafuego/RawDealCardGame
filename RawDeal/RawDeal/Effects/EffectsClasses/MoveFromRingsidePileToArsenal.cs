using RawDealView;
using RawDealView.Options;

namespace RawDeal.Effects.EffectsClasses;

public class MoveFromRingsidePileToArsenal : Effect
{
    private readonly int _numberOfCardsToRecover;
    private readonly View _view;

    public MoveFromRingsidePileToArsenal(int numberOfCardsToRecover, View view)
    {
        _numberOfCardsToRecover = numberOfCardsToRecover;
        _view = view;
    }

    public override void Apply(Play actualPlay, Player playerThatPlayedCard, Player opponent)
    {
        for (var i = _numberOfCardsToRecover; i > 0; i--)
        {
            var cardsObjectsToShow = playerThatPlayedCard.GetCardsToShow(CardSet.RingsidePile);
            var stringsOfRingsidePileCards = GetCardsToShowAsString(cardsObjectsToShow);
            var cardIndex = _view.AskPlayerToSelectCardsToRecover(
                playerThatPlayedCard.GetSuperstarName(), i,
                stringsOfRingsidePileCards);
            playerThatPlayedCard.MoveCardFromRingsideToArsenal(cardsObjectsToShow[cardIndex]);
        }
    }

    private List<string> GetCardsToShowAsString(List<Card> cardsObjectsToShow)
    {
        var stringsOfRingsidePileCards = new List<string>();
        foreach (var card in cardsObjectsToShow) stringsOfRingsidePileCards.Add(card.ToString());

        return stringsOfRingsidePileCards;
    }
}