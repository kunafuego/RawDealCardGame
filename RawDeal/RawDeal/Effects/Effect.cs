using RawDealView;
namespace RawDeal.Effects;

public abstract class Effect
{
    public abstract void Apply(Play playThatIsBeingReversed, View view, Player playerPlayingRound, Player playerNotPlayingRound);
}