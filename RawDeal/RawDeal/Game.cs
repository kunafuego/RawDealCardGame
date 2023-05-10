using RawDealView.Options;
using RawDealView;
using System.Text.Json;

namespace RawDeal;

public class Game
{
    private View _view;
    private string _deckFolder;
    private Player _player1 = new ();
    private Player _player2 = new ();
    private Player _playerPlayingRound;
    private Player _winner;
    private bool _gameEnded;
    private SuperstarAbility _player1SuperstarAbility;
    private SuperstarAbility _player2SuperstarAbility;
    private readonly Dictionary<string, SuperstarAbility> _abilities;

    public Game(View view, string deckFolder)
    {
        _view = view;
        _deckFolder = deckFolder;
        _gameEnded = false;
        _abilities = new Dictionary<string, SuperstarAbility>
        {
            {"THE ROCK", new TheRockAbility(_view)},
            {"CHRIS JERICHO", new ChrisJerichoAbility(_player1, _player2, _view)},
            {"KANE", new KaneAbility(_player1, _player2, _view)},
            {"MANKIND", new MankindAbility(_view)},
            {"THE UNDERTAKER", new TheUndertakerAbility(_view)},
            {"STONE COLD STEVE AUSTIN", new StoneColdAbility(_view)},
            {"HHH", new HHHAbility(_view)}
        };
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
    AssignPlayersAbilities();
    ChooseStarter();
    while (_gameEnded == false)
    {
        PlayRound();
        SwapPlayers();
    }
    if (_winner != _player1 && _winner != _player2) _winner = GetPlayerThatIsNotPlayingRound();
    Superstar winnerSuperstar = _winner.Superstar;
    _view.CongratulateWinner(winnerSuperstar.Name);
    }
    
    private void PlayersSelectTheirDecks()
    {
        List<Player> iteradorPlayers = new List<Player>(){ _player1, _player2 };
        foreach (var player in iteradorPlayers)
        {
            List<string> listOfStringsWithNamesOfCardsInDeck = AskPlayerToSelectDeck();
            Superstar superstar = GetDecksSuperstar(listOfStringsWithNamesOfCardsInDeck);
            RemoveSuperstarFromListOfDecksCards(listOfStringsWithNamesOfCardsInDeck);
            Deck deck = CreateDeckObject(listOfStringsWithNamesOfCardsInDeck);
            if (!deck.IsValid(superstar.Logo))
            {
                throw new InvalidDeckException("");
            }
            AssignDeckToPlayer(player, deck);
            AssignSuperstarToPlayer(player, superstar);
            DrawCardsToHandForFirstTime(player);
        }
    }
    
    private List<string> AskPlayerToSelectDeck()
    {
        string deckPath = _view.AskUserToSelectDeck(_deckFolder);
        string[] deckText = File.ReadAllLines(deckPath);
        return new List<string>(deckText);
    }

    private Superstar GetDecksSuperstar(List<string> listOfStringsWithDeckContent)
    {
        string superstarName = listOfStringsWithDeckContent[0];
        Superstar superstar = InitializeSuperstar(superstarName);
        return superstar;
    }

    private Superstar InitializeSuperstar(string superstarName)
    {
        string superstarInfo = ReadSuperstarInfo();
        DeserializedSuperstars serializedSuperstar = FindSerializedSuperstar(superstarName, superstarInfo);
        Superstar superstarObject = CreateSuperstarObject(serializedSuperstar);
        return superstarObject;
    }

    private string ReadSuperstarInfo()
    {
        string superstarPath = Path.Combine("data", "superstar.json");
        string superstarInfo = File.ReadAllText(superstarPath);
        return superstarInfo;
    }

    private DeserializedSuperstars FindSerializedSuperstar(string superstarName, string superstarInfo)
    {
        var superstarSerializer = JsonSerializer.Deserialize<List<DeserializedSuperstars>>(superstarInfo);
        var serializedSuperstar = superstarSerializer.Find(x => superstarName.Contains(x.Name));
        return serializedSuperstar;
    }

    private Superstar CreateSuperstarObject(DeserializedSuperstars serializedSuperstar)
    {
        Superstar superstarObject = new Superstar(serializedSuperstar.Name, serializedSuperstar.Logo, serializedSuperstar.HandSize, serializedSuperstar.SuperstarValue,
            serializedSuperstar.SuperstarAbility);
        return superstarObject;
    }
    
    private static void RemoveSuperstarFromListOfDecksCards(List<string> listOfStringsWithNamesOfCardsInDeck)
    {
        listOfStringsWithNamesOfCardsInDeck.RemoveAt(0);
    }
    
    private Deck CreateDeckObject(List<string> deckContent)
    {
        var deckListWithCardsNames = new List<string>(deckContent);
        Deck deck = CreateDeck(deckListWithCardsNames);
        return deck;
    }

    private Deck CreateDeck(List<string> deckListNamesWithCardNames)
    {
        var listWithDeserializedCards = LoadCards();
        List<Card> deckCards = new List<Card>();
        foreach (var card in deckListNamesWithCardNames)
        {
            DeserializedCards deserializedCard = listWithDeserializedCards.Find(x => x.Title == card);
            Card cardObject = CreateCard(deserializedCard);
            deckCards.Add(cardObject);
        }   
        Deck deckObject = new Deck(deckCards);
        return deckObject;
    }

    private List<DeserializedCards> LoadCards()
    {
        string cardsPath = Path.Combine("data", "cards.json");
        string cardsInfo = File.ReadAllText(cardsPath);
        var cardsSerializer = JsonSerializer.Deserialize<List<DeserializedCards>>(cardsInfo);
        return cardsSerializer;
    }
    
    private Card CreateCard(DeserializedCards deserializedCard)
    {
        return new Card(
            deserializedCard.Title, 
            deserializedCard.Types, 
            deserializedCard.Subtypes, 
            deserializedCard.Fortitude, 
            deserializedCard.Damage, 
            deserializedCard.StunValue, 
            deserializedCard.CardEffect);
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
        player.DrawCardsFromArsenalToHand();
    }
    
    private void AssignPlayersAbilities()
    {
        AssignPlayer1Ability();
        AssignPlayer2Ability();
    }

    private void AssignPlayer1Ability()
    {
        Superstar playersSuperstar = _player1.Superstar;
        _player1SuperstarAbility = _abilities[playersSuperstar.Name];

    }
    
    private void AssignPlayer2Ability()
    {
        Superstar playersSuperstar = _player2.Superstar;
        _player2SuperstarAbility = _abilities[playersSuperstar.Name];
    }

    private void ChooseStarter()
    {
        Superstar player1Superstar = _player1.Superstar;
        Superstar player2Superstar = _player2.Superstar;
        _playerPlayingRound = (player1Superstar.SuperstarValue >= player2Superstar.SuperstarValue)
            ? _player1
            : _player2;
    }
    
    private void PlayRound()
    {
        Player playerNotPlayingRound = GetPlayerThatIsNotPlayingRound();
        Superstar playerPlayingSuperstar = _playerPlayingRound.Superstar;
        _view.SayThatATurnBegins(playerPlayingSuperstar.Name);
        ManageEffectBeforeDraw();
        PlayerDrawCards();
        bool effectUsed = false;
        NextPlay nextPlayOptionChosen;
        do
        {
            _view.ShowGameInfo(_playerPlayingRound.GetInfo(), playerNotPlayingRound.GetInfo());
            if (!effectUsed && CheckIfUserCanUseEffect())
            {
                nextPlayOptionChosen = AskUserWhatToDoWhenUsingHisAbilityIsPossible();
            }
            else
            {
                nextPlayOptionChosen = AskUserWhatToDoWhenHeCannotUseHisAbility();
            }
            
            effectUsed |= (nextPlayOptionChosen == NextPlay.UseAbility);
            
            ManageChosenOption(nextPlayOptionChosen);
        } while (!GameEnded() && nextPlayOptionChosen != NextPlay.EndTurn);
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
        Superstar playerPlayingRoundSuperstar = _playerPlayingRound.Superstar;
        if (playerPlayingRoundSuperstarAbility.MustUseEffectAtStartOfTurn(_playerPlayingRound))
        {
            _view.SayThatPlayerIsGoingToUseHisAbility(playerPlayingRoundSuperstar.Name,
                playerPlayingRoundSuperstar.SuperstarAbility);
            playerPlayingRoundSuperstarAbility.UseEffect(_playerPlayingRound);
        }

        if (playerPlayingRoundSuperstarAbility.NeedToAskToUseAbilityAtBeginningOfTurn(_playerPlayingRound) &&
            playerPlayingRoundSuperstarAbility.MeetsTheRequirementsForUsingEffect(_playerPlayingRound))
        {
            bool wantsToUse = _view.DoesPlayerWantToUseHisAbility(playerPlayingRoundSuperstar.Name);
            if (wantsToUse)
            {
                playerPlayingRoundSuperstarAbility.UseEffect(_playerPlayingRound);
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
            playerPlayingRoundSuperstarAbility.UseEffect(_playerPlayingRound);
        }
        else
        {
            _playerPlayingRound.MovesCardFromArsenalToHandInDrawSegment();
        }
    }
    
    private bool CheckIfUserCanUseEffect()
    {
        var playerPlayingRoundSuperstarAbility =
            (_playerPlayingRound == _player1) ? _player1SuperstarAbility : _player2SuperstarAbility;
        if (playerPlayingRoundSuperstarAbility.NeedToAskToUseAbilityDuringTurn(_playerPlayingRound) &&
            playerPlayingRoundSuperstarAbility.MeetsTheRequirementsForUsingEffect(_playerPlayingRound))
        {
            return true;
        }
        return false;
    }

    private NextPlay AskUserWhatToDoWhenUsingHisAbilityIsPossible()
    {
        return _view.AskUserWhatToDoWhenUsingHisAbilityIsPossible();
    }

    private NextPlay AskUserWhatToDoWhenHeCannotUseHisAbility()
    {
        return _view.AskUserWhatToDoWhenHeCannotUseHisAbility();
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
        Superstar playerPlayingRoundSuperstar = _playerPlayingRound.Superstar;
        _view.SayThatPlayerIsTryingToPlayThisCard(playerPlayingRoundSuperstar.Name, chosenPlay.ToString());
        _view.SayThatPlayerSuccessfullyPlayedACard();
        _playerPlayingRound.MoveCardFromHandToRingArea(chosenPlay);
        if (chosenPlay.PlayedAs == "MANEUVER")
        {
            PlayManeuver(cardPlayed);
        }
    }

    private void PlayManeuver(Card cardPlayed)
    {
        Player playerNotPlayingRound = GetPlayerThatIsNotPlayingRound();
        int cardDamage = ManageCardDamage(cardPlayed.Damage);
        Superstar playerNotPlayingRoundSuperstar = playerNotPlayingRound.Superstar;
        _view.SayThatOpponentWillTakeSomeDamage(playerNotPlayingRoundSuperstar.Name, cardDamage);
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
        Superstar playerPlayingRoundSuperstar = _playerPlayingRound.Superstar;
        _view.SayThatPlayerIsGoingToUseHisAbility(playerPlayingRoundSuperstar.Name,
            playerPlayingRoundSuperstar.SuperstarAbility);
        playerPlayingRoundSuperstarAbility.UseEffect(_playerPlayingRound);

    }

    private bool GameEnded()
    {
        return _gameEnded;
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