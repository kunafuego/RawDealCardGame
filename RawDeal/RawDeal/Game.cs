using System.Text;
using RawDealView.Options;
using RawDealView;
using System.Text.Json;

namespace RawDeal;

public class Game
{
    private View _view;
    private string _deckFolder;
    private Player _playerPlayingRound = new ();
    private Player _playerNotPlayingRound = new ();
    private RoundManager _actualRoundManager;

    public Game(View view, string deckFolder)
    {
        _view = view;
        _deckFolder = deckFolder;
    }

    public void Play()
    {
        try
        {
            TryToPlayGame();
        }
        catch (InvalidDeckException e)
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
        Superstar winnerSuperstar = winner.Superstar;
        _view.CongratulateWinner(winnerSuperstar.Name);
    }
    
    private void PlayersSelectTheirDecks()
    {
        List<Player> iteradorPlayers = new List<Player>(){ _playerPlayingRound, _playerNotPlayingRound };
        foreach (var player in iteradorPlayers)
        {
            List<string> listOfStringsWithNamesOfCardsInDeck = AskPlayerToSelectDeck();
            Deck deck = DeckCreator.InitializeDeck(listOfStringsWithNamesOfCardsInDeck);
            Superstar superstar = DeckCreator.GetDeckSuperstar();
            if (!deck.IsValid(superstar.Logo))
            {
                throw new InvalidDeckException("");
            }
            // Sacarlos de acá
            AssignDeckToPlayer(player, deck);
            AssignSuperstarToPlayer(player, DeckCreator.GetDeckSuperstar());
            DrawCardsToHandForFirstTime(player);
        }
    }
    
    private List<string> AskPlayerToSelectDeck()
    {
        string deckPath = _view.AskUserToSelectDeck(_deckFolder);
        string[] deckText = File.ReadAllLines(deckPath);
        return new List<string>(deckText);
    }

    private void AssignDeckToPlayer(Player player, Deck deck)
    {
        player.AssignArsenal(deck);
    }
    
    private void AssignSuperstarToPlayer(Player player, Superstar superstar)
    {
        player.AssignSuperstar(superstar);
    }
    
    private void DrawCardsToHandForFirstTime(Player player)
    {
        player.DrawCardsFromArsenalToHandAtStart();
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
        _actualRoundManager = new RoundManager(_playerPlayingRound, _playerNotPlayingRound, _view, effectForNextRound);
        _actualRoundManager.PlayRound();
        _actualRoundManager.CheckIfGameShouldEnd();
    }

    private EffectForNextMove GetPossibleEffectFromLastRound()
    {
        try
        {
            return _actualRoundManager.NextMoveEffect;
        }
        catch (NullReferenceException e)
        {
            return new EffectForNextMove(0, 0);
        }
    }
    
    private void SwapPlayers()
    {
        (_playerPlayingRound, _playerNotPlayingRound) = (_playerNotPlayingRound, _playerPlayingRound);
    }

}