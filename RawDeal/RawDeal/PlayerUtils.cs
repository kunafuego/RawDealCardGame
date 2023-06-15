namespace RawDeal;

public static class PlayerUtils
{
    public static PlaysList GetAvailablePlays(Player player)
    {
        var hand = player.Hand;
        var playsThatCanBePlayed = new PlaysList();
        foreach (var card in hand.Cards)
            if (card.Title == "Undertaker's Tombstone Piledriver")
                CheckIfAddUndertakerTombstone(card, playsThatCanBePlayed, player.Fortitude);
            else if (CanPlayCard(card, player)) AddPlayablePlays(card, playsThatCanBePlayed);

        return playsThatCanBePlayed;
    }

    private static bool CanPlayCard(Card card, Player player)
    {
        var cardEffectPrecondition = card.Precondition;
        var meetsPrecondition =
            cardEffectPrecondition.DoesMeetPrecondition(player, "Checking To Play Action");
        return card.Fortitude <= player.Fortitude && meetsPrecondition;
    }

    private static void AddPlayablePlays(Card card, PlaysList playsThatCanBePlayed)
    {
        foreach (var type in card.Types)
        {
            if (type == "Reversal") continue;
            var play = new Play(card, type);
            playsThatCanBePlayed.Add(play);
        }
    }

    private static void CheckIfAddUndertakerTombstone(Card card, PlaysList playsThatCanBePlayed,
        int fortitude)
    {
        if (fortitude >= 0) playsThatCanBePlayed.Add(new Play(card, "Action"));
        if (fortitude >= 30) playsThatCanBePlayed.Add(new Play(card, "Maneuver"));
    }
}