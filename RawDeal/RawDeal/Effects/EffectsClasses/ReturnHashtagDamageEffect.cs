using RawDeal.Bonus;
using RawDealView;
namespace RawDeal.Effects.EffectsClasses;

public class ReturnHashtagDamageEffect : Effect
{
    private LastPlay _lastPlayInstance;
    private BonusManager _bonusManager;
    public ReturnHashtagDamageEffect(LastPlay lastPlayInstance, BonusManager bonusManager)
    {
        _lastPlayInstance = lastPlayInstance;
        _bonusManager = bonusManager;
    }
    
    public override void Apply(Play actualPlay, View view, Player playerThatPlayedCard, Player opponent)
    {
        Card cardThatReverted = actualPlay.Card;
        cardThatReverted.ReversalDamage = ManageDamage(playerThatPlayedCard);
        ManeuverPlayer maneuverPlayer = new ManeuverPlayer(view, playerThatPlayedCard, opponent, new EffectForNextMove(0,0), _lastPlayInstance, _bonusManager);
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