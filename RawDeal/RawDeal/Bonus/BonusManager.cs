namespace RawDeal.Bonus;

public class BonusManager
{
    private List<DamageBonus> _damageBonusList { get; set; }
    private List<FortitudeBonus> _fortitudeBonusList { get; set; }
    private LastPlay _lastPlayInstance;

    public BonusManager(LastPlay lastPlayInstance)
    {
        _damageBonusList = new List<DamageBonus>();
        _fortitudeBonusList = new List<FortitudeBonus>();
        _lastPlayInstance = lastPlayInstance;
    }

    public void AddDamageBonus(DamageBonus bonus)
    {
        _damageBonusList.Add(bonus);
    }

    public void AddFortitudeBonus(FortitudeBonus bonus)
    {
        _fortitudeBonusList.Add(bonus);
    }

    public void CheckIfBonusExpire()
    {
        foreach (var damageBonus in _damageBonusList.Where(damageBonus => damageBonus.CheckIfBonusExpired()))
        {
            _damageBonusList.Remove(damageBonus);
        }

        foreach (var fortitudeBonus in _fortitudeBonusList.Where(fortitudeBonus => fortitudeBonus.CheckIfBonusExpired()))
        {
            _fortitudeBonusList.Remove(fortitudeBonus);
        }
    }
    
    public void GetPlayDamage(){}
    
    public void GetPlayFortitude(){}
    
    
}
