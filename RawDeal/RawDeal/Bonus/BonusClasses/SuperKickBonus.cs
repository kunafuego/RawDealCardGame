using RawDeal;
using RawDeal.Bonus;

public class SuperKickBonus : Bonus
{
    private const int BonusAmount = 5;
    public SuperKickBonus()
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
        return cardBeingPlayed.Title == "Superkick";
    }
}