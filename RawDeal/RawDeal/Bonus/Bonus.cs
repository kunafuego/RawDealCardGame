namespace RawDeal.Bonus;

public abstract class Bonus
{
    protected int BonusAmount { get; set; }

    
    public abstract bool CheckIfBonusCanApplyToPlay(Play playThatIsTryingToBePlayed, Player opponent);

    public virtual bool CheckIfBonusExpired(ExpireOptions expireOptions)
    {
        return true;
    }

    public int GetBonusAmount()
    {
        return BonusAmount;
    }
}