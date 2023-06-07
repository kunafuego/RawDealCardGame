namespace RawDeal.Preconditions.PreconditionClasses;

public class ReverseJockeyingForPosition : Precondition
{
    public override bool DoesMeetPrecondition(Play playThatIsBeingPlayed, string askedFromDeskOrHand, int netDamageThatWillReceive)
    {
        Card cardThatIsBeingPlayed = playThatIsBeingPlayed.Card;
        return cardThatIsBeingPlayed.Title == "Jockeying for Position";
        
    }

}