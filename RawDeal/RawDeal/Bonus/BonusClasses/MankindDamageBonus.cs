namespace RawDeal.Bonus.BonusClasses;

public class MankindDamageBonus : DamageBonus
{
    private const int BonusAmount = -1;
    public MankindDamageBonus()
    {
        
    }

    public override int GetBonusAmount()
    {
        return BonusAmount;
    }

    public override bool CheckIfBonusExpired()
    {
        return false;
    }

    public override bool CheckIfBonusCanApplyToPlay(Play playThatIsTryingToBePlayed, Player opponent)
    {
        return opponent.GetSuperstarName() == "MANKIND";
    }
}