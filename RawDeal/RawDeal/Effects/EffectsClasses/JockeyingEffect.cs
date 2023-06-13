using RawDeal;
using RawDeal.Bonus;
using RawDeal.Bonus.BonusClasses;
using RawDeal.Effects;
using RawDealView;
using RawDealView.Options;

public class JockeyingEffect : Effect
{
    private BonusManager _bonusManager;
    public JockeyingEffect(BonusManager bonusManager)
    {
        _bonusManager = bonusManager;
    }
    
    public override void Apply(Play actualPlay, View view, Player playerThatPlayedCard, Player opponent)
    {
        SelectedEffect chosenOption = view.AskUserToSelectAnEffectForJockeyForPosition(playerThatPlayedCard.GetSuperstarName());
        if (chosenOption == SelectedEffect.NextGrappleIsPlus4D)
        {
            _bonusManager.AddDamageBonus(new JockeyingDamageBonus());
        }
        else if (chosenOption == SelectedEffect.NextGrapplesReversalIsPlus8F)
        {
            _bonusManager.AddFortitudeBonus(new JockeyingFortitudeBonus());
        }
    }
}