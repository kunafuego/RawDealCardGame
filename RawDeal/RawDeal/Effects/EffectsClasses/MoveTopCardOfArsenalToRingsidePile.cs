using RawDealView;
namespace RawDeal.Effects.EffectsClasses;

public class MoveTopCardOfArsenalToRingsidePile : Effect
{
    public override void Apply(Play playThatIsBeingReversed, View view, Player playerNotPlayingRound, Player playerPlayingRound)
    {
        playerPlayingRound.MoveArsenalTopCardToRingside();
    }
}