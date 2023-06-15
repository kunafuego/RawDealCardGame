using RawDeal;
using RawDeal.Bonus;
using RawDeal.Bonus.BonusClasses;
using RawDeal.Effects;
using RawDealView;
using RawDealView.Options;

public class JockeyingEffect : Effect
{
    private BonusManager _bonusManager;
    private View _view;
    public JockeyingEffect(BonusManager bonusManager, View view)
    {
        _bonusManager = bonusManager;
        _view = view;
    }
    
    public override void Apply(Play actualPlay, Player playerThatPlayedCard, Player opponent)
    {
        SelectedEffect chosenOption = _view.AskUserToSelectAnEffectForJockeyForPosition(playerThatPlayedCard.GetSuperstarName());
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