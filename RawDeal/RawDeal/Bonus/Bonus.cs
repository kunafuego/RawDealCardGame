namespace RawDeal.Bonus;

public abstract class Bonus
{
    protected int BonusAmount { get; set; }

    
    public abstract bool CheckIfBonusExpired(ExpireOptions expireOptions);
    public abstract bool CheckIfBonusCanApplyToPlay(Play playThatIsTryingToBePlayed, Player opponent);

    public int GetBonusAmount()
    {
        return BonusAmount;
    }
}