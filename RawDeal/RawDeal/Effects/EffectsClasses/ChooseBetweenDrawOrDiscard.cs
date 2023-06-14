using RawDeal;
using RawDeal.Effects;
using RawDealView;
using RawDealView.Options;


public class ChooseBetweenDrawOrDiscard : Effect
{
    private readonly int _amountOfCardsToDiscardOrDrawInEffect;
    
    public ChooseBetweenDrawOrDiscard(int amountOfCardsToDiscardOrDrawInEffect)
    { 
        _amountOfCardsToDiscardOrDrawInEffect = amountOfCardsToDiscardOrDrawInEffect; 
    }    
    public override void Apply(Play playThatIsBeingPlayed, View view, Player playerThatPlayedCard, Player opponent)
    {
        Card cardBeingPlayed = playThatIsBeingPlayed.Card;
        SelectedEffect chosenOption =
            view.AskUserToChooseBetweenDrawingOrForcingOpponentToDiscardCards(playerThatPlayedCard.GetSuperstarName());
        if (chosenOption == SelectedEffect.DrawCards)
        {
            if (cardBeingPlayed.Title == "Y2J")
            {
                MayDrawCardsEffect mayDrawCardsEffect = new MayDrawCardsEffect(_amountOfCardsToDiscardOrDrawInEffect);
                mayDrawCardsEffect.Apply(playThatIsBeingPlayed, view, playerThatPlayedCard, opponent);    
            }
            else
            {
                DrawCardEffect drawEffect = new DrawCardEffect(_amountOfCardsToDiscardOrDrawInEffect);
                drawEffect.Apply(playThatIsBeingPlayed, view, playerThatPlayedCard, opponent);
            }
        }
        else if (chosenOption == SelectedEffect.ForceOpponentToDiscard)
        {
            OpponentDiscardCardEffect discardEffect = new OpponentDiscardCardEffect(_amountOfCardsToDiscardOrDrawInEffect);
            discardEffect.Apply(playThatIsBeingPlayed, view, playerThatPlayedCard, opponent);
        }
    }
}