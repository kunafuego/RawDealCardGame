using RawDealView;
namespace RawDeal.ReversalCards;

public class NoChanceInHell: ReversalCard
{
    public NoChanceInHell(View view) : base(view) {}

    public override void PerformEffect(Play playThatIsBeingReversed,  Card cardObject, Player playerThatReversePlay, Player playerThatWasReversed)
    {
        // Console.WriteLine(playThatIsBeingReversed.PlayedAs);
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
        return playThatIsBeingPlayed.PlayedAs == "ACTION";
    }
}