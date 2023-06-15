using RawDealView;
using RawDealView.Formatters;
using RawDealView.Options;

namespace RawDeal.Effects.EffectsClasses;

public class MoveFromRingsidePileToArsenal : Effect
{
    private readonly int _numberOfCardsToRecover;
    private View _view;
    public MoveFromRingsidePileToArsenal(int numberOfCardsToRecover, View view)
    {
        _numberOfCardsToRecover = numberOfCardsToRecover;
        _view = view;
    }
    public override void Apply(Play actualPlay, Player playerThatPlayedCard, Player opponent)
    {
        for (int i = _numberOfCardsToRecover; i > 0; i--)
        {
            List<Card> cardsObjectsToShow = playerThatPlayedCard.GetCardsToShow(CardSet.RingsidePile);
            List<string> stringsOfRingsidePileCards = GetCardsToShowAsString(cardsObjectsToShow);
            int cardIndex = _view.AskPlayerToSelectCardsToRecover(playerThatPlayedCard.GetSuperstarName(), i,
                stringsOfRingsidePileCards);
            playerThatPlayedCard.MoveCardFromRingsideToArsenal(cardsObjectsToShow[cardIndex]);
        }
    }
    
    private List<string> GetCardsToShowAsString(List<Card> cardsObjectsToShow)
    {
        List<string> stringsOfRingsidePileCards = new List<string>();
        foreach (Card card in cardsObjectsToShow)
        {
            stringsOfRingsidePileCards.Add(card.ToString());
        }

        return stringsOfRingsidePileCards;
    }
}