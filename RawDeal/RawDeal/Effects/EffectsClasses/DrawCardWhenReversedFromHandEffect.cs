using RawDealView;
namespace RawDeal.Effects.EffectsClasses;

public class DrawCardWhenReversedFromHandEffect : Effect
{
    private int _amountOfCardsToDrawInEffect;
    
    public DrawCardWhenReversedFromHandEffect(int amountOfCardsToDrawInEffect)
    { 
        _amountOfCardsToDrawInEffect = amountOfCardsToDrawInEffect; 
    }

    public override void Apply(Play actualPlay, View view, Player playerThatPlayedCard, Player opponent)
    {
        if (actualPlay.PlayedAs != "REVERSAL HAND") return;
        view.SayThatPlayerDrawCards(playerThatPlayedCard.GetSuperstarName(), _amountOfCardsToDrawInEffect);
        for (int i = 0; i < _amountOfCardsToDrawInEffect; i++)
        {
            playerThatPlayedCard.DrawSingleCard();
        }
    }
}