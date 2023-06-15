namespace RawDeal.Bonus;

public class BonusManager
{
    private LastPlay _lastPlayInstance;

    public BonusManager(LastPlay lastPlayInstance)
    {
        _damageBonusList = new List<Bonus>();
        _fortitudeBonusList = new List<FortitudeBonus>();
        _lastPlayInstance = lastPlayInstance;
    }

    private List<Bonus> _damageBonusList { get; }
    private List<FortitudeBonus> _fortitudeBonusList { get; }

    public void AddDamageBonus(Bonus bonus)
    {
        if (bonus is IrishBonus) CheckIfBonusExpire(ExpireOptions.OneMoreCardWasPlayed);
        _damageBonusList.Add(bonus);
    }

    public void AddFortitudeBonus(FortitudeBonus bonus)
    {
        _fortitudeBonusList.Add(bonus);
    }

    public int GetPlayDamage(Play playThatIsBeingPlayed, Player opponent)
    {
        var cardThatIsBeingPlayed = playThatIsBeingPlayed.Card;
        var netDamage = cardThatIsBeingPlayed.GetDamage();
        foreach (var damageBonus in _damageBonusList)
            if (damageBonus.CheckIfBonusCanApplyToPlay(playThatIsBeingPlayed, opponent))
                netDamage += damageBonus.GetBonusAmount();
        return netDamage;
    }

    public int GetPlayFortitude(Card cardThatIsTryingToBePlayed, Card cardThatIWantToReverseWith)
    {
        var netFortitude = cardThatIWantToReverseWith.Fortitude;
        foreach (var fortitudeBonus in _fortitudeBonusList)
            if (fortitudeBonus.CheckIfBonusCanApplyToPlay(cardThatIsTryingToBePlayed))
                netFortitude += fortitudeBonus.GetBonusAmount();
        return netFortitude;
    }


    public void CheckIfBonusExpire(ExpireOptions expireOptions)
    {
        var expiredDamageBonuses = _damageBonusList
            .Where(damageBonus => damageBonus.CheckIfBonusExpired(expireOptions)).ToList();
        foreach (var damageBonus in expiredDamageBonuses) _damageBonusList.Remove(damageBonus);
    }

    public void CheckIfFortitudeBonusExpire()
    {
        var expiredFortitudeBonuses = _fortitudeBonusList
            .Where(fortitudeBonus => fortitudeBonus.CheckIfFortitudeBonusExpired()).ToList();
        foreach (var fortitudeBonus in expiredFortitudeBonuses)
            _fortitudeBonusList.Remove(fortitudeBonus);
    }
}