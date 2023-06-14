using RawDeal;
using RawDeal.Bonus;
using RawDeal.Effects;
using RawDeal.Effects.EffectsClasses;

public class NextManeuverBonusDamage : Bonus
{
    private int _bonusAmount;
    private int _playsThatHavePassed;
    private LastPlay _lastPlayInstance;
    public NextManeuverBonusDamage(int bonusAmount, LastPlay lastPlayInstance)
    {
        _bonusAmount = bonusAmount;
        _playsThatHavePassed = 0;
        _lastPlayInstance = lastPlayInstance;
    }

    public override int GetBonusAmount()
    {
        return _bonusAmount;
    }

    public override bool CheckIfBonusExpired(ExpireOptions expireOptions)
    {
        _playsThatHavePassed += 1;
        Console.WriteLine($"HAN PASADO {_playsThatHavePassed} jugadas desde que creamos el bonus");
        return _playsThatHavePassed == 2;
    }

    public override bool CheckIfBonusCanApplyToPlay(Play playThatIsTryingToBePlayed, Player opponent)
    {
        if (_lastPlayInstance.WasItASuccesfulReversal) return false;
        Console.WriteLine($"La útlima fue reversal? {_lastPlayInstance.WasItASuccesfulReversal}");
        return playThatIsTryingToBePlayed.PlayedAs == "MANEUVER" && _playsThatHavePassed >= 1;
    }
}