namespace RawDeal.Bonus;

public abstract class Bonus
{
    // private int _bonusAmount;
    //
    // protected DamageBonus(int bonusAmount)
    // {
    //     _bonusAmount = bonusAmount;
    // }

    public abstract int GetBonusAmount();
    
    public abstract bool CheckIfBonusExpired();
    public abstract bool CheckIfBonusCanApplyToPlay(Play playThatIsTryingToBePlayed, Player opponent);

}