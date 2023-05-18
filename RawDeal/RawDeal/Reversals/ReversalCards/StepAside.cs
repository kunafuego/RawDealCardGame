using RawDealView;
namespace RawDeal.ReversalCards;

public class StepAside: ReversalCard
{
    public StepAside(View view) : base(view) {}
    public override void PerformEffect(Play playThatIsBeingReversed,  Card cardObject, Player playerThatReversePlay, Player playerThatWasReversed)
    {
        if (playThatIsBeingReversed.PlayedAs == "Reversed From Hand")
        {
            playerThatReversePlay.MoveCardFromHandToRingArea(cardObject);
        }
        else if (playThatIsBeingReversed.PlayedAs == "REVERSED FROM DECK")
        {
            playerThatReversePlay.MoveArsenalTopCardToRingside();
        }
    }

    public override bool CheckIfCanReversePlay(Play playThatIsBeingPlayed, string askedFromDeskOrHand, int netDamageThatWillReceive)
    {
        Card cardThatIsBeingPlayed = playThatIsBeingPlayed.Card;
        if (playThatIsBeingPlayed.PlayedAs == "MANEUVER" && cardThatIsBeingPlayed.CheckIfSubtypesContain("Strike"))
        {
            return true;
        }

        return false;
    }
}