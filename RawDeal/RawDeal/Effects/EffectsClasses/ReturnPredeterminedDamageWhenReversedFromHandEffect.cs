using RawDealView;
namespace RawDeal.Effects.EffectsClasses;

public class ReturnPredeterminedDamageWhenReversedFromHandEffect : Effect
{
    public override void Apply(Play playThatIsBeingReversed, View view, Player playerThatReversePlay, Player playerThatWasReversed)
    {
        if (playThatIsBeingReversed.PlayedAs != "Reversed From Hand") return;
        ManeuverPlayer maneuverPlayer = new ManeuverPlayer(view, playerThatReversePlay, playerThatWasReversed, new EffectForNextMove(0,0));
        maneuverPlayer.PlayReversalAsManeuver(playThatIsBeingReversed.CardThatWasReversedBy);
    }
}