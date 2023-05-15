using RawDeal.ReversalCards;

namespace RawDeal;

public class ReversalManager
{
    private Dictionary<string, ReversalCard> _effects;

    public ReversalManager()
    {
        _effects = new Dictionary<string, ReversalCard>
        {
            { "Rolling Takedown", new RollingTakedown() },
            { "Knee to the Gut", new KneeToTheGut() },
            { "Elbow to the Face", new ElbowToTheFace() },
            { "Manager Interferes", new ManagerInterferes() },
            { "Chyna Interferes", new ChynaInterferes() },
            { "Clean Break", new CleanBreak() },
            { "Jockeying for Position", new JockeyingForPosition() },
            { "Step Aside", new StepAside()},
            { "Escape Move", new EscapeMove()},
            { "Break the Hold", new BreakTheHold()},
            { "No Chance in Hell", new NoChanceInHell()}
        };
    }

    public void PerformEffect(Play playThatIsReversingManeuver, Player playerWhoReverse, Player playerBeingReversed)
    {
        Card cardThatIsReversingPlay = playThatIsReversingManeuver.Card;
        _effects[cardThatIsReversingPlay.Title].PerformEffect(playThatIsReversingManeuver.PlayedAs, playerWhoReverse, playerBeingReversed);
    }

    public bool CheckIfCanReverseThisPlay(Card cardThatCanPossibleReverse, Play playIsBeingMade)
    {
        ReversalCard reversalCardObject = _effects[cardThatCanPossibleReverse.Title];
        return reversalCardObject.CheckIfCanReversePlay(playIsBeingMade);
    }
}