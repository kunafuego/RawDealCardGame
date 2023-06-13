using RawDeal.Bonus;
using RawDeal.Bonus.BonusClasses;
using RawDealView;
using RawDealView.Options;

namespace RawDeal.Effects.EffectsClasses;

public class IrishWhipEffect : Effect
{
    private BonusManager _bonusManager;
    public IrishWhipEffect(BonusManager bonusManager)
    {
        _bonusManager = bonusManager;
    }
    
    public override void Apply(Play actualPlay, View view, Player playerThatPlayedCard, Player opponent)
    {
        _bonusManager.AddDamageBonus(new IrishBonus());
    }
}