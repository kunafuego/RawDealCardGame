using RawDealView;
namespace RawDeal.Effects.EffectsClasses;

public class DrawCardWhenReversedFromHandEffect : Effect
{
    private int _amountOfCardsToDrawInEffect;
    private View _view;
    
    public DrawCardWhenReversedFromHandEffect(int amountOfCardsToDrawInEffect, View view)
    { 
        _amountOfCardsToDrawInEffect = amountOfCardsToDrawInEffect;
        _view = view;
    }

    public override void Apply(Play actualPlay, Player playerThatPlayedCard, Player opponent)
    {
        if (actualPlay.PlayedAs != "REVERSAL HAND") return;
        _view.SayThatPlayerDrawCards(playerThatPlayedCard.GetSuperstarName(), _amountOfCardsToDrawInEffect);
        for (int i = 0; i < _amountOfCardsToDrawInEffect; i++)
        {
            playerThatPlayedCard.DrawSingleCard();
        }
    }
}