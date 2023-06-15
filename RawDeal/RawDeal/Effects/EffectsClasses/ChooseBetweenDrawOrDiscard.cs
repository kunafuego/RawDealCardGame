using RawDeal;
using RawDeal.Effects;
using RawDealView;
using RawDealView.Options;

public class ChooseBetweenDrawOrDiscard : Effect
{
    private readonly int _amountOfCardsToDiscardOrDrawInEffect;
    private readonly View _view;

    public ChooseBetweenDrawOrDiscard(int amountOfCardsToDiscardOrDrawInEffect, View view)
    {
        _amountOfCardsToDiscardOrDrawInEffect = amountOfCardsToDiscardOrDrawInEffect;
        _view = view;
    }

    public override void Apply(Play playThatIsBeingPlayed, Player playerThatPlayedCard,
        Player opponent)
    {
        var cardBeingPlayed = playThatIsBeingPlayed.Card;
        var chosenOption =
            _view.AskUserToChooseBetweenDrawingOrForcingOpponentToDiscardCards(playerThatPlayedCard
                .GetSuperstarName());
        if (chosenOption == SelectedEffect.DrawCards)
        {
            if (cardBeingPlayed.Title == "Y2J")
            {
                var mayDrawCardsEffect =
                    new MayDrawCardsEffect(_amountOfCardsToDiscardOrDrawInEffect, _view);
                mayDrawCardsEffect.Apply(playThatIsBeingPlayed, playerThatPlayedCard, opponent);
            }
            else
            {
                var drawEffect = new DrawCardEffect(_amountOfCardsToDiscardOrDrawInEffect, _view);
                drawEffect.Apply(playThatIsBeingPlayed, playerThatPlayedCard, opponent);
            }
        }
        else if (chosenOption == SelectedEffect.ForceOpponentToDiscard)
        {
            var discardEffect =
                new OpponentDiscardCardEffect(_amountOfCardsToDiscardOrDrawInEffect, _view);
            discardEffect.Apply(playThatIsBeingPlayed, playerThatPlayedCard, opponent);
        }
    }
}