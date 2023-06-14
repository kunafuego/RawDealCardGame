using RawDeal;
using RawDeal.Bonus;

public class IrishBonus : Bonus
{
    private const int BonusAmount = 5;
    public IrishBonus()
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
        Console.WriteLine($"\n Es Strike? {cardBeingPlayed.CheckIfSubtypesContain("Strike")}");
        return cardBeingPlayed.CheckIfSubtypesContain("Strike");
    }
}