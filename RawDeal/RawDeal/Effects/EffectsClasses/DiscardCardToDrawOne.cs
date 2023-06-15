using RawDeal;
using RawDeal.Effects;
using RawDealView;

public class DiscardCardToDrawOne : Effect
{
    private readonly View _view;

    public DiscardCardToDrawOne(View view)
    {
        _view = view;
    }

    public override void Apply(Play playThatIsBeingPlayed, Player playerThatPlayedCard,
        Player opponent)
    {
        if (playThatIsBeingPlayed.PlayedAs != "ACTION") return;
        var cardPlayed = playThatIsBeingPlayed.Card;
        playerThatPlayedCard.DrawSingleCard();
        _view.SayThatPlayerMustDiscardThisCard(playerThatPlayedCard.GetSuperstarName(),
            cardPlayed.Title);
        _view.SayThatPlayerDrawCards(playerThatPlayedCard.GetSuperstarName(), 1);
        playerThatPlayedCard.MoveCardFromHandToRingside(cardPlayed);
    }
}