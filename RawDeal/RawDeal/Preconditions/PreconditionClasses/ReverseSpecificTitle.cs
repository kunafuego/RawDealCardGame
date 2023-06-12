namespace RawDeal.Preconditions.PreconditionClasses;

public class ReverseSpecificTitle : Precondition
{
    private LastPlay _lastPlayInstance;
    private string _nameOfCard;
    public ReverseSpecificTitle(string nameOfCardThatCanReverse, LastPlay lastPlayInstance)
    {
        _nameOfCard = nameOfCardThatCanReverse;
        _lastPlayInstance = lastPlayInstance;
    }
    public override bool DoesMeetPrecondition(Player playerTryingToPlayCard, string askedFromDeskOrHand)
    {
        if (_lastPlayInstance.LastPlayPlayed is null) return true;
        if (askedFromDeskOrHand == "Checking To Play Action") return true;
        Play playThatYouWantToReverse = _lastPlayInstance.LastPlayPlayed;
        Card cardThatIsBeingPlayed = playThatYouWantToReverse.Card;
        return cardThatIsBeingPlayed.Title == _nameOfCard;
    }

}