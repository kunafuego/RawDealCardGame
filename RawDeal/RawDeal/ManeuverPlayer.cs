using RawDeal.Bonus;
using RawDealView;

namespace RawDeal;

public class ManeuverPlayer
{
    private readonly BonusManager _bonusManager;
    private readonly LastPlay _lastPlayInstance;
    private readonly Player _playerNotPlayingRound;
    private readonly Player _playerPlayingRound;
    private readonly View _view;

    public ManeuverPlayer(View view, Player playerPlayingRound, Player playerNotPlayingRound,
        LastPlay lastPlayInstance, BonusManager bonusManager)
    {
        _view = view;
        _playerPlayingRound = playerPlayingRound;
        _playerNotPlayingRound = playerNotPlayingRound;
        GameShouldEnd = false;
        TurnEnded = false;
        _bonusManager = bonusManager;
        _lastPlayInstance = lastPlayInstance;
    }

    public bool GameShouldEnd { get; private set; }

    public bool TurnEnded { get; private set; }

    public void PlayManeuver(Card cardPlayed)
    {
        var cardTotalDamage = ManageCardDamage(cardPlayed, "Maneuver");
        if (cardTotalDamage > 0)
        {
            _view.SayThatSuperstarWillTakeSomeDamage(_playerNotPlayingRound.GetSuperstarName(),
                cardTotalDamage);
            CheckIfGameAndTurnShouldEndWhileReceivingDamage();
        }

        for (var i = 1; i <= cardTotalDamage && !GameShouldEnd; i++)
        {
            SayThatCardWasOverturned(i, cardTotalDamage);
            var reversalPerformer = new ReversalManager(_view, _playerPlayingRound,
                _playerNotPlayingRound, _bonusManager, _lastPlayInstance);
            reversalPerformer.CheckIfManeuverCanBeReversedFromDeckWithASpecificCard(i,
                cardTotalDamage, cardPlayed);
            DealSingleCardDamage(i, cardTotalDamage);
        }

        SetBonusManager();
        SetLastPlay(cardTotalDamage);
        _playerPlayingRound.MoveCardFromHandToRingArea(cardPlayed);
    }

    public void PlayReversalAsManeuver(Card cardPlayed)
    {
        var cardTotalDamage = ManageCardDamage(cardPlayed, "Reversal");
        if (cardTotalDamage > 0)
        {
            _view.SayThatSuperstarWillTakeSomeDamage(_playerNotPlayingRound.GetSuperstarName(),
                cardTotalDamage);
            CheckIfGameAndTurnShouldEndWhileReceivingDamage();
        }

        for (var i = 1; i <= cardTotalDamage && !GameShouldEnd; i++)
        {
            SayThatCardWasOverturned(i, cardTotalDamage);
            DealSingleCardDamage(i, cardTotalDamage);
        }
    }

    private int ManageCardDamage(Card cardPlayed, string playedAs)
    {
        var damage =
            _bonusManager.GetPlayDamage(new Play(cardPlayed, playedAs), _playerNotPlayingRound);
        return damage;
    }

    private void CheckIfGameAndTurnShouldEndWhileReceivingDamage()
    {
        GameShouldEnd = !_playerNotPlayingRound.HasCardsInArsenal();
        TurnEnded = GameShouldEnd;
    }

    private void SayThatCardWasOverturned(int amountOfDamageReceivedAtMoment, int totalCardDamage)
    {
        var cardThatWillGoToRingside = _playerNotPlayingRound.GetCardOnTopOfArsenal();
        _view.ShowCardOverturnByTakingDamage(cardThatWillGoToRingside.ToString(),
            amountOfDamageReceivedAtMoment, totalCardDamage);
    }

    private void DealSingleCardDamage(int cardIndex, int cardDamage)
    {
        _playerNotPlayingRound.MoveArsenalTopCardToRingside();
        if (cardIndex < cardDamage) CheckIfGameAndTurnShouldEndWhileReceivingDamage();
    }

    private void SetBonusManager()
    {
        _bonusManager.CheckIfFortitudeBonusExpire();
        _bonusManager.CheckIfBonusExpire(ExpireOptions.OneMoreCardWasPlayed);
    }

    private void SetLastPlay(int cardTotalDamage)
    {
        _lastPlayInstance.ActualDamageMade = cardTotalDamage;
    }


}