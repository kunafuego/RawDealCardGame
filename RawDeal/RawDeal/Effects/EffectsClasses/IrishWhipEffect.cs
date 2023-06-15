using RawDeal.Bonus;

namespace RawDeal.Effects.EffectsClasses;

public class IrishWhipEffect : Effect
{
    private readonly BonusManager _bonusManager;

    public IrishWhipEffect(BonusManager bonusManager)
    {
        _bonusManager = bonusManager;
    }

    public override void Apply(Play actualPlay, Player playerThatPlayedCard, Player opponent)
    {
        _bonusManager.AddDamageBonus(new IrishBonus());
    }
}