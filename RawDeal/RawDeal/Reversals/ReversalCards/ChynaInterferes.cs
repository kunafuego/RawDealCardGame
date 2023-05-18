using RawDealView;
namespace RawDeal.ReversalCards;

public class ChynaInterferes: ReversalCard
{
    private readonly int _amountOfCardsToDrawInEffect;
    public ChynaInterferes(View view) : base(view)
    {
        _amountOfCardsToDrawInEffect = 2;
    }

    public override void PerformEffect(Play playThatWasReversed,  Card cardObject, Player playerThatReversePlay, Player playerThatWasReversed)
    {
        if (playThatWasReversed.PlayedAs == "Reversed From Hand")
        {
            View.SayThatPlayerDrawCards(playerThatReversePlay.GetSuperstarName(), _amountOfCardsToDrawInEffect);
            for (int i = 0; i < _amountOfCardsToDrawInEffect; i++)
            {
                playerThatReversePlay.DrawSingleCard();
            }
        }     
        ManeuverPlayer maneuverPlayer = new ManeuverPlayer(View, playerThatReversePlay, playerThatWasReversed, new EffectForNextMove(0,0));
        maneuverPlayer.PlayReversalAsManeuver(cardObject);
    }
    
    public override bool CheckIfCanReversePlay(Play playThatIsBeingPlayed, string askedFromDeskOrHand, int netDamageThatWillReceive)
    {
        return playThatIsBeingPlayed.PlayedAs == "MANEUVER";
    }
}