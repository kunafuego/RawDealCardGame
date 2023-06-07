using RawDealView;
namespace RawDeal.Effects;

public class DrawCardEffect : Effect
{
    private int _amountOfCardsToDrawInEffect;
    
    public DrawCardEffect(int amountOfCardsToDrawInEffect)
    { 
        _amountOfCardsToDrawInEffect = amountOfCardsToDrawInEffect; 
    }
    
    public override void Apply(Play playThatIsBeingReversed, View view, Player playerThatReversePlay, Player playerThatWasReversed)
    {
        view.SayThatPlayerDrawCards(playerThatReversePlay.GetSuperstarName(), _amountOfCardsToDrawInEffect);
        for (int i = 0; i < _amountOfCardsToDrawInEffect; i++)
        {
            playerThatReversePlay.DrawSingleCard();
        }
    }
}