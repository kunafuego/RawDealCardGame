namespace RawDeal.Bonus;

public abstract class Bonus
{
    public abstract int GetBonusAmount();
    
    public abstract bool CheckIfBonusExpired(ExpireOptions expireOptions);
    public abstract bool CheckIfBonusCanApplyToPlay(Play playThatIsTryingToBePlayed, Player opponent);

}