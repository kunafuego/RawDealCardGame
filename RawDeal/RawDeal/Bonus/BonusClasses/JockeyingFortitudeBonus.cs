namespace RawDeal.Bonus.BonusClasses;

public class JockeyingFortitudeBonus : FortitudeBonus
{
    private const int BonusAmount = 8;
    public JockeyingFortitudeBonus()
    {
        
    }

    public override int GetBonusAmount()
    {
        return BonusAmount;
    }

    public override bool CheckIfFortitudeBonusExpired()
    {
        return true;
    }

    public override bool CheckIfBonusCanApplyToPlay(Card cardThatIsTryingToBePlayed)
    {
        return cardThatIsTryingToBePlayed.CheckIfSubtypesContain("Grapple");
    }
}