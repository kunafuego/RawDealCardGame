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
        return _playsThatHavePassed == 2;
        
    }

    public override bool CheckIfBonusCanApplyToPlay(Play playThatIsTryingToBePlayed, Player opponent)
    {
        Card cardBeingPlayed = playThatIsTryingToBePlayed.Card;
        if (_lastPlayInstance.WasItASuccesfulReversal) return false;
        return cardBeingPlayed.CheckIfSubtypesContain("Strike") && _playsThatHavePassed >= 1;    
    }
}