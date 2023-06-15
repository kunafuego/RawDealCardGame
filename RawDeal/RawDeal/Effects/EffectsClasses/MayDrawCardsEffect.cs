using RawDealView;
namespace RawDeal.Effects;

public class MayDrawCardsEffect : Effect
{
    private int _maxAmountOfCardsToDrawInEffect;
    private View _view;
    public MayDrawCardsEffect(int amountOfCardsToDrawInEffect, View view)
    { 
        _maxAmountOfCardsToDrawInEffect = amountOfCardsToDrawInEffect;
        _view = view;
    }
    
    public override void Apply(Play actualPlay, Player playerThatPlayedCard, Player opponent)
    {
        int amountOfCardsToDiscard = _view.AskHowManyCardsToDrawBecauseOfACardEffect(playerThatPlayedCard.GetSuperstarName(), 
            _maxAmountOfCardsToDrawInEffect);
        Effect drawCardEffect = new DrawCardEffect(amountOfCardsToDiscard, _view);
        drawCardEffect.Apply(actualPlay, playerThatPlayedCard, opponent);
    }
}