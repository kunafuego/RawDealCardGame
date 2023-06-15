using RawDealView;
using RawDealView.Options;

namespace RawDeal;

internal class TheUndertakerAbility : SuperstarAbility
{
    private const int AmountOfCardsToDiscard = 2;

    public TheUndertakerAbility(View view) : base(view)
    {
    }

    public override void UseAbility(Player playerPlayingRound, Player playerNotPlayingRound)
    {
        DiscardTwoCards(playerPlayingRound);
        GetCardFromRingSidePile(playerPlayingRound);
    }

    private void DiscardTwoCards(Player playerThatWillDiscardCards)
    {
        for (var numberOfCardsLeftToDiscard = AmountOfCardsToDiscard;
             numberOfCardsLeftToDiscard > 0;
             numberOfCardsLeftToDiscard--)
            DiscardCard(numberOfCardsLeftToDiscard, playerThatWillDiscardCards);
    }

    private void DiscardCard(int numberOfCardDiscarding, Player playerThatWillDiscardCard)
    {
        var handCardsObjectsToShow = playerThatWillDiscardCard.GetCardsToShow(CardSet.Hand);
        var stringsOfHandCards = GetCardsToShowAsString(handCardsObjectsToShow);
        var handCardIndex = View.AskPlayerToSelectACardToDiscard(stringsOfHandCards,
            playerThatWillDiscardCard.GetSuperstarName(),
            playerThatWillDiscardCard.GetSuperstarName(),
            numberOfCardDiscarding);
        playerThatWillDiscardCard.MoveCardFromHandToRingside(handCardsObjectsToShow[handCardIndex]);
    }

    private void GetCardFromRingSidePile(Player playerThatWillGetCardBack)
    {
        var ringsideCardsObjectsToShow =
            playerThatWillGetCardBack.GetCardsToShow(CardSet.RingsidePile);
        var stringsOfRingsideCards = GetCardsToShowAsString(ringsideCardsObjectsToShow);
        var ringsideCardIndex =
            View.AskPlayerToSelectCardsToPutInHisHand(playerThatWillGetCardBack.GetSuperstarName(),
                1, stringsOfRingsideCards);
        playerThatWillGetCardBack.MoveCardFromRingsideToHand(
            ringsideCardsObjectsToShow[ringsideCardIndex]);
    }

    private List<string> GetCardsToShowAsString(CardsList cardsObjectsToShow)
    {
        var stringsOfCards = new List<string>();
        foreach (var card in cardsObjectsToShow) stringsOfCards.Add(card.ToString());

        return stringsOfCards;
    }

    public override bool MeetsTheRequirementsForUsingEffect(Player player)
    {
        return player.CheckIfPlayerHasMoreThanOneCard(CardSet.Hand);
    }
}