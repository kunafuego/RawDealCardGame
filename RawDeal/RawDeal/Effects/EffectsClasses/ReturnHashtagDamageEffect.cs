using RawDeal.Bonus;
using RawDealView;

namespace RawDeal.Effects.EffectsClasses;

public class ReturnHashtagDamageEffect : Effect
{
    private readonly BonusManager _bonusManager;
    private readonly LastPlay _lastPlayInstance;
    private readonly View _view;

    public ReturnHashtagDamageEffect(LastPlay lastPlayInstance, BonusManager bonusManager,
        View view)
    {
        _lastPlayInstance = lastPlayInstance;
        _bonusManager = bonusManager;
        _view = view;
    }

    public override void Apply(Play actualPlay, Player playerThatPlayedCard, Player opponent)
    {
        var cardThatReverted = actualPlay.Card;
        cardThatReverted.ReversalDamage = ManageDamage(playerThatPlayedCard);
        var maneuverPlayer = new ManeuverPlayer(_view, playerThatPlayedCard, opponent,
            _lastPlayInstance, _bonusManager);
        if (_lastPlayInstance.LastPlayPlayed.Card.Title != "Rolling Takedown")
            maneuverPlayer.PlayReversalAsManeuver(cardThatReverted);
    }

    private int ManageDamage(Player playerThatReverse)
    {
        var lastPlay = _lastPlayInstance.LastPlayPlayed;
        var lastCardPlayed = lastPlay.Card;
        var initialDamage = lastCardPlayed.ReversalDamage;
        if (AbilitiesManager.CheckIfHasAbilityWhenReceivingDamage(playerThatReverse))
            initialDamage -= 1;

        return initialDamage;
    }
}