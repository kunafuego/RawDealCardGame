using RawDealView;
namespace RawDeal.Effects.EffectsClasses;

public class ReturnHashtagDamageEffect : Effect
{
    public override void Apply(Play playThatIsBeingReversed, View view, Player playerThatPlayedCard, Player opponent)
    {
        Card cardThatWasReversed = playThatIsBeingReversed.Card;
        Card cardThatReverted = playThatIsBeingReversed.CardThatWasReversedBy;
        int damageThatReversalHasToMake = ManageDamage(cardThatWasReversed, playerThatPlayedCard);
        cardThatReverted.ReversalDamage = damageThatReversalHasToMake;
        ManeuverPlayer maneuverPlayer = new ManeuverPlayer(view, playerThatPlayedCard, opponent, new EffectForNextMove(0,0));
        maneuverPlayer.PlayReversalAsManeuver(playThatIsBeingReversed.CardThatWasReversedBy);
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
}