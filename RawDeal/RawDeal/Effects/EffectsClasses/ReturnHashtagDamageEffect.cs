using RawDealView;
namespace RawDeal.Effects.EffectsClasses;

public class ReturnHashtagDamageEffect : Effect
{
    private LastPlay _lastPlayInstance;
    public ReturnHashtagDamageEffect(LastPlay lastPlayInstance)
    {
        _lastPlayInstance = lastPlayInstance;
    }
    
    public override void Apply(Play actualPlay, View view, Player playerThatPlayedCard, Player opponent)
    {
        Card cardThatReverted = actualPlay.Card;
        cardThatReverted.ReversalDamage = ManageDamage(playerThatPlayedCard);
        ManeuverPlayer maneuverPlayer = new ManeuverPlayer(view, playerThatPlayedCard, opponent, new EffectForNextMove(0,0), _lastPlayInstance);
        maneuverPlayer.PlayReversalAsManeuver(cardThatReverted);
    }
    
    private int ManageDamage(Player playerThatReverse)
    {
        Play lastPlay = _lastPlayInstance.LastPlayPlayed;
        Card lastCardPlayed = lastPlay.Card;
        int initialDamage = lastCardPlayed.ReversalDamage;
        if (AbilitiesManager.CheckIfHasAbilityWhenReceivingDamage(playerThatReverse))
        {
            initialDamage -= 1;
        }

        return initialDamage;
    }
}