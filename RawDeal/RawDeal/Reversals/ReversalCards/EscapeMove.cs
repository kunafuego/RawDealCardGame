using RawDealView;
namespace RawDeal.ReversalCards;

public class EscapeMove: ReversalCard
{
    public EscapeMove(View view) : base(view) {}

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
        if (playThatIsBeingPlayed.PlayedAs == "MANEUVER" && cardThatIsBeingPlayed.CheckIfSubtypesContain("Grapple"))
        {
            return true;
        }

        return false;
    }
}