using RawDealView.Options;
using RawDealView;
namespace RawDeal;

class ChrisJerichoAbility : SuperstarAbility
{    
    private readonly Player _player1;
    private readonly Player _player2;
    public ChrisJerichoAbility(Player player1, Player player2, View view) : base(view)     
    {
        _player1 = player1;
        _player2 = player2;
    }

    public override void UseEffect(Player playerPlayingRound)
    {
        DiscardCard(playerPlayingRound);
        Player playerNotPlayingRound = GetPlayerNotPlayingRound(playerPlayingRound);
        DiscardCard(playerNotPlayingRound);
    }

    private void DiscardCard(Player playerToDiscard)
    {
        List<Card> handCardsObjectsToShow = playerToDiscard.GetCardsToShow(CardSet.Hand);
        List<string> stringsOfHandCards = GetCardsToShowAsString(handCardsObjectsToShow);
        int handCardIndex = View.AskPlayerToSelectACardToDiscard(stringsOfHandCards, playerToDiscard.Superstar.Name, playerToDiscard.Superstar.Name, 1);
        playerToDiscard.MoveCardFromHandToRingside(handCardsObjectsToShow[handCardIndex]);
    }

    private Player GetPlayerNotPlayingRound(Player playerPlayingRound)
    {
        Player playerNotPlayingRound = (playerPlayingRound == _player1) ? _player2 : _player1;
        return playerNotPlayingRound;
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
        return player.HasCards(CardSet.Hand);
    }
}