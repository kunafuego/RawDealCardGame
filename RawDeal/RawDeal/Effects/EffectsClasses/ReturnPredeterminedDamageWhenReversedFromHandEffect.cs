using RawDeal.Bonus;
using RawDealView;
namespace RawDeal.Effects.EffectsClasses;

public class ReturnPredeterminedDamageWhenReversedFromHandEffect : Effect
{
    private LastPlay _lastPlayInstance;
    private BonusManager _bonusManager;
    public ReturnPredeterminedDamageWhenReversedFromHandEffect(LastPlay lastPlayInstance, BonusManager bonusManager)
    {
        _lastPlayInstance = lastPlayInstance;
        _bonusManager = bonusManager;
    }
    public override void Apply(Play actualPlay, View view, Player playerThatPlayedCard, Player opponent)
    {
        if (actualPlay.PlayedAs != "REVERSAL HAND") return;
        ManeuverPlayer maneuverPlayer = new ManeuverPlayer(view, playerThatPlayedCard, opponent, new EffectForNextMove(0,0), _lastPlayInstance, _bonusManager);
        maneuverPlayer.PlayReversalAsManeuver(actualPlay.Card);
        // Play lastPlayPlayed = _lastPlayInstance.LastPlayPlayed;
        // lastPlayPlayed.PlayedAs = "MANEUVER";
    }
}