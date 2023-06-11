using RawDealView;
namespace RawDeal.Effects;

public class DrawCardEffect : Effect
{
    private int _amountOfCardsToDrawInEffect;
    
    public DrawCardEffect(int amountOfCardsToDrawInEffect)
    { 
        _amountOfCardsToDrawInEffect = amountOfCardsToDrawInEffect; 
    }
    
    public override void Apply(Play actualPlay, View view, Player playerThatPlayedCard, Player opponent)
    {
        view.SayThatPlayerDrawCards(playerThatPlayedCard.GetSuperstarName(), _amountOfCardsToDrawInEffect);
        for (int i = 0; i < _amountOfCardsToDrawInEffect; i++)
        {
            playerThatPlayedCard.DrawSingleCard();
        }
    }
}