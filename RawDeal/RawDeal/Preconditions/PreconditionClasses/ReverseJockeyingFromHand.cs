namespace RawDeal.Preconditions.PreconditionClasses;

public class ReverseJockeyingFromHand : Precondition
{
    private LastPlay _lastPlayInstance;
    public ReverseJockeyingFromHand(LastPlay lastPlayInstance)
    {
        _lastPlayInstance = lastPlayInstance;
    }
    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard, string askedFromDeskOrHand, int netDamageThatWillReceive)
    {
        Play possibleLastPlay = _lastPlayInstance.LastPlayPlayed;
        Card cardThatIsBeingPlayed = possibleLastPlay.Card;
        if(askedFromDeskOrHand == "Hand")    return cardThatIsBeingPlayed.Title == "Jockeying for Position";
        return false;
    }

}