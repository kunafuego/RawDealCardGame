using RawDealView;
namespace RawDeal.Effects.EffectsClasses;

public class DrawCardWhenPlayFromHandEffect : Effect
{
    private int _amountOfCardsToDrawInEffect;
    
    public DrawCardWhenPlayFromHandEffect(int amountOfCardsToDrawInEffect)
    { 
        _amountOfCardsToDrawInEffect = amountOfCardsToDrawInEffect; 
    }

    public override void Apply(Play playThatIsBeingReversed, View view, Player playerBeingReversed, Player playerThatReversePlay)
    {
        if (playThatIsBeingReversed.PlayedAs != "Reversed From Hand") return;
        view.SayThatPlayerDrawCards(playerThatReversePlay.GetSuperstarName(), _amountOfCardsToDrawInEffect);
        playerThatReversePlay.DrawSingleCard();
    }
}