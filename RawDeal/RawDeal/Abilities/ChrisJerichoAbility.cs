using RawDealView;
using RawDealView.Options;

namespace RawDeal;

internal class ChrisJerichoAbility : SuperstarAbility
{
    public ChrisJerichoAbility(View view) : base(view)
    {
    }

    public override void UseAbility(Player playerPlayingRound, Player playerNotPlayingRound)
    {
        DiscardCard(playerPlayingRound);
        DiscardCard(playerNotPlayingRound);
    }

    private void DiscardCard(Player playerThatHasToDiscard)
    {
        var handCardsObjectsToShow = playerThatHasToDiscard.GetCardsToShow(CardSet.Hand);
        var stringsOfHandCards = GetCardsToShowAsString(handCardsObjectsToShow);
        var handCardIndex = View.AskPlayerToSelectACardToDiscard(stringsOfHandCards,
            playerThatHasToDiscard.GetSuperstarName(),
            playerThatHasToDiscard.GetSuperstarName(), 1);
        playerThatHasToDiscard.MoveCardFromHandToRingside(handCardsObjectsToShow[handCardIndex]);
    }

    private List<string> GetCardsToShowAsString(CardsList cardsObjectsToShow)
    {
        var stringsOfCards = new List<string>();
        foreach (var card in cardsObjectsToShow) stringsOfCards.Add(card.ToString());

        return stringsOfCards;
    }

    public override bool MeetsTheRequirementsForUsingEffect(Player player)
    {
        return player.HasCards(CardSet.Hand);
    }
}