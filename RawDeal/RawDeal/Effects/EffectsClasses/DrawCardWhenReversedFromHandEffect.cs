using RawDealView;
namespace RawDeal.Effects.EffectsClasses;

public class DrawCardWhenReversedFromHandEffect : Effect
{
    private int _amountOfCardsToDrawInEffect;
    
    public DrawCardWhenReversedFromHandEffect(int amountOfCardsToDrawInEffect)
    { 
        _amountOfCardsToDrawInEffect = amountOfCardsToDrawInEffect; 
    }

    public override void Apply(Play playThatIsBeingReversed, View view, Player playerThatReversePlay, Player playerThatWasReversed)
    {
        if (playThatIsBeingReversed.PlayedAs != "Reversed From Hand") return;
        view.SayThatPlayerDrawCards(playerThatReversePlay.GetSuperstarName(), _amountOfCardsToDrawInEffect);
        for (int i = 0; i < _amountOfCardsToDrawInEffect; i++)
        {
            playerThatReversePlay.DrawSingleCard();
        }
    }
}