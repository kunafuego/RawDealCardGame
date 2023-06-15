using RawDealView;
using RawDealView.Options;

namespace RawDeal.Effects.EffectsClasses;

public class DiscardCardsAndReturnToHand : Effect
{
    private const int AmountOfCardsToDiscardInEffect = 2;
    private View _view;
    public DiscardCardsAndReturnToHand(View view)
    {
        _view = view;
    }

    public override void Apply(Play playThatIsBeingPlayed, Player playerThatPlayedCard, Player opponent)
    {
        int amountOfCards = DiscardUpToCards(playerThatPlayedCard);
        if (amountOfCards != 0)
        {
            DiscardOwnCardEffect effect = new DiscardOwnCardEffect(amountOfCards, _view);
            effect.Apply(playThatIsBeingPlayed, playerThatPlayedCard, opponent);
            GetCardsFromRingSidePile(playerThatPlayedCard, amountOfCards);
        }
    }
    
    private int DiscardUpToCards(Player playerThatWillDiscardCards)
    {
        List<Card> handCardsObjectsToShow = playerThatWillDiscardCards.GetCardsToShow(CardSet.Hand);
        if(!handCardsObjectsToShow.Any()) return 0;
        int maxCardsToDiscard = Math.Min(handCardsObjectsToShow.Count() - 1, AmountOfCardsToDiscardInEffect);
        int amountChosen =
            _view.AskHowManyCardsToDiscard(playerThatWillDiscardCards.GetSuperstarName(), maxCardsToDiscard);
        return amountChosen;
    }

    private void GetCardsFromRingSidePile(Player playerThatWillGetCardBack, int amountOfCards)
    {
        for (int i = amountOfCards; i > 0; i--)
        {
            List<Card> ringsideCardsObjectsToShow = playerThatWillGetCardBack.GetCardsToShow(CardSet.RingsidePile);
            List<string> stringsOfRingsideCards = GetCardsToShowAsString(ringsideCardsObjectsToShow);
            int ringsideCardIndex = _view.AskPlayerToSelectCardsToPutInHisHand(playerThatWillGetCardBack.GetSuperstarName(), i, stringsOfRingsideCards);
            playerThatWillGetCardBack.MoveCardFromRingsideToHand(ringsideCardsObjectsToShow[ringsideCardIndex]);
        }
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