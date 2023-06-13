namespace RawDeal.Preconditions.PreconditionClasses;

public class AfterXDOrGreaterManeuver : Precondition
{
    private LastPlay _lastPlayInstance;
    private int _damageRequired;
    public AfterXDOrGreaterManeuver(LastPlay lastPlayInstance, int DamageRequired)
    {
        _lastPlayInstance = lastPlayInstance;
        _damageRequired = DamageRequired;
    }
    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard, string askedFromDeskOrHand)
    {
        if (_lastPlayInstance.LastPlayPlayed is null) return true;
        if (_lastPlayInstance.LastPlayPlayed.Card.Title is "Tree of Woe" or "Austin Elbow Smash"
            or "Leaping Knee to the Face") return false;
        if (!_lastPlayInstance.WasItPlayedOnSameTurnThanActualPlay) return false;
        Play lastPlay = _lastPlayInstance.LastPlayPlayed;
        return _lastPlayInstance.ActualDamageMade >= _damageRequired && lastPlay.PlayedAs is "MANEUVER";
    }
}