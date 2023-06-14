using RawDeal;
using RawDeal.Bonus;

public class EndOfTurnManeuverBonus : Bonus
{
    private int _bonusAmount;
    public EndOfTurnManeuverBonus(int bonusAmount)
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
        return playThatIsTryingToBePlayed.PlayedAs == "MANEUVER";
    }
}