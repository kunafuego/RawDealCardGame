using RawDealView;
namespace RawDeal.Effects;

public class MayDrawCardsEffect : Effect
{
    private int _maxAmountOfCardsToDrawInEffect;
    
    public MayDrawCardsEffect(int amountOfCardsToDrawInEffect)
    { 
        _maxAmountOfCardsToDrawInEffect = amountOfCardsToDrawInEffect; 
    }
    
    public override void Apply(Play actualPlay, View view, Player playerThatPlayedCard, Player opponent)
    {
        int amountOfCardsToDiscard = view.AskHowManyCardsToDrawBecauseOfACardEffect(playerThatPlayedCard.GetSuperstarName(), 
            _maxAmountOfCardsToDrawInEffect);
        Effect drawCardEffect = new DrawCardEffect(amountOfCardsToDiscard);
        drawCardEffect.Apply(actualPlay, view, playerThatPlayedCard, opponent);
    }
}