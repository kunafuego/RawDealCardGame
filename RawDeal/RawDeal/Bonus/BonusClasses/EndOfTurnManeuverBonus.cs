using RawDeal;
using RawDeal.Bonus;

public class EndOfTurnManeuverBonus : Bonus
{
    public EndOfTurnManeuverBonus(int bonusAmount)
    {
        BonusAmount = bonusAmount;
    }


    public override bool CheckIfBonusExpired(ExpireOptions expireOptions)
    {
        if (expireOptions == ExpireOptions.EndOfTurn) return true;
        return false;
    }

    public override bool CheckIfBonusCanApplyToPlay(Play playThatIsTryingToBePlayed,
        Player opponent)
    {
        return playThatIsTryingToBePlayed.PlayedAs == "MANEUVER";
    }
}