using RawDeal;
using RawDeal.Bonus;
using RawDeal.Bonus.BonusClasses;
using RawDeal.Effects;
using RawDealView;

public class AddBonusUntilEndOfTurn : Effect
{
    private BonusManager _bonusManager;
    private int _bonusAmount;
    private string _maneuverOrStrike;
    public AddBonusUntilEndOfTurn(BonusManager bonusManager, int bonusAmount, string maneuverOrStrike)
    {
        _bonusManager = bonusManager;
        _bonusAmount = bonusAmount;
        _maneuverOrStrike = maneuverOrStrike;
    }
    
    public override void Apply(Play actualPlay, View view, Player playerThatPlayedCard, Player opponent)
    {
        if(_maneuverOrStrike == "MANEUVER") _bonusManager.AddDamageBonus(new EndOfTurnManeuverBonus(_bonusAmount));
        if(_maneuverOrStrike == "STRIKE") _bonusManager.AddDamageBonus(new EndOfTurnStrikeBonus(_bonusAmount));
    }
}