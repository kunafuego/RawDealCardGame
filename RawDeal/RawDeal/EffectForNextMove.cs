namespace RawDeal;

public struct EffectForNextMove
{
    public int DamageChange;
    public int FortitudeChange;

    public EffectForNextMove(int damage, int fortitude)
    {
        DamageChange = damage;
        FortitudeChange = fortitude;
    }
}