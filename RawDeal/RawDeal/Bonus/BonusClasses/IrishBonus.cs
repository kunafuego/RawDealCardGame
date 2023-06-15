using RawDeal;
using RawDeal.Bonus;

public class IrishBonus : Bonus
{
    private const int ConstBonusAmount = 5;

    public IrishBonus()
    {
        BonusAmount = ConstBonusAmount;
    }

    public override bool CheckIfBonusCanApplyToPlay(Play playThatIsTryingToBePlayed,
        Player opponent)
    {
        var cardBeingPlayed = playThatIsTryingToBePlayed.Card;
        return cardBeingPlayed.CheckIfSubtypesContain("Strike");
    }
}