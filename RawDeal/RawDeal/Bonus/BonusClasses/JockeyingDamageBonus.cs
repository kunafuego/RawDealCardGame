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

    public override bool CheckIfBonusExpired()
    {
        return true;
    }

    public override bool CheckIfBonusCanApplyToPlay(Play playThatIsTryingToBePlayed, Player opponent)
    {
        Card cardBeingPlayed = playThatIsTryingToBePlayed.Card;
        // Console.WriteLine(cardBeingPlayed.ToString());
        Console.WriteLine($"\n Es grapple? {cardBeingPlayed.CheckIfSubtypesContain("Grapple")}");
        return cardBeingPlayed.CheckIfSubtypesContain("Grapple");
    }
}