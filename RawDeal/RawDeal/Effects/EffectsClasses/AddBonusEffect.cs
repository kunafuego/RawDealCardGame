using RawDeal.Bonus;
using RawDeal.Bonus.BonusClasses;
using RawDealView;
using RawDealView.Options;

namespace RawDeal.Effects.EffectsClasses;

public class AddBonusEffect : Effect
{
    private BonusManager _bonusManager;
    private Bonus.Bonus _bonusToAdd;
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