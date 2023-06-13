
using RawDeal;
using RawDeal.Effects;
using RawDealView;
using RawDealView.Options;

public class DiscardCardToDrawOne : Effect
{
    
    public DiscardCardToDrawOne()
    { 
    }    
    public override void Apply(Play playThatIsBeingPlayed, View view, Player playerThatPlayedCard, Player opponent)
    {
        if (playThatIsBeingPlayed.PlayedAs != "ACTION") return;
        Card cardPlayed = playThatIsBeingPlayed.Card;
        playerThatPlayedCard.DrawSingleCard();
        view.SayThatPlayerMustDiscardThisCard(playerThatPlayedCard.GetSuperstarName(), cardPlayed.Title);
        view.SayThatPlayerDrawCards(playerThatPlayedCard.GetSuperstarName(), 1);
        playerThatPlayedCard.MoveCardFromHandToRingside(cardPlayed);
    }
    
}