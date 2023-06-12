using RawDealView;
using RawDealView.Formatters;
using RawDealView.Options;

namespace RawDeal.Effects.EffectsClasses;

public class MoveFromRingsidePileToArsenal : Effect
{
    private readonly int _numberOfCardsToRecover;
    
    public MoveFromRingsidePileToArsenal(int numberOfCardsToRecover)
    {
        _numberOfCardsToRecover = numberOfCardsToRecover;
    }
    public override void Apply(Play actualPlay, View view, Player playerThatPlayedCard, Player opponent)
    {
        for (int i = _numberOfCardsToRecover; i > 0; i--)
        {
            List<Card> cardsObjectsToShow = playerThatPlayedCard.GetCardsToShow(CardSet.RingsidePile);
            List<string> stringsOfRingsidePileCards = GetCardsToShowAsString(cardsObjectsToShow);
            int cardIndex = view.AskPlayerToSelectCardsToRecover(playerThatPlayedCard.GetSuperstarName(), i,
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