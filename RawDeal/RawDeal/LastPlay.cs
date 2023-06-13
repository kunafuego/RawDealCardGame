namespace RawDeal;

public class LastPlay
{
    public Play LastPlayPlayed { get; set; }
    public bool WasItPlayedOnSameTurnThanActualPlay { get; set; }
    
    public bool WasItASuccesfulReversal { get; set; }
    public int ActualDamageMade { get; set; }
    public bool WasThisLastPlayAManeuverPlayedAfterIrishWhip { get; set; }


    public override string ToString()
    {
        if(LastPlayPlayed != null) 
            return
            $"LastPlayPlayed: {Convert.ToString(LastPlayPlayed.Card)} Was it played on the same turn: {WasItPlayedOnSameTurnThanActualPlay} IrishWhip: {WasThisLastPlayAManeuverPlayedAfterIrishWhip}, Damage: {ActualDamageMade}";
        return "";
    }
}