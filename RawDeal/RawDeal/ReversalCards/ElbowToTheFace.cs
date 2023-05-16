using RawDealView;
namespace RawDeal.ReversalCards;

public class ElbowToTheFace: ReversalCard
{
    public ElbowToTheFace(View view) : base(view) {}

    public override void PerformEffect(Play playThatIsBeingReversed,  Card cardObject, Player playerThatReversePlay, Player playerThatWasReversed)
    {
        if (playThatIsBeingReversed.PlayedAs == "Reversed From Hand")
        {
            ManeuverPlayer maneuverPlayer = new ManeuverPlayer(View, playerThatReversePlay, playerThatWasReversed, new EffectForNextMove(0,0));
            maneuverPlayer.PlayReversalAsManeuver(cardObject);
        }
        else
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
    }
    
    public override bool CheckIfCanReversePlay(Play playThatIsBeingPlayed, string askedFromDeskOrHand, int netDamageThatWillReceive)
    {        
        if (netDamageThatWillReceive <= 7 && playThatIsBeingPlayed.PlayedAs == "MANEUVER")
        {
            return true;
        }
        return false;
    }
}