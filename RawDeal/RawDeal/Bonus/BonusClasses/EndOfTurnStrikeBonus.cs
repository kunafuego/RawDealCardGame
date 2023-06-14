namespace RawDeal.Bonus.BonusClasses;

public class EndOfTurnStrikeBonus : Bonus
{
    private int _bonusAmount;
    public EndOfTurnStrikeBonus(int bonusAmount)
    {
        _bonusAmount = bonusAmount;
    }

    public override int GetBonusAmount()
    {
        return _bonusAmount;
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