using RawDeal;
using RawDeal.Bonus;

public class IrishBonus : Bonus
{
    private const int ConstBonusAmount = 5;
    public IrishBonus()
    {
        BonusAmount = ConstBonusAmount;
    }

    public override bool CheckIfBonusCanApplyToPlay(Play playThatIsTryingToBePlayed, Player opponent)
    {
        Card cardBeingPlayed = playThatIsTryingToBePlayed.Card;
        Console.WriteLine($"\n Es Strike? {cardBeingPlayed.CheckIfSubtypesContain("Strike")}");
        return cardBeingPlayed.CheckIfSubtypesContain("Strike");
    }
}