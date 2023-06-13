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

    public int GetPlayDamage(Play playThatIsBeingPlayed, Player opponent)
    {
        Card cardThatIsBeingPlayed = playThatIsBeingPlayed.Card;
        Console.WriteLine("Obteniendo el damage que debería hacer");
        int netDamage = cardThatIsBeingPlayed.GetDamage();
        Console.WriteLine(netDamage);
        foreach (DamageBonus damageBonus in _damageBonusList)
        {
            if (damageBonus.CheckIfBonusCanApplyToPlay(playThatIsBeingPlayed, opponent))
                netDamage += damageBonus.GetBonusAmount();
            Console.WriteLine(damageBonus.CheckIfBonusCanApplyToPlay(playThatIsBeingPlayed, opponent));
        }

        return netDamage;
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
    
    public void GetPlayFortitude(){}
    
    
}
