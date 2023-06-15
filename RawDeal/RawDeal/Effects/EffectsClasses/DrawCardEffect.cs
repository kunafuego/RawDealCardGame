using RawDealView;

namespace RawDeal.Effects;

public class DrawCardEffect : Effect
{
    private readonly int _amountOfCardsToDrawInEffect;
    private readonly View _view;

    public DrawCardEffect(int amountOfCardsToDrawInEffect, View view)
    {
        _amountOfCardsToDrawInEffect = amountOfCardsToDrawInEffect;
        _view = view;
    }

    public override void Apply(Play actualPlay, Player playerThatPlayedCard, Player opponent)
    {
        _view.SayThatPlayerDrawCards(playerThatPlayedCard.GetSuperstarName(),
            _amountOfCardsToDrawInEffect);
        for (var i = 0; i < _amountOfCardsToDrawInEffect; i++)
            playerThatPlayedCard.DrawSingleCard();
    }
}