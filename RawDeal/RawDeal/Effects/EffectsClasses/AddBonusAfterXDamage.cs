using RawDeal;
using RawDeal.Bonus;
using RawDeal.Bonus.BonusClasses;
using RawDeal.Effects;
using RawDealView;

public class AddBonusAfterXDamage : Effect
{
    private BonusManager _bonusManager;
    private LastPlay _lastPlayInstance;
    private const int DamageThatShouldBeMade = 5;
    
    public AddBonusAfterXDamage(BonusManager bonusManager, LastPlay lastPlayInstance)
    {
        _bonusManager = bonusManager;
        _lastPlayInstance = lastPlayInstance;
    }
    
    public override void Apply(Play actualPlay, View view, Player playerThatPlayedCard, Player opponent)
    {
        if (_lastPlayInstance.LastPlayPlayed is null) return;
        if (!_lastPlayInstance.WasItPlayedOnSameTurnThanActualPlay) return;
        Console.WriteLine($"El damage hecho por la última maniobra fue {_lastPlayInstance.ActualDamageMade}");
        if(_lastPlayInstance.ActualDamageMade >= DamageThatShouldBeMade) 
            _bonusManager.AddDamageBonus(new SuperKickBonus());
    }
}