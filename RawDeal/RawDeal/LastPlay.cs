namespace RawDeal;

public class LastPlay
{
    public Play LastPlayPlayed { get; set; }
    public bool WasItPlayedOnSameTurnThanActualPlay { get; set; }

    public bool WasItASuccesfulReversal { get; set; }
    public int ActualDamageMade { get; set; }
    public bool WasThisLastPlayAManeuverPlayedAfterIrishWhip { get; set; }
}