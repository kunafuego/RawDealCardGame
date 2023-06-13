using RawDeal.Bonus;
using RawDeal.Bonus.BonusClasses;
using RawDealView;

namespace RawDeal;

public class Game
{
    private View _view;
    private string _deckFolder;
    private Player _playerPlayingRound = new ();
    private Player _playerNotPlayingRound = new ();
    private LastPlay _lastPlayInstance = new LastPlay();
    private BonusManager _bonusManager;
    private RoundManager _actualRoundManager;

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
        Player winner = _actualRoundManager.GetGameWinner();
        _view.CongratulateWinner(winner.GetSuperstarName());
    }
    
    private void PlayersSelectTheirDecks()
    {
        DeckSelectionManager deckSelectionManager = new DeckSelectionManager(_view, _deckFolder, _lastPlayInstance, _bonusManager);
        foreach (var player in new List<Player>() { _playerPlayingRound, _playerNotPlayingRound })
        {
            deckSelectionManager.SelectDeck(player);
        }
    }

    private void SetAbilityManager()
    {
        AbilitiesManager.View = _view;
        AbilitiesManager.SetAbility(_playerPlayingRound) ;
        AbilitiesManager.SetAbility(_playerNotPlayingRound) ;
    }

    private void SwapPlayersIfNecessary()
    {
        Superstar playerPlayingRoundSuperstar = _playerPlayingRound.Superstar;
        Superstar playerNotPlayingRoundSuperstar = _playerNotPlayingRound.Superstar;
        if (playerPlayingRoundSuperstar.SuperstarValue < playerNotPlayingRoundSuperstar.SuperstarValue) SwapPlayers();
    }
    
    private void PlayRound()
    {
        EffectForNextMove effectForNextRound = GetPossibleEffectFromLastRound();
        _actualRoundManager = new RoundManager(_playerPlayingRound, _playerNotPlayingRound, _view, 
            effectForNextRound, _lastPlayInstance, _bonusManager);
        _actualRoundManager.PlayRound();
        _actualRoundManager.CheckIfGameShouldEnd();
    }

    private EffectForNextMove GetPossibleEffectFromLastRound()
    {
        try
        {
            return _actualRoundManager.NextMoveEffect;
        }
        catch (NullReferenceException)
        {
            return new EffectForNextMove(0, 0);
        }
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
        (_playerPlayingRound, _playerNotPlayingRound) = (_playerNotPlayingRound, _playerPlayingRound);
    }

}