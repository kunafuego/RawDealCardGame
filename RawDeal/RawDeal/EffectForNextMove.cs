namespace RawDeal;

public struct EffectForNextMove
{
    public readonly int DamageChange;
    public readonly int FortitudeChange;

    public EffectForNextMove(int damage, int fortitude)
    {
        DamageChange = damage;
        FortitudeChange = fortitude;
    }
}