namespace RawDeal.Preconditions.PreconditionClasses;

public class ReverseJockeyingFromHand : Precondition
{
    public override bool DoesMeetPrecondition(Play playThatIsBeingPlayed, string askedFromDeskOrHand, int netDamageThatWillReceive)
    {
        Card cardThatIsBeingPlayed = playThatIsBeingPlayed.Card;
        if(askedFromDeskOrHand == "Hand") return cardThatIsBeingPlayed.Title == "Jockeying for Position";
        return false;
    }

}