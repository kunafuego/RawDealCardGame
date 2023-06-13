namespace RawDeal.Bonus;

public abstract class FortitudeBonus
{
    private int _bonusAmount;

    protected FortitudeBonus(int bonusAmount, ExpireOptions expireCondition)
    {
        _bonusAmount = bonusAmount;
    }

    public abstract bool CheckIfBonusExpired();
    public abstract bool CheckIfBonusCanApplyToPlay();

}