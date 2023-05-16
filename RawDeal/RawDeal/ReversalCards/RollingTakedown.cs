using RawDealView;
namespace RawDeal.ReversalCards;

public class RollingTakedown: ReversalCard
{
    public RollingTakedown(View view) : base(view) {}
    
    public override void PerformEffect(Play playThatWasReversed,  Card cardObject, Player playerThatReversePlay, Player playerThatWasReversed)
    {
        Card cardThatWasReversed = playThatWasReversed.Card;
        if (playThatWasReversed.PlayedAs == "REVERSED FROM DECK")
        {
            View.SayThatCardWasReversedByDeck(playerThatReversePlay.GetSuperstarName());
        }
        // View.SayThatCard
        View.SayThatOpponentWillTakeSomeDamage(playerThatWasReversed.GetSuperstarName(), cardThatWasReversed.GetDamage());
        for (int i = 1; i <= cardThatWasReversed.GetDamage(); i++)
        {
            Card cardThatWillGoToRingside = playerThatWasReversed.GetCardOnTopOfArsenal();
            View.ShowCardOverturnByTakingDamage(cardThatWillGoToRingside.ToString(), i, cardThatWasReversed.GetDamage());
            playerThatWasReversed.MoveArsenalTopCardToRingside();
        }
        playerThatReversePlay.MoveCardFromHandToRingArea(cardObject);
    }
    
    public override bool CheckIfCanReversePlay(Play playThatIsBeingPlayed, string askedFromDeskOrHand, int netDamageThatWillReceive)
    {
        Card cardThatIsBeingPlayed = playThatIsBeingPlayed.Card;
        List<string> cardsSubtypes = cardThatIsBeingPlayed.Subtypes;
        if (netDamageThatWillReceive <= 7 && playThatIsBeingPlayed.PlayedAs == "MANEUVER" && cardsSubtypes.Contains("Grapple"))
        {
            return true;
        }

        return false;
    }
}