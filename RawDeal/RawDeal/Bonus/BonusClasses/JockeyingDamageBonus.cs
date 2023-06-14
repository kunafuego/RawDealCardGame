namespace RawDeal.Bonus.BonusClasses;

public class JockeyingDamageBonus : Bonus
{
    private const int BonusAmount = 4;
    public JockeyingDamageBonus()
    {
        
    }

    public override int GetBonusAmount()
    {
        return BonusAmount;
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