using RawDeal.Bonus;
using RawDeal.Bonus.BonusClasses;
using RawDealView;

namespace RawDeal;

public class Game
{
    private RoundManager _actualRoundManager;
    private BonusManager _bonusManager;
    private readonly string _deckFolder;
    private readonly LastPlay _lastPlayInstance = new();
    private Player _playerNotPlayingRound = new();
    private Player _playerPlayingRound = new();
    private readonly View _view;

    public Game(View view, string deckFolder)
    {
        _view = view;
        _deckFolder = deckFolder;
        InitiateLastPlayInstance();
        InitiateBonusManager();
    }

    public void Play()
    {
        try
        {
            TryToPlayGame();
        }
        catch (InvalidDeckException)
        {
            _view.SayThatDeckIsInvalid();
        }
    }

    private void TryToPlayGame()
    {
        PlayersSelectTheirDecks();
        SetAbilityManager();
        SwapPlayersIfNecessary();
        do
        {
            PlayRound();
            SwapPlayers();
        } while (_actualRoundManager.GameShouldEnd == false);

        var winner = _actualRoundManager.GetGameWinner();
        _view.CongratulateWinner(winner.GetSuperstarName());
    }

    private void PlayersSelectTheirDecks()
    {
        var deckSelectionManager =
            new DeckSelectionManager(_view, _deckFolder, _lastPlayInstance, _bonusManager);
        foreach (var player in new List<Player> { _playerPlayingRound, _playerNotPlayingRound })
            deckSelectionManager.SelectDeck(player);
    }

    private void SetAbilityManager()
    {
        AbilitiesManager.View = _view;
        AbilitiesManager.SetAbility(_playerPlayingRound);
        AbilitiesManager.SetAbility(_playerNotPlayingRound);
    }

    private void SwapPlayersIfNecessary()
    {
        var playerPlayingRoundSuperstar = _playerPlayingRound.Superstar;
        var playerNotPlayingRoundSuperstar = _playerNotPlayingRound.Superstar;
        if (playerPlayingRoundSuperstar.SuperstarValue <
            playerNotPlayingRoundSuperstar.SuperstarValue) SwapPlayers();
    }

    private void PlayRound()
    {
        _actualRoundManager = new RoundManager(_playerPlayingRound, _playerNotPlayingRound, _view
            , _lastPlayInstance, _bonusManager);
        _actualRoundManager.PlayRound();
        _actualRoundManager.CheckIfGameShouldEnd();
    }


    private void InitiateLastPlayInstance()
    {
        _lastPlayInstance.LastPlayPlayed = null;
        _lastPlayInstance.WasThisLastPlayAManeuverPlayedAfterIrishWhip = false;
        _lastPlayInstance.WasItPlayedOnSameTurnThanActualPlay = false;
        _lastPlayInstance.WasItASuccesfulReversal = false;
    }

    private void InitiateBonusManager()
    {
        _bonusManager = new BonusManager(_lastPlayInstance);
        Bonus.Bonus mankindBonus = new MankindDamageBonus();
        _bonusManager.AddDamageBonus(mankindBonus);
    }

    private void SwapPlayers()
    {
        (_playerPlayingRound, _playerNotPlayingRound) =
            (_playerNotPlayingRound, _playerPlayingRound);
    }
}