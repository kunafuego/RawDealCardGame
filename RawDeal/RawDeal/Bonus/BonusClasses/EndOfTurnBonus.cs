namespace RawDeal.Bonus.BonusClasses;

public class EndOfTurnBonus : Bonus
{
    public EndOfTurnBonus(int bonusAmount)
    {
        BonusAmount = bonusAmount;
    }

    public override bool CheckIfBonusExpired(ExpireOptions expireOptions)
    {
        if (expireOptions == ExpireOptions.EndOfTurn) return true;
        return false;
    }

    public override bool CheckIfBonusCanApplyToPlay(Play playThatIsTryingToBePlayed, Player opponent)
    {
        Card cardTryingToBePlayed = playThatIsTryingToBePlayed.Card;
        return cardTryingToBePlayed.CheckIfSubtypesContain("Strike");
    }
}