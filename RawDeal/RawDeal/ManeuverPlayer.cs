using RawDealView;
namespace RawDeal;

public class ManeuverPlayer
{
    private readonly View _view;
    private readonly Player _playerPlayingRound;
    private readonly Player _playerNotPlayingRound;
    private bool _gameShouldEnd;
    private bool _turnEnded;
    private readonly EffectForNextMove _effectForThisMove;
    public ManeuverPlayer(View view, Player playerPlayingRound, Player playerNotPlayingRound, EffectForNextMove effectForThisMove)
    {
        _view = view;
        _playerPlayingRound = playerPlayingRound;
        _playerNotPlayingRound = playerNotPlayingRound;
        _gameShouldEnd = false;
        _turnEnded = false;
        _effectForThisMove = effectForThisMove;
    }

    public bool GameShouldEnd
    {
        get { return _gameShouldEnd; }
    }
    
    public bool TurnEnded
    {
        get { return _turnEnded; }
    }
    
    public void PlayManeuver(Card cardPlayed)
    {
        int cardTotalDamage = ManageCardDamage(cardPlayed);
        if (cardTotalDamage > 0)
        {
            _view.SayThatSuperstarWillTakeSomeDamage(_playerNotPlayingRound.GetSuperstarName(), cardTotalDamage);
            CheckIfGameAndTurnShouldEndWhileReceivingDamage();
        }
        for (int i = 1; i <= cardTotalDamage && !_gameShouldEnd; i++)
        {
            SayThatCardWasOverturned(i, cardTotalDamage);
            ReversalManager reversalPerformer = new ReversalManager(_view, _playerPlayingRound, _playerNotPlayingRound, _effectForThisMove);
            reversalPerformer.CheckIfManeuverCanBeReversedFromDeck(i, cardTotalDamage, cardPlayed);
            DealSingleCardDamage(i, cardTotalDamage);
        }
        _playerPlayingRound.MoveCardFromHandToRingArea(cardPlayed);
    }

    public void PlayReversalAsManeuver(Card cardPlayed)
    {
        int cardTotalDamage = ManageCardDamage(cardPlayed);
        if (cardTotalDamage > 0)
        {
            _view.SayThatSuperstarWillTakeSomeDamage(_playerNotPlayingRound.GetSuperstarName(), cardTotalDamage);
            CheckIfGameAndTurnShouldEndWhileReceivingDamage();
        }
        for (int i = 1; i <= cardTotalDamage && !_gameShouldEnd; i++)
        {
            SayThatCardWasOverturned(i, cardTotalDamage);
            DealSingleCardDamage(i, cardTotalDamage);
        }
        _playerPlayingRound.MoveCardFromHandToRingArea(cardPlayed);
    }
    
    private int ManageCardDamage(Card cardPlayed)
    {
        int initialDamage = cardPlayed.GetDamage();
        if (AbilitiesManager.CheckIfHasAbilityWhenReceivingDamage(_playerNotPlayingRound))
        {
            initialDamage -= 1;
        }
        int extraDamage = (cardPlayed.CheckIfSubtypesContain("Grapple")) ? _effectForThisMove.DamageChange : 0;
        return initialDamage + extraDamage;
    }
    
    private void CheckIfGameAndTurnShouldEndWhileReceivingDamage()
    {
        _gameShouldEnd = !_playerNotPlayingRound.HasCardsInArsenal();
        _turnEnded = _gameShouldEnd;
    }
    
    private void SayThatCardWasOverturned(int amountOfDamageReceivedAtMoment, int totalCardDamage)
    {
        Card cardThatWillGoToRingside = _playerNotPlayingRound.GetCardOnTopOfArsenal();
        _view.ShowCardOverturnByTakingDamage(cardThatWillGoToRingside.ToString(), amountOfDamageReceivedAtMoment, totalCardDamage);
    }

    private void DealSingleCardDamage(int cardIndex, int cardDamage)
    {
        _playerNotPlayingRound.MoveArsenalTopCardToRingside();
        if (cardIndex < cardDamage)
        {
            CheckIfGameAndTurnShouldEndWhileReceivingDamage();
        }
    }
}