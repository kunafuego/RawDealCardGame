using RawDealView;
using System.Text.Json;

namespace RawDeal;

public class Game
{
    private View _view;
    private string _deckFolder;
    private Player _player1;
    private Player _player2;
    private Player _playerPlayingRound;
    private Player _winner;
    private bool _gameEnded;
    private SuperstarAbility _player1SuperstarAbility;
    private SuperstarAbility _player2SuperstarAbility;
    private Dictionary<string, SuperstarAbility> _abilities;

    public Game(View view, string deckFolder)
    {
        _view = view;
        _deckFolder = deckFolder;
        _gameEnded = false;
        _abilities = new Dictionary<string, SuperstarAbility>
        {
            {"THE ROCK", new TheRockAbility(_player1, _player2, _view)},
            {"CHRIS JERICHO", new ChrisJerichoAbility(_player1, _player2, _view)},
            {"KANE", new KaneAbility(_player1, _player2, _view)},
            {"MANKIND", new MankindAbility(_player1, _player2, _view)},
            {"THE UNDERTAKER", new TheUndertakerAbility(_player1, _player2, _view)},
            {"STONE COLD STEVE AUSTIN", new StoneColdAbility(_player1, _player2, _view)},
            {"HHH", new HHHAbility(_player1, _player2, _view)}
        };
    }

    public void Play()
    {
        CreatePlayers();
        bool decksAreValid = UsersSelectDecks();
        if (decksAreValid)
        {
            ChooseStarter();
            while (_gameEnded == false)
            {
                PlayRound();
                SwapPlayers();
            }
            if (_winner != _player1 && _winner != _player2) _winner = GetPlayerThatIsNotPlayingRound();
            _view.CongratulateWinner(_winner.Superstar.Name);
        }
    }
    private void CreatePlayers()
    {
        _player1 = new Player();
        _player2 = new Player();
    }

    private bool UsersSelectDecks()
    {
        List<Player> iteradorPlayers = new List<Player>(){ _player1, _player2 };
        foreach (var player in iteradorPlayers)
        {
            List<string> deckContent = GetPlayersDecks();
        
            Deck deck = CreateDeckObject(deckContent);
            if (!deck.IsValid())
            {
                _view.SayThatDeckIsInvalid();
                return false;
            }
            Superstar superstar = InitializeSuperstar(deckContent[0]);
            AssignDeckAndSuperstarToPlayer(player, deck, superstar);
            DrawCardsToHand(player);
        }

        AssignPlayersAbilities();
        return true;
    }
    private List<string> GetPlayersDecks()
    {
        string deckPath = _view.AskUserToSelectDeck(_deckFolder);
        string[] deckText = File.ReadAllLines(deckPath);
        return new List<string>(deckText);
    }

    private Deck CreateDeckObject(List<string> deckContent)
    {
        var deckListWithCardsNames = new List<string>(deckContent);
        var superstarName = deckListWithCardsNames[0];
        deckListWithCardsNames.Remove(superstarName);
        Superstar superstar = InitializeSuperstar(superstarName);
        Deck deck = InitializeCardsAndDeck(deckListWithCardsNames);
        deck.AssignSuperstar(superstar);
        return deck;
    }
    
    public Superstar InitializeSuperstar(string superstarName)
    {
        string superstarPath = Path.Combine("data", "superstar.json");
        string superstarInfo = File.ReadAllText(superstarPath);
        var superstarSerializer = JsonSerializer.Deserialize<List<DeserializedSuperstars>>(superstarInfo);
        var serializedSuperstar = superstarSerializer.Find(x => superstarName.Contains(x.Name));
        Superstar superstarObject = new Superstar(serializedSuperstar.Name, serializedSuperstar.Logo, serializedSuperstar.HandSize, serializedSuperstar.SuperstarValue,
            serializedSuperstar.SuperstarAbility);
        return superstarObject;
    }
    
    public Deck InitializeCardsAndDeck(List<string> deckListNames)
    {
        string cardsPath = Path.Combine("data", "cards.json");
        string cardsInfo = File.ReadAllText(cardsPath);
        var cardsSerializer = JsonSerializer.Deserialize<List<DeserializedCards>>(cardsInfo);
        List<Card> deckCards = new List<Card>();
        foreach (var card in deckListNames)
        {
            DeserializedCards deserializedCard = cardsSerializer.Find(x => x.Title == card);
            Card cardObject = new Card(deserializedCard.Title, deserializedCard.Types, deserializedCard.Subtypes, deserializedCard.Fortitude, 
                deserializedCard.Damage, deserializedCard.StunValue, deserializedCard.CardEffect);
            deckCards.Add((cardObject));
        }   
        Deck deckObject = new Deck(deckCards);
        return deckObject;
    }


    private void AssignDeckAndSuperstarToPlayer(Player player, Deck deck, Superstar superstar)
    {
        player.AssignArsenal(deck);
        player.AssignSuperstar(superstar);
    }

