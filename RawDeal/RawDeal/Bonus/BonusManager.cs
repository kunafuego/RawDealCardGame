namespace RawDeal.Bonus;

public class BonusManager
{
    private List<Bonus> _damageBonusList { get; set; }
    private List<FortitudeBonus> _fortitudeBonusList { get; set; }
    private LastPlay _lastPlayInstance;

    public BonusManager(LastPlay lastPlayInstance)
    {
        _damageBonusList = new List<Bonus>();
        _fortitudeBonusList = new List<FortitudeBonus>();
        _lastPlayInstance = lastPlayInstance;
    }

    public void AddDamageBonus(Bonus bonus)
    {
        CheckIfBonusExpire(ExpireOptions.OneMoreCardWasPlayed);
        Console.WriteLine("DAMAGE BONUS ADDED");
        _damageBonusList.Add(bonus);
    }

    public void AddFortitudeBonus(FortitudeBonus bonus)
    {
        Console.WriteLine("FORTITUDE BONUS ADDED");
        _fortitudeBonusList.Add(bonus);
    }

    public int GetPlayDamage(Play playThatIsBeingPlayed, Player opponent)
    {
        Card cardThatIsBeingPlayed = playThatIsBeingPlayed.Card;
        int netDamage = cardThatIsBeingPlayed.GetDamage();
        Console.WriteLine($"\nObteniendo el damage que debería hacer la jugada, teniendo como damage inicial {netDamage}");
        foreach (Bonus damageBonus in _damageBonusList)
        {
            if (damageBonus.CheckIfBonusCanApplyToPlay(playThatIsBeingPlayed, opponent))
            {
                netDamage += damageBonus.GetBonusAmount();
                Console.WriteLine($"El damage aumentó a {netDamage}");
            }
        }
        return netDamage;
    }

    public int GetPlayFortitude(Card cardThatIsTryingToBePlayed, Card cardThatIWantToReverseWith)
    {
        int netFortitude = cardThatIWantToReverseWith.Fortitude;
        foreach (FortitudeBonus fortitudeBonus in _fortitudeBonusList)
        {
            if (fortitudeBonus.CheckIfBonusCanApplyToPlay(cardThatIsTryingToBePlayed))
                netFortitude += fortitudeBonus.GetBonusAmount();
        }
        Console.WriteLine($"El fortitude que requiere esta carta es: {netFortitude}");
        return netFortitude;
    }


    public void CheckIfBonusExpire(ExpireOptions expireOptions)
    {
        var expiredDamageBonuses = _damageBonusList.Where(damageBonus => damageBonus.CheckIfBonusExpired(expireOptions)).ToList();
        foreach (var damageBonus in expiredDamageBonuses)
        {
            _damageBonusList.Remove(damageBonus);
            Console.WriteLine("DAMAGE BONUS DELETED");
        }
    }
    
    public void CheckIfFortitudeBonusExpire(){
        var expiredFortitudeBonuses = _fortitudeBonusList.Where(fortitudeBonus => fortitudeBonus.CheckIfFortitudeBonusExpired()).ToList();
        foreach (var fortitudeBonus in expiredFortitudeBonuses)
        {
            _fortitudeBonusList.Remove(fortitudeBonus);
            Console.WriteLine("FORTITUDE BONUS DELETED");

        }
    }
    
    
}
