namespace RawDeal;

public class GameEndedBecauseOfCollateralDamage : Exception
{
    public GameEndedBecauseOfCollateralDamage(string message) : base(message)
    {
    }
}