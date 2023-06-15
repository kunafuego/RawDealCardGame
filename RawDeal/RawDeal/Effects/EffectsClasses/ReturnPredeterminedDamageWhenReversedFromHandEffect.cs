using RawDeal.Bonus;
using RawDealView;

namespace RawDeal.Effects.EffectsClasses;

public class ReturnPredeterminedDamageWhenReversedFromHandEffect : Effect
{
    private readonly BonusManager _bonusManager;
    private readonly LastPlay _lastPlayInstance;
    private readonly View _view;

    public ReturnPredeterminedDamageWhenReversedFromHandEffect(LastPlay lastPlayInstance,
        BonusManager bonusManager, View view)
    {
        _lastPlayInstance = lastPlayInstance;
        _bonusManager = bonusManager;
        _view = view;
    }

    public override void Apply(Play actualPlay, Player playerThatPlayedCard, Player opponent)
    {
        if (actualPlay.PlayedAs != "REVERSAL HAND") return;
        _bonusManager.CheckIfBonusExpire(ExpireOptions.EndOfTurn);
        var maneuverPlayer = new ManeuverPlayer(_view, playerThatPlayedCard, opponent,
            _lastPlayInstance, _bonusManager);
        maneuverPlayer.PlayReversalAsManeuver(actualPlay.Card);
    }
}