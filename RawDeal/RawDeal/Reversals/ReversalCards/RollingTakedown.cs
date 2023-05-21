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
        Console.WriteLine("The damage that the reversal is going to make is " + Convert.ToString(damage));
        // View.SayThatOpponentWillTakeSomeDamage(playerThatWasReversed.GetSuperstarName(), cardThatWasReversed.GetDamage());
        // for (int i = 1; i <= cardThatWasReversed.GetDamage(); i++)
        // {
        //     Card cardThatWillGoToRingside = playerThatWasReversed.GetCardOnTopOfArsenal();
        //     View.ShowCardOverturnByTakingDamage(cardThatWillGoToRingside.ToString(), i, cardThatWasReversed.GetDamage());
        //     playerThatWasReversed.MoveArsenalTopCardToRingside();
        // }
        ManeuverPlayer maneuverPlayer = new ManeuverPlayer(View, playerThatReversePlay, playerThatWasReversed, new EffectForNextMove(0,0));
        cardObject.ReversalDamage = damage;
        maneuverPlayer.PlayReversalAsManeuver(cardObject);
        // playerThatReversePlay.MoveCardFromHandToRingside(cardObject);
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
        Console.WriteLine("CHEcking net damage for rolling takedown" + Convert.ToString(netDamageThatWillReceive));
        Card cardThatIsBeingPlayed = playThatIsBeingPlayed.Card;
        List<string> cardsSubtypes = cardThatIsBeingPlayed.Subtypes;
        if (netDamageThatWillReceive <= 7 && playThatIsBeingPlayed.PlayedAs == "MANEUVER" && cardsSubtypes.Contains("Grapple"))
        {
            return true;
        }

        return false;
    }
}