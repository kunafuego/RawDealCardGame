using RawDeal.Bonus;

namespace RawDeal.Effects.EffectsClasses;

public class AddBonusEffect : Effect
{
    private readonly BonusManager _bonusManager;
    private readonly Bonus.Bonus _bonusToAdd;

    public AddBonusEffect(BonusManager bonusManager, Bonus.Bonus bonusToAdd)
    {
        _bonusManager = bonusManager;
        _bonusToAdd = bonusToAdd;
    }

    public override void Apply(Play actualPlay, Player playerThatPlayedCard, Player opponent)
    {
        _bonusManager.AddDamageBonus(_bonusToAdd);
    }
}