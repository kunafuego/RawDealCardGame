using RawDeal.Bonus;
using RawDealView;
using RawDeal.Effects;
namespace RawDeal;

public class ManeuverPlayer
{
    private readonly View _view;
    private readonly Player _playerPlayingRound;
    private readonly Player _playerNotPlayingRound;
    private bool _gameShouldEnd;
    private bool _turnEnded;
    private readonly BonusManager _bonusManager;
    private readonly LastPlay _lastPlayInstance;

    public ManeuverPlayer(View view, Player playerPlayingRound, Player playerNotPlayingRound, LastPlay lastPlayInstance, BonusManager bonusManager)
    {
        _view = view;
        _playerPlayingRound = playerPlayingRound;
        _playerNotPlayingRound = playerNotPlayingRound;
        _gameShouldEnd = false;
        _turnEnded = false;
        _bonusManager = bonusManager;
        _lastPlayInstance = lastPlayInstance;
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
        int cardTotalDamage = ManageCardDamage(cardPlayed, "Maneuver");
        if (cardTotalDamage > 0)
        {
            _view.SayThatSuperstarWillTakeSomeDamage(_playerNotPlayingRound.GetSuperstarName(), cardTotalDamage);
            CheckIfGameAndTurnShouldEndWhileReceivingDamage();
        }
        for (int i = 1; i <= cardTotalDamage && !_gameShouldEnd; i++)
        {
            SayThatCardWasOverturned(i, cardTotalDamage);
            ReversalManager reversalPerformer = new ReversalManager(_view, _playerPlayingRound, _playerNotPlayingRound, _bonusManager, _lastPlayInstance);
            reversalPerformer.CheckIfManeuverCanBeReversedFromDeckWithASpecificCard(i, cardTotalDamage, cardPlayed);
            DealSingleCardDamage(i, cardTotalDamage);
        }
        _bonusManager.CheckIfFortitudeBonusExpire();
        _bonusManager.CheckIfBonusExpire(ExpireOptions.OneMoreCardWasPlayed);
        _playerPlayingRound.MoveCardFromHandToRingArea(cardPlayed);
    }

    public void PlayReversalAsManeuver(Card cardPlayed)
    {
        int cardTotalDamage = ManageCardDamage(cardPlayed, "Reversal");
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
    }
    
    private int ManageCardDamage(Card cardPlayed, string playedAs)
    {
        int damage = _bonusManager.GetPlayDamage(new Play(cardPlayed, playedAs), _playerNotPlayingRound);
        return damage;
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