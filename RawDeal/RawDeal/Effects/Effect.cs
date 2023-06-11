using RawDealView;
namespace RawDeal.Effects;

public abstract class Effect
{
    public abstract void Apply(Play actualPlay, View view, Player playerThatPlayedCard, Player opponent);
}