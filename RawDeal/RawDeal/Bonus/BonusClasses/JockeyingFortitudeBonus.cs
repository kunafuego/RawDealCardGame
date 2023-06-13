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
        // Card cardBeingPlayed = playThatIsTryingToBePlayed.Card;
        Console.WriteLine(cardThatIsTryingToBePlayed.ToString());
        Console.WriteLine("Grapple?");
        Console.WriteLine(cardThatIsTryingToBePlayed.CheckIfSubtypesContain("Grapple"));
        Console.WriteLine("\n");
        return cardThatIsTryingToBePlayed.CheckIfSubtypesContain("Grapple");
    }
}