    private void DrawCardsToHand(Player player)
    {
        player.DrawCardsFromArsenalToHand();
    }
    
    private void AssignPlayersAbilities()
    {
        AssignPlayer1Ability();
        AssignPlayer2Ability();
    }

    private void AssignPlayer1Ability()
    {
        _player1SuperstarAbility = _abilities[_player1.Superstar.Name];

    }
    
    private void AssignPlayer2Ability()
    {
        _player2SuperstarAbility = _abilities[_player2.Superstar.Name];
    }

    private void ChooseStarter()
    {
        _playerPlayingRound = (_player1.Superstar.SuperstarValue >= _player2.Superstar.SuperstarValue)
            ? _player1
            : _player2;
    }

    
    private void PlayRound()
    {
        Player playerNotPlayingRound = GetPlayerThatIsNotPlayingRound();
        _view.SayThatATurnBegins(_playerPlayingRound.Superstar.Name);
        ManageEffectBeforeDraw();
        PlayerDrawCards();
        bool effectUsed = false;
        NextPlay nextPlayOptionChosen;
        do
        {
            _view.ShowGameInfo(_playerPlayingRound.GetInfo(), playerNotPlayingRound.GetInfo());
            nextPlayOptionChosen = AskUserWhatToDo(effectUsed);
            if (nextPlayOptionChosen == NextPlay.UseAbility) effectUsed = true;
            ManageChosenOption(nextPlayOptionChosen);
        } while (_gameEnded == false && nextPlayOptionChosen != NextPlay.EndTurn);
        CheckIfThereIsWinner();
        
    }
    private Player GetPlayerThatIsNotPlayingRound()
    {
        Player playerNotPlayingRound = (_playerPlayingRound == _player1) ? _player2 : _player1;
        return playerNotPlayingRound;
    }


    private void ManageEffectBeforeDraw()
    {
        var playerPlayingRoundSuperstarAbility =
            (_playerPlayingRound == _player1) ? _player1SuperstarAbility : _player2SuperstarAbility;
        if (playerPlayingRoundSuperstarAbility.MustUseEffectAtStartOfTurn(_playerPlayingRound))
        {
            _view.SayThatPlayerIsGoingToUseHisAbility(_playerPlayingRound.Superstar.Name,
                _playerPlayingRound.Superstar.SuperstarAbility);
            playerPlayingRoundSuperstarAbility.UseEffect(_playerPlayingRound, GetPlayerThatIsNotPlayingRound(), _view);
        }

        if (playerPlayingRoundSuperstarAbility.NeedToAskToUseAbilityAtBeginningOfTurn(_playerPlayingRound) &&
            playerPlayingRoundSuperstarAbility.MeetsTheRequirementsForUsingEffect(_playerPlayingRound))
        {
            bool wantsToUse = _view.DoesPlayerWantToUseHisAbility(_playerPlayingRound.Superstar.Name);
            if (wantsToUse)
            {
                playerPlayingRoundSuperstarAbility.UseEffect(_playerPlayingRound, GetPlayerThatIsNotPlayingRound(), _view);
            }
        }
    }
    
    private void PlayerDrawCards()
    {
        var playerPlayingRoundSuperstarAbility =
            (_playerPlayingRound == _player1) ? _player1SuperstarAbility : _player2SuperstarAbility;
        if (playerPlayingRoundSuperstarAbility.MustUseEffectDuringDrawSegment(_playerPlayingRound) &&
            playerPlayingRoundSuperstarAbility.MeetsTheRequirementsForUsingEffect(_playerPlayingRound))
        {
            playerPlayingRoundSuperstarAbility.UseEffect(_playerPlayingRound, GetPlayerThatIsNotPlayingRound(), _view);
        }
        else
        {
            _playerPlayingRound.MovesCardFromArsenalToHandInDrawSegment();
        }
    }

    
    private NextPlay AskUserWhatToDo(bool effectUsed)
    {
        NextPlay nextPlayChosen;
        var playerPlayingRoundSuperstarAbility =
            (_playerPlayingRound == _player1) ? _player1SuperstarAbility : _player2SuperstarAbility;
        if (playerPlayingRoundSuperstarAbility.NeedToAskToUseAbilityDuringTurn(_playerPlayingRound) &&
            playerPlayingRoundSuperstarAbility.MeetsTheRequirementsForUsingEffect(_playerPlayingRound) &&
            !effectUsed)
        {
            nextPlayChosen = _view.AskUserWhatToDoWhenUsingHisAbilityIsPossible();
        }
        else
        {
            nextPlayChosen = _view.AskUserWhatToDoWhenHeCannotUseHisAbility();
        }

        return nextPlayChosen;

    }
    
