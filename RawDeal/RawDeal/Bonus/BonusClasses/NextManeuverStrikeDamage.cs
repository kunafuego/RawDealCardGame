using RawDeal;
using RawDeal.Bonus;
using RawDeal.Effects;
using RawDeal.Effects.EffectsClasses;

public class NextManeuverStrikeDamage : Bonus
{
    private int _playsThatHavePassed;
    private LastPlay _lastPlayInstance;
    public NextManeuverStrikeDamage(int bonusAmount, LastPlay lastPlayInstance)
    {
        BonusAmount = bonusAmount;
        _playsThatHavePassed = 0;
        _lastPlayInstance = lastPlayInstance;
    }


    public override bool CheckIfBonusExpired(ExpireOptions expireOptions)
    {
        _playsThatHavePassed += 1;
        Console.WriteLine($"HAN PASADO {_playsThatHavePassed} jugadas desde que creamos el bonus");
        return _playsThatHavePassed == 2;
        
    }

    public override bool CheckIfBonusCanApplyToPlay(Play playThatIsTryingToBePlayed, Player opponent)
    {
        Card cardBeingPlayed = playThatIsTryingToBePlayed.Card;
        Console.WriteLine($"La útlima fue reversal? {_lastPlayInstance.WasItASuccesfulReversal}");
        if (_lastPlayInstance.WasItASuccesfulReversal) return false;
        return cardBeingPlayed.CheckIfSubtypesContain("Strike") && _playsThatHavePassed >= 1;    
    }
}