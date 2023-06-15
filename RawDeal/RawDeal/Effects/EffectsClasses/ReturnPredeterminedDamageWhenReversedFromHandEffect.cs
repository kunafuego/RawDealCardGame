using RawDeal.Bonus;
using RawDealView;
namespace RawDeal.Effects.EffectsClasses;

public class ReturnPredeterminedDamageWhenReversedFromHandEffect : Effect
{
    private LastPlay _lastPlayInstance;
    private BonusManager _bonusManager;
    private View _view;
    public ReturnPredeterminedDamageWhenReversedFromHandEffect(LastPlay lastPlayInstance, BonusManager bonusManager, View view)
    {
        _lastPlayInstance = lastPlayInstance;
        _bonusManager = bonusManager;
        _view = view;
    }
    public override void Apply(Play actualPlay, Player playerThatPlayedCard, Player opponent)
    {
        if (actualPlay.PlayedAs != "REVERSAL HAND") return;
        _bonusManager.CheckIfBonusExpire(ExpireOptions.EndOfTurn);
        ManeuverPlayer maneuverPlayer = new ManeuverPlayer(_view, playerThatPlayedCard, opponent, _lastPlayInstance, _bonusManager);
        maneuverPlayer.PlayReversalAsManeuver(actualPlay.Card);
    }
}