    private void ManageChosenOption(NextPlay optionChosen)
    {
        if (optionChosen == NextPlay.ShowCards)
        {
            ManageShowingCards();
        }
        else if (optionChosen == NextPlay.PlayCard)
        {
            ManagePlayingCards();
        }
        else if (optionChosen == NextPlay.UseAbility)
        {
            UseAbilityDuringTurn();
        }
        else if (optionChosen == NextPlay.GiveUp)
        {
            _gameEnded = true;
            _winner = GetPlayerThatIsNotPlayingRound();
        }
    }
    private void ManageShowingCards()
    {
        CardSet cardSetChosenForShowing = _view.AskUserWhatSetOfCardsHeWantsToSee();
        List<Card> cardsObjectsToShow = new List<Card>();
        if (cardSetChosenForShowing.ToString().Contains("Opponents"))
        {
            Player playerNotPlayingRound = GetPlayerThatIsNotPlayingRound();
            cardsObjectsToShow = playerNotPlayingRound.GetCardsToShow(cardSetChosenForShowing);
        }
        else
        {
            cardsObjectsToShow = _playerPlayingRound.GetCardsToShow(cardSetChosenForShowing);
        }

        List<string> cardsStringsToShow = GetCardsAsStringForShowing(cardsObjectsToShow);
        _view.ShowCards(cardsStringsToShow);
    }
    
    private List<string> GetCardsAsStringForShowing(List<Card> cardsObjectsToShow)
    {
        List<string> cardsStringsToShow = new List<string>();
        foreach (Card card in cardsObjectsToShow)
        {
            cardsStringsToShow.Add(card.ToString());
        }
        return cardsStringsToShow;
    }

    private void ManagePlayingCards()
    {
        List<Play> playsToShow = _playerPlayingRound.GetAvailablePlays();
        List<string> availabePlaysInStringFormat = GetStringsOfPlays(playsToShow);
        int chosenPlay = _view.AskUserToSelectAPlay(availabePlaysInStringFormat);
        if (chosenPlay != -1)
        {
            PlayCard(playsToShow[chosenPlay]);
        }
    }
    
    private List<string> GetStringsOfPlays(List<Play> availablePlays)
    {
        List<string> playsToShow = new List<string>();
        foreach (Play playObject in availablePlays)
        {
            playsToShow.Add(playObject.ToString());
        }

        return playsToShow;
    }

    private void PlayCard(Play chosenPlay)
    {
        Card cardPlayed = chosenPlay.Card;
        _view.SayThatPlayerIsTryingToPlayThisCard(_playerPlayingRound.Superstar.Name, chosenPlay.ToString());
        _view.SayThatPlayerSuccessfullyPlayedACard();
        _playerPlayingRound.MoveCardFromHandToRingArea(chosenPlay);
        if (chosenPlay.Type == "MANEUVER")
        {
            PlayManeuver(cardPlayed);
        }
    }

    private void PlayManeuver(Card cardPlayed)
    {
        Player playerNotPlayingRound = GetPlayerThatIsNotPlayingRound();
        int cardDamage = ManageCardDamage(cardPlayed.Damage);
        _view.SayThatOpponentWillTakeSomeDamage(playerNotPlayingRound.Superstar.Name, cardDamage);
        for (int i = 1; i <= cardDamage; i++)
        {
            if (!_gameEnded)
            {
                Card cardThatWentToRingSide = playerNotPlayingRound.ReceivesDamage();
                _view.ShowCardOverturnByTakingDamage(cardThatWentToRingSide.ToString(), i, cardDamage);
                _gameEnded = !playerNotPlayingRound.HasCardsInArsenal();
            }
            else
            {
                _winner = _playerPlayingRound;
            }
        }
        _playerPlayingRound.UpdateFortitude();
    }

    private int ManageCardDamage(int initialDamage)
    {
        var playerReceivingDamageSuperstarAbility =
            (_playerPlayingRound == _player1) ? _player2SuperstarAbility : _player1SuperstarAbility;
        if (playerReceivingDamageSuperstarAbility.MustUseEffectWhileReceivingDamage(GetPlayerThatIsNotPlayingRound()))
        {
            return initialDamage - 1;
        }

        return initialDamage;
    }
    
    
    private void UseAbilityDuringTurn()
    {
        var playerPlayingRoundSuperstarAbility =
            (_playerPlayingRound == _player1) ? _player1SuperstarAbility : _player2SuperstarAbility;
        _view.SayThatPlayerIsGoingToUseHisAbility(_playerPlayingRound.Superstar.Name,
            _playerPlayingRound.Superstar.SuperstarAbility);
        playerPlayingRoundSuperstarAbility.UseEffect(_playerPlayingRound, GetPlayerThatIsNotPlayingRound(), _view);

    }
    
    private void CheckIfThereIsWinner()
    {
        if (!_gameEnded && !_player1.HasCardsInArsenal())
        {
            _gameEnded = true;
            _winner = _player2;
        }
        else if (!_gameEnded && !_player2.HasCardsInArsenal())
        {
            _gameEnded = true;
            _winner = _player1;
        }
    }


    
    private void SwapPlayers()
    {
        _playerPlayingRound = GetPlayerThatIsNotPlayingRound();
    }

}