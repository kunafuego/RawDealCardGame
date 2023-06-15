using RawDeal.Bonus;
using RawDealView;
using RawDealView.Options;

namespace RawDeal;

public class RoundManager
{
    private readonly BonusManager _bonusManager;
    private readonly LastPlay _lastPlayInstance;
    private readonly Player _playerNotPlayingRound;
    private readonly Player _playerPlayingRound;
    private bool _turnEnded;
    private readonly View _view;

    public RoundManager(Player playerPlayingRound, Player playerNotPlayingRound, View view,
        LastPlay lastPlayInstance, BonusManager bonusManager)
    {
        GameShouldEnd = false;
        _turnEnded = false;
        _playerPlayingRound = playerPlayingRound;
        _playerNotPlayingRound = playerNotPlayingRound;
        _view = view;
        _lastPlayInstance = lastPlayInstance;
        _bonusManager = bonusManager;
    }

    public bool GameShouldEnd { get; private set; }

    public void PlayRound()
    {
        _turnEnded = false;
        _view.SayThatATurnBegins(_playerPlayingRound.GetSuperstarName());
        AbilitiesManager.ManageAbilityBeforeDraw(_playerPlayingRound, _playerNotPlayingRound);
        PlayerDrawCards();
        var effectUsed = false;
        do
        {
            NextPlay nextPlayOptionChosen;
            _view.ShowGameInfo(_playerPlayingRound.GetInfo(), _playerNotPlayingRound.GetInfo());
            if (!effectUsed &&
                AbilitiesManager.CheckIfUserCanUseAbilityDuringTurn(_playerPlayingRound))
                nextPlayOptionChosen = _view.AskUserWhatToDoWhenUsingHisAbilityIsPossible();
            else
                nextPlayOptionChosen = _view.AskUserWhatToDoWhenHeCannotUseHisAbility();
            effectUsed |= nextPlayOptionChosen == NextPlay.UseAbility;
            ManageChosenOption(nextPlayOptionChosen);
        } while (!_turnEnded);

        if (!_lastPlayInstance.WasItASuccesfulReversal)
        {
            _bonusManager.CheckIfBonusExpire(ExpireOptions.EndOfTurn);
            _bonusManager.CheckIfFortitudeBonusExpire();
        }

        _lastPlayInstance.WasItPlayedOnSameTurnThanActualPlay =
            _lastPlayInstance.WasItASuccesfulReversal;
    }

    private void PlayerDrawCards()
    {
        var userCanUseAbility =
            AbilitiesManager.CheckIfUserCanUseAbilityDuringDrawSection(_playerPlayingRound);
        if (userCanUseAbility)
            AbilitiesManager.UseAbilityDuringDrawSegment(_playerPlayingRound,
                _playerNotPlayingRound);
        else
            _playerPlayingRound.DrawSingleCard();
    }

    private void ManageChosenOption(NextPlay optionChosen)
    {
        switch (optionChosen)
        {
            case NextPlay.ShowCards:
                ManageShowingCards();
                break;
            case NextPlay.PlayCard:
                ManagePlayingCards();
                break;
            case NextPlay.UseAbility:
                UseAbilityDuringTurn();
                break;
            case NextPlay.EndTurn:
                EndTurn();
                break;
            case NextPlay.GiveUp:
                GiveUp();
                break;
        }
    }

    private void ManageShowingCards()
    {
        var cardsShower = new CardsShower(_view, _playerPlayingRound, _playerNotPlayingRound);
        cardsShower.ManageShowingCards();
    }

    private void ManagePlayingCards()
    {
        var cardPlayer = new CardPlayer(_view, _playerPlayingRound, _playerNotPlayingRound,
            _lastPlayInstance, _bonusManager);
        cardPlayer.ManagePlayingCards();
        _turnEnded = cardPlayer.TurnEnded;
        GameShouldEnd = cardPlayer.GameShouldEnd;
    }

    private void UseAbilityDuringTurn()
    {
        AbilitiesManager.UseAbilityDuringTurn(_playerPlayingRound, _playerNotPlayingRound);
    }

    private void EndTurn()
    {
        _turnEnded = true;
    }

    private void GiveUp()
    {
        _turnEnded = true;
        GameShouldEnd = true;
    }

    public void CheckIfGameShouldEnd()
    {
        if (!_playerPlayingRound.HasCardsInArsenal() || !_playerNotPlayingRound.HasCardsInArsenal())
            GameShouldEnd = true;
    }

    public Player GetGameWinner()
    {
        if (!_playerNotPlayingRound.HasCardsInArsenal()) return _playerPlayingRound;
        return _playerNotPlayingRound;
    }
}