using RawDealView;
namespace RawDeal.Effects;

public class NoEffect : Effect
{
    public override void Apply(Play playThatIsBeingReversed, View view, Player playerThatPlayedCard, Player opponent) { }
}