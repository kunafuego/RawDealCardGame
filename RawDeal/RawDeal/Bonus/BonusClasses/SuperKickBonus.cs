using RawDeal;
using RawDeal.Bonus;

public class SuperKickBonus : Bonus
{
    private const int ConstBonusAmount = 5;
    public SuperKickBonus()
    {
        BonusAmount = ConstBonusAmount;
    }

    public override bool CheckIfBonusCanApplyToPlay(Play playThatIsTryingToBePlayed, Player opponent)
    {
        Card cardBeingPlayed = playThatIsTryingToBePlayed.Card;
        return cardBeingPlayed.Title == "Superkick";
    }
}