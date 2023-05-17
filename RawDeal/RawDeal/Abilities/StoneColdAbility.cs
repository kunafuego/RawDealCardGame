using RawDealView.Options;
using RawDealView;
namespace RawDeal;

class StoneColdAbility : SuperstarAbility
{
    public StoneColdAbility(View view) : base(view) {}

    public override void UseAbility(Player playerPlayingRound, Player playerNotPlayingRound)
    {
        DrawCardFromArsenalToHand(playerPlayingRound);
        ChooseAndMoveCardFromHandToArsenal(playerPlayingRound);
    }

    private void DrawCardFromArsenalToHand(Player playerDrawingCard)
    {
        playerDrawingCard.DrawSingleCard();
        View.SayThatPlayerDrawCards(playerDrawingCard.GetSuperstarName(), 1);
    }

    private void ChooseAndMoveCardFromHandToArsenal(Player playerGettingCardBack)
    {
        List<Card> handCardsObjectsToShow = playerGettingCardBack.GetCardsToShow(CardSet.Hand);
        List<string> stringsOfHandCards = GetCardsToShowAsString(handCardsObjectsToShow);
        int handCardIndex = View.AskPlayerToReturnOneCardFromHisHandToHisArsenal(playerGettingCardBack.GetSuperstarName(), stringsOfHandCards);
        playerGettingCardBack.MoveCardFromHandToArsenal(handCardsObjectsToShow[handCardIndex]);   
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
    public override bool MeetsTheRequirementsForUsingEffect(Player player)
    {
        return player.HasCardsInArsenal();

    }
}