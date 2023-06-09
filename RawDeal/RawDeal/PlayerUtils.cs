namespace RawDeal;

public static class PlayerUtils
{
    public static List<Play> GetAvailablePlays(Deck hand, int fortitude)
    {
        List<Play> playsThatCanBePlayed = new List<Play>();
        foreach (Card card in hand.Cards)
        {
            if (card.Title == "Undertaker's Tombstone Piledriver") 
                CheckIfAddUndertakerTombstone(card, playsThatCanBePlayed, fortitude);
            else if (CanPlayCard(card, fortitude))
            {
                AddPlayablePlays(card, playsThatCanBePlayed);
            }
        }

        return playsThatCanBePlayed;
    }
    
    private static bool CanPlayCard(Card card, int fortitude)
    {
        return card.Fortitude <= fortitude;
    }

    private static void AddPlayablePlays(Card card, List<Play> playsThatCanBePlayed)
    {
        foreach (string type in card.Types)
        {
            if (type == "Reversal") continue;
            Play play = new Play(card, type);
            playsThatCanBePlayed.Add(play);
        }
    }

    private static void CheckIfAddUndertakerTombstone(Card card, List<Play> playsThatCanBePlayed, int fortitude)
    {
        if(fortitude >= 0) playsThatCanBePlayed.Add(new Play(card, "Action"));
        if(fortitude >= 30) playsThatCanBePlayed.Add(new Play(card, "Maneuver"));
    }
    
}