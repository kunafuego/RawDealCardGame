using RawDealView;
using RawDealView.Options;
namespace RawDeal.Effects;

public class OpponentDiscardCardEffect : Effect
{
    private int _amountOfCardsToDiscardInEffect;
    private View _view;
    
    public OpponentDiscardCardEffect(int amountOfCardsToDiscardInEffect, View view)
    { 
        _amountOfCardsToDiscardInEffect = amountOfCardsToDiscardInEffect;
        _view = view;
    }    
    public override void Apply(Play actualPlay, Player playerThatPlayedCard, Player opponent)
    {
        Effect discardCardEffect = new DiscardOwnCardEffect(_amountOfCardsToDiscardInEffect, _view);
        discardCardEffect.Apply(actualPlay, opponent, playerThatPlayedCard);
    }
}