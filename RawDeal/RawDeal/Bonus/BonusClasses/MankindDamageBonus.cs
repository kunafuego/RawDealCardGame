namespace RawDeal.Bonus.BonusClasses;

public class MankindDamageBonus : Bonus
{
    private const int ConstBonusAmount = -1;
    public MankindDamageBonus()
    {
        BonusAmount = ConstBonusAmount;
    }

    public override bool CheckIfBonusExpired(ExpireOptions expireOptions)
    {
        return false;
    }

    public override bool CheckIfBonusCanApplyToPlay(Play playThatIsTryingToBePlayed, Player opponent)
    {
        return opponent.GetSuperstarName() == "MANKIND";
    }
}