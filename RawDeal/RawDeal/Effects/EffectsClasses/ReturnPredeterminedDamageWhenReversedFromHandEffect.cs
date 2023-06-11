using RawDealView;
namespace RawDeal.Effects.EffectsClasses;

public class ReturnPredeterminedDamageWhenReversedFromHandEffect : Effect
{
    private LastPlay _lastPlayInstance;
    public ReturnPredeterminedDamageWhenReversedFromHandEffect(LastPlay lastPlayInstance)
    {
        _lastPlayInstance = lastPlayInstance;
    }
    public override void Apply(Play actualPlay, View view, Player playerThatPlayedCard, Player opponent)
    {
        if (actualPlay.PlayedAs != "REVERSAL HAND") return;
        ManeuverPlayer maneuverPlayer = new ManeuverPlayer(view, playerThatPlayedCard, opponent, new EffectForNextMove(0,0), _lastPlayInstance);
        maneuverPlayer.PlayReversalAsManeuver(actualPlay.Card);
    }
}