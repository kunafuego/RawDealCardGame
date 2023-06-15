
using RawDeal;
using RawDeal.Effects;
using RawDealView;
using RawDealView.Options;


public class DiscardCardToDrawOne : Effect
{
    private View _view;
    public DiscardCardToDrawOne(View view)
    {
        _view = view;
    }    
    public override void Apply(Play playThatIsBeingPlayed, Player playerThatPlayedCard, Player opponent)
    {
        if (playThatIsBeingPlayed.PlayedAs != "ACTION") return;
        Card cardPlayed = playThatIsBeingPlayed.Card;
        playerThatPlayedCard.DrawSingleCard();
        _view.SayThatPlayerMustDiscardThisCard(playerThatPlayedCard.GetSuperstarName(), cardPlayed.Title);
        _view.SayThatPlayerDrawCards(playerThatPlayedCard.GetSuperstarName(), 1);
        playerThatPlayedCard.MoveCardFromHandToRingside(cardPlayed);
    }
    
}