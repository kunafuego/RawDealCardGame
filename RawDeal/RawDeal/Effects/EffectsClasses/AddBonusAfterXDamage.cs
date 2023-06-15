using RawDeal;
using RawDeal.Bonus;
using RawDeal.Effects;

public class AddBonusAfterXDamage : Effect
{
    private const int DamageThatShouldBeMade = 5;
    private readonly BonusManager _bonusManager;
    private readonly LastPlay _lastPlayInstance;

    public AddBonusAfterXDamage(BonusManager bonusManager, LastPlay lastPlayInstance)
    {
        _bonusManager = bonusManager;
        _lastPlayInstance = lastPlayInstance;
    }

    public override void Apply(Play actualPlay, Player playerThatPlayedCard, Player opponent)
    {
        if (_lastPlayInstance.LastPlayPlayed is null) return;
        if (!_lastPlayInstance.WasItPlayedOnSameTurnThanActualPlay) return;
        if (_lastPlayInstance.ActualDamageMade >= DamageThatShouldBeMade)
            _bonusManager.AddDamageBonus(new SuperKickBonus());
    }
}