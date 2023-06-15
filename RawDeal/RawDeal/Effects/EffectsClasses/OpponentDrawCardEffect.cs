using RawDealView;
namespace RawDeal.Effects;

public class OpponentDrawCardEffect : Effect
{
    private int _amountOfCardsToDrawInEffect;
    private View _view;
    public OpponentDrawCardEffect(int amountOfCardsToDrawInEffect, View view)
    { 
        _amountOfCardsToDrawInEffect = amountOfCardsToDrawInEffect;
        _view = view;
    }
    
    public override void Apply(Play actualPlay, Player playerThatPlayedCard, Player opponent)
    {
        Effect drawCardEffect = new DrawCardEffect(_amountOfCardsToDrawInEffect, _view);
        drawCardEffect.Apply(actualPlay, opponent, playerThatPlayedCard);
    }
}