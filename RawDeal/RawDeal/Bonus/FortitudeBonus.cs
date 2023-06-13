namespace RawDeal.Bonus;

public abstract class FortitudeBonus
{
    // private int _bonusAmount;
    //
    // protected DamageBonus(int bonusAmount)
    // {
    //     _bonusAmount = bonusAmount;
    // }

    public abstract int GetBonusAmount();
    
    public abstract bool CheckIfFortitudeBonusExpired();
    public abstract bool CheckIfBonusCanApplyToPlay(Card cardThatIsTryingToBePlayed);

}