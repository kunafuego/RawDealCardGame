using RawDeal;
using RawDeal.Bonus;

public class NextManeuverBonusDamage : Bonus
{
    private readonly LastPlay _lastPlayInstance;
    private int _playsThatHavePassed;

    public NextManeuverBonusDamage(int bonusAmount, LastPlay lastPlayInstance)
    {
        BonusAmount = bonusAmount;
        _playsThatHavePassed = 0;
        _lastPlayInstance = lastPlayInstance;
    }


    public override bool CheckIfBonusExpired(ExpireOptions expireOptions)
    {
        _playsThatHavePassed += 1;
        return _playsThatHavePassed == 2;
    }

    public override bool CheckIfBonusCanApplyToPlay(Play playThatIsTryingToBePlayed,
        Player opponent)
    {
        if (_lastPlayInstance.WasItASuccesfulReversal) return false;
        return playThatIsTryingToBePlayed.PlayedAs == "MANEUVER" && _playsThatHavePassed >= 1;
    }
}