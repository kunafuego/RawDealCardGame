namespace RawDeal.Bonus.BonusClasses;

public class JockeyingDamageBonus : Bonus
{
    private const int ConstBonusAmount = 4;
    public JockeyingDamageBonus()
    {
        BonusAmount = ConstBonusAmount;
    }


    public override bool CheckIfBonusExpired(ExpireOptions expireOptions)
    {
        return true;
    }

    public override bool CheckIfBonusCanApplyToPlay(Play playThatIsTryingToBePlayed, Player opponent)
    {
        Card cardBeingPlayed = playThatIsTryingToBePlayed.Card;
        return cardBeingPlayed.CheckIfSubtypesContain("Grapple");
    }
}