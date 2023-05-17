using RawDealView;
namespace RawDeal.ReversalCards;

public class RollingTakedown: ReversalCard
{
    public RollingTakedown(View view) : base(view) {}
    
    public override void PerformEffect(Play playThatWasReversed,  Card cardObject, Player playerThatReversePlay, Player playerThatWasReversed)
    {
        Card cardThatWasReversed = playThatWasReversed.Card;
        if (playThatWasReversed.PlayedAs == "REVERSED FROM DECK")
        {
            playerThatReversePlay.MoveArsenalTopCardToRingside();
        }
        int damage = ManageDamage(cardThatWasReversed, playerThatReversePlay);
        ManeuverPlayer maneuverPlayer = new ManeuverPlayer(View, playerThatReversePlay, playerThatWasReversed, new EffectForNextMove(0,0));
        cardObject.ReversalDamage = damage;
        maneuverPlayer.PlayReversalAsManeuver(cardObject);
    }

    private int ManageDamage(Card cardPlayed, Player playerThatReverse)
    {
        int initialDamage = cardPlayed.ReversalDamage;
        if (AbilitiesManager.CheckIfHasAbilityWhenReceivingDamage(playerThatReverse))
        {
            initialDamage -= 1;
        }

        return initialDamage;
    }

    public override bool CheckIfCanReversePlay(Play playThatIsBeingPlayed, string askedFromDeskOrHand, int netDamageThatWillReceive)
    {
        Card cardThatIsBeingPlayed = playThatIsBeingPlayed.Card;
        if (netDamageThatWillReceive <= 7 && playThatIsBeingPlayed.PlayedAs == "MANEUVER" && cardThatIsBeingPlayed.CheckIfSubtypesContain("Grapple"))
        {
            return true;
        }

        return false;
    }
}