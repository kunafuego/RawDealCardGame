namespace RawDeal.Bonus.BonusClasses;

public class JockeyingDamageBonus : Bonus
{
    private const int ConstBonusAmount = 4;

    public JockeyingDamageBonus()
    {
        BonusAmount = ConstBonusAmount;
    }

    public override bool CheckIfBonusCanApplyToPlay(Play playThatIsTryingToBePlayed,
        Player opponent)
    {
        var cardBeingPlayed = playThatIsTryingToBePlayed.Card;
        return cardBeingPlayed.CheckIfSubtypesContain("Grapple");
    }
}