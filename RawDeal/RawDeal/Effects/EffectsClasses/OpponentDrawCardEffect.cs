using RawDealView;
namespace RawDeal.Effects;

public class OpponentDrawCardEffect : Effect
{
    private int _amountOfCardsToDrawInEffect;
    
    public OpponentDrawCardEffect(int amountOfCardsToDrawInEffect)
    { 
        _amountOfCardsToDrawInEffect = amountOfCardsToDrawInEffect; 
    }
    
    public override void Apply(Play actualPlay, View view, Player playerThatPlayedCard, Player opponent)
    {
        Effect drawCardEffect = new DrawCardEffect(_amountOfCardsToDrawInEffect);
        drawCardEffect.Apply(actualPlay, view, opponent, playerThatPlayedCard);
    }
}