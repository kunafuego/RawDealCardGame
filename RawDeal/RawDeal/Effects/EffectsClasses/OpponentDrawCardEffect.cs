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
        view.SayThatPlayerDrawCards(opponent.GetSuperstarName(), _amountOfCardsToDrawInEffect);
        for (int i = 0; i < _amountOfCardsToDrawInEffect; i++)
        {
            opponent.DrawSingleCard();
        }
    }
}