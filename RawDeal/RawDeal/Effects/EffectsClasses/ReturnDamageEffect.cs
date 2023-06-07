using RawDealView;
namespace RawDeal.Effects.EffectsClasses;

public class ReturnDamageEffect : Effect
{
    public override void Apply(Play playThatIsBeingReversed, View view, Player playerBeingReversed, Player playerThatReversePlay)
    {
        ManeuverPlayer maneuverPlayer = new ManeuverPlayer(view, playerThatReversePlay, playerBeingReversed, new EffectForNextMove(0,0));
        maneuverPlayer.PlayReversalAsManeuver(playThatIsBeingReversed.CardThatWasReversedBy);
    }
}