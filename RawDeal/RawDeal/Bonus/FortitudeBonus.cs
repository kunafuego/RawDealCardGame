namespace RawDeal.Bonus;

public abstract class FortitudeBonus
{
    public abstract int GetBonusAmount();

    public abstract bool CheckIfFortitudeBonusExpired();
    public abstract bool CheckIfBonusCanApplyToPlay(Card cardThatIsTryingToBePlayed);
}