using RawDealView;
using RawDealView.Options;
namespace RawDeal.Effects;

public class OpponentDiscardCardEffect : Effect
{
    private int _amountOfCardsToDiscardInEffect;
    
    public OpponentDiscardCardEffect(int amountOfCardsToDiscardInEffect)
    { 
        _amountOfCardsToDiscardInEffect = amountOfCardsToDiscardInEffect; 
    }    
    public override void Apply(Play actualPlay, View view, Player playerThatPlayedCard, Player opponent)
    {
        Effect discardCardEffect = new DiscardOwnCardEffect(_amountOfCardsToDiscardInEffect);
        discardCardEffect.Apply(actualPlay, view, opponent, playerThatPlayedCard);
    }
}