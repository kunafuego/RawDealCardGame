using RawDeal.ReversalCards;
using RawDealView;

namespace RawDeal;

public class ReversalManager
{
    private View _view;
    private Dictionary<string, ReversalCard> _effects;

    public ReversalManager(View view)
    {
        _view = view;
        _effects = new Dictionary<string, ReversalCard>
        {
            { "Rolling Takedown", new RollingTakedown(view) },
            { "Knee to the Gut", new KneeToTheGut(view) },
            { "Elbow to the Face", new ElbowToTheFace(view) },
            { "Manager Interferes", new ManagerInterferes(view) },
            { "Chyna Interferes", new ChynaInterferes(view) },
            { "Clean Break", new CleanBreak(view) },
            { "Jockeying for Position", new JockeyingForPosition(view) },
            { "Step Aside", new StepAside(view)},
            { "Escape Move", new EscapeMove(view)},
            { "Break the Hold", new BreakTheHold(view)},
            { "No Chance in Hell", new NoChanceInHell(view)}
        };
    }

    public void PerformEffect(Play playThatIsBeingReversed, Card cardThatIsReversingManeuver, Player playerWhoReverse, Player playerBeingReversed)
    {
        _effects[cardThatIsReversingManeuver.Title].PerformEffect(playThatIsBeingReversed, cardThatIsReversingManeuver, playerWhoReverse, playerBeingReversed);
    }

    public bool CheckIfCanReverseThisPlay(Card cardThatCanPossibleReverse, Play playIsBeingMade, string askedFromDeckOrHand, int netDamageThatWillReceive)
    {
        ReversalCard reversalCardObject = _effects[cardThatCanPossibleReverse.Title];
        return reversalCardObject.CheckIfCanReversePlay(playIsBeingMade, askedFromDeckOrHand, netDamageThatWillReceive);
    }
    
}