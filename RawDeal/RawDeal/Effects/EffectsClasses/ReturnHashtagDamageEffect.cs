using RawDeal.Bonus;
using RawDealView;
namespace RawDeal.Effects.EffectsClasses;

public class ReturnHashtagDamageEffect : Effect
{
    private LastPlay _lastPlayInstance;
    private BonusManager _bonusManager;
    private View _view;
    public ReturnHashtagDamageEffect(LastPlay lastPlayInstance, BonusManager bonusManager, View view)
    {
        _lastPlayInstance = lastPlayInstance;
        _bonusManager = bonusManager;
        _view = view;
    }
    
    public override void Apply(Play actualPlay, Player playerThatPlayedCard, Player opponent)
    {
        Card cardThatReverted = actualPlay.Card;
        cardThatReverted.ReversalDamage = ManageDamage(playerThatPlayedCard);
        ManeuverPlayer maneuverPlayer = new ManeuverPlayer(_view, playerThatPlayedCard, opponent, _lastPlayInstance, _bonusManager);
        if(_lastPlayInstance.LastPlayPlayed.Card.Title != "Rolling Takedown")
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