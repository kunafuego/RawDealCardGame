namespace RawDeal.Effects;

public abstract class Effect
{
    public abstract void Apply(Play actualPlay, Player playerThatPlayedCard, Player opponent);
}