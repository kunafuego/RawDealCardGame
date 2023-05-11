using RawDealView.Options;
using RawDealView;
namespace RawDeal;

public class RoundManager
{
    private bool _gameShouldEnd;
    private bool _turnEnded;
    private Player _playerPlayingRound;
    private Player _playerNotPlayingRound;
    private View _view;
    
    public RoundManager(Player playerPlayingRound, Player playerNotPlayingRound, View view)
    {
        _gameShouldEnd = false;
        _turnEnded = false;
        _playerPlayingRound = playerPlayingRound;
        _playerNotPlayingRound = playerNotPlayingRound;
        _view = view;
    }

    public bool GameShouldEnd
    {
        get { return _gameShouldEnd; }
    }
    
    public void PlayRound()
    {
        _turnEnded = false;
        Superstar playerPlayingSuperstar = _playerPlayingRound.Superstar;
        _view.SayThatATurnBegins(playerPlayingSuperstar.Name);
        AbilitiesManager.ManageAbilityBeforeDraw(_playerPlayingRound, _playerNotPlayingRound);
        PlayerDrawCards();
        bool effectUsed = false;
        NextPlay nextPlayOptionChosen;
        do
        {
            _view.ShowGameInfo(_playerPlayingRound.GetInfo(), _playerNotPlayingRound.GetInfo());
            if (!effectUsed && AbilitiesManager.CheckIfUserCanUseAbilityDuringTurn(_playerPlayingRound))
            {
                nextPlayOptionChosen = _view.AskUserWhatToDoWhenUsingHisAbilityIsPossible();
            }
            else
            {
                nextPlayOptionChosen = _view.AskUserWhatToDoWhenHeCannotUseHisAbility();
            }
            
            effectUsed |= (nextPlayOptionChosen == NextPlay.UseAbility);
            
            ManageChosenOption(nextPlayOptionChosen);
        } while (!_turnEnded);
    }
    
    private void PlayerDrawCards()
    {
        try
        {
            TryToUseEffectDuringDrawSegment();
        }
        catch (CantUseAbilityException)
        {
            _playerPlayingRound.DrawSingleCard();
        }
    }
    
    private void TryToUseEffectDuringDrawSegment()
    {
        AbilitiesManager.UseAbilityDuringDrawSegment(_playerPlayingRound, _playerNotPlayingRound);
    }
    
    private void ManageChosenOption(NextPlay optionChosen)
    {
        if (optionChosen == NextPlay.ShowCards)
        {
            Console.WriteLine("PARTIENDO A MOSTRAR CARTAS 81");
            Console.WriteLine(_turnEnded);
            ManageShowingCards();
        }
        else if (optionChosen == NextPlay.PlayCard)
        {
            ManagePlayingCards();
        }
        else if (optionChosen == NextPlay.UseAbility)
        {
            AbilitiesManager.UseAbilityDuringTurn(_playerPlayingRound, _playerNotPlayingRound);
        }
        else if (optionChosen == NextPlay.EndTurn)
        {
            _turnEnded = true;
            Console.WriteLine("TURN ENDED PORQUE ACABO EL TURNO ELEGIDO");
        }
        else if (optionChosen == NextPlay.GiveUp)
        {
            _turnEnded = true;
            _gameShouldEnd = true;
            Console.WriteLine("TURN ENDED PORQUE SE RINDIO");
            // _winner = _playerNotPlayingRound;
        }
    }
    
    private void ManageShowingCards()
    {
        CardSet cardSetChosenForShowing = _view.AskUserWhatSetOfCardsHeWantsToSee();
        List<Card> cardsObjectsToShow = new List<Card>();
        if (cardSetChosenForShowing.ToString().Contains("Opponents"))
        {
            cardsObjectsToShow = _playerNotPlayingRound.GetCardsToShow(cardSetChosenForShowing);
        }
        else
        {
            cardsObjectsToShow = _playerPlayingRound.GetCardsToShow(cardSetChosenForShowing);
        }
        Console.WriteLine(_turnEnded);
        List<string> cardsStringsToShow = GetCardsAsStringForShowing(cardsObjectsToShow);
        _view.ShowCards(cardsStringsToShow);
        Console.WriteLine("Mostrando Cartas");
        Console.WriteLine(_turnEnded);
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
            try
            {
                TryToPlayCard(playsToShow[chosenPlay]);
            }
            catch (CardWasReversedException e)
            {
                _turnEnded = true;
            }
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

    private void TryToPlayCard(Play chosenPlay)
    {
        Card cardPlayed = chosenPlay.Card;
        Superstar playerPlayingRoundSuperstar = _playerPlayingRound.Superstar;
        _view.SayThatPlayerIsTryingToPlayThisCard(playerPlayingRoundSuperstar.Name, chosenPlay.ToString());
        TryToReversePlay(chosenPlay);
        _view.SayThatPlayerSuccessfullyPlayedACard();
        if (chosenPlay.PlayedAs == "MANEUVER")
        {
            _playerPlayingRound.MoveCardFromHandToRingArea(chosenPlay);
            PlayManeuver(cardPlayed);
        }
        else if (chosenPlay.PlayedAs == "ACTION")
        {
            PlayAction(cardPlayed);
        }
    }

    private void TryToReversePlay(Play playOpponentIsTryingToMake)
    {
        try
        {
            List<Card> reversalCardsThatPlayerCanPlay = _playerNotPlayingRound.GetReversalCardsThatPlayerCanPlay();
            List<Card> reversalCardsThatPlayerCanPlayOnThisCard = GetReversalCardsThatPlayerCanPlayOnThisCard(reversalCardsThatPlayerCanPlay, playOpponentIsTryingToMake);
                reversalCardsThatPlayerCanPlay
                .Where(card => card.CheckIfCanReverseThisPlay(playOpponentIsTryingToMake)).ToList();
            List<Play> reversalPlays = GetPlaysOfAvailablesCards(reversalCardsThatPlayerCanPlayOnThisCard);
            List<string> reversalPlaysString = reversalPlays.Select(play => play.ToString()).ToList();
            int usersChoice = _view.AskUserToSelectAReversal(_playerNotPlayingRound.GetSuperstarName(), reversalPlaysString);
            if (usersChoice != -1)
            {
                Play reversalSelected = reversalPlays[usersChoice];
                _view.SayThatPlayerReversedTheCard(_playerNotPlayingRound.GetSuperstarName(), reversalSelected.ToString());
                _playerPlayingRound.MoveCardFromHandToRingside(reversalSelected.Card);
                Console.WriteLine("MOVIENDO LA CARTA AL RINGSIDE");
                throw new CardWasReversedException();
            }
        }
        catch (NoReverseCardsException e)
        {
        }
        
    }

    private List<Play> GetPlaysOfAvailablesCards(List<Card> reversalCardThatPlayerCanPlay)
    {
        List<Play> playsToReturn = new();
        foreach (Card card in reversalCardThatPlayerCanPlay)
        {
            Play play = new Play(card, "REVERSAL");
            playsToReturn.Add(play);
        }

        return playsToReturn;
    }

    private List<Card> GetReversalCardsThatPlayerCanPlayOnThisCard(List<Card> reversalCardsThatPlayerCanPlay, Play playOpponentIsTryingToMake)
    {
        List<Card> reversalCardsThatPlayerCanPlayOnThisCard = reversalCardsThatPlayerCanPlay
            .Where(card => card.CheckIfCanReverseThisPlay(playOpponentIsTryingToMake)).ToList();
        if (!reversalCardsThatPlayerCanPlayOnThisCard.Any())
        {
            throw new NoReverseCardsException();
        }

        return reversalCardsThatPlayerCanPlayOnThisCard;
    }

    private void PlayManeuver(Card cardPlayed)
    {
        int cardDamage = ManageCardDamage(cardPlayed.Damage);
        Superstar playerNotPlayingRoundSuperstar = _playerNotPlayingRound.Superstar;
        if (cardDamage > 0)
        {
            _view.SayThatOpponentWillTakeSomeDamage(playerNotPlayingRoundSuperstar.Name, cardDamage);
        }
        for (int i = 1; i <= cardDamage; i++)
        {
            if (!_turnEnded)
            {
                DealSingleCardDamage(i, cardDamage);
            }
        }
        _playerPlayingRound.UpdateFortitude();
    }

    private int ManageCardDamage(int initialDamage)
    {
        if (AbilitiesManager.CheckIfHasAbilityWhenReceivingDamage(_playerNotPlayingRound))
        {
            return initialDamage - 1;
        }
        return initialDamage;
    }
    
    private void DealSingleCardDamage(int cardIndex, int cardDamage)
    {
        Card cardThatWentToRingSide = _playerNotPlayingRound.ReceivesDamage();
        _view.ShowCardOverturnByTakingDamage(cardThatWentToRingSide.ToString(), cardIndex, cardDamage);

        if (cardIndex < cardDamage)
        {
            CheckIfGameAndTurnShouldEndWhileReceivingDamage();
        }
    }

    private void CheckIfGameAndTurnShouldEndWhileReceivingDamage()
    {
        _gameShouldEnd = !_playerNotPlayingRound.HasCardsInArsenal();
        _turnEnded = _gameShouldEnd;
    }

    private void PlayAction(Card cardPlayed)
    {
        Superstar playerPlayingRoundSuperstar = _playerPlayingRound.Superstar;
        _playerPlayingRound.MoveCardFromHandToRingside(cardPlayed);
        _playerPlayingRound.DrawSingleCard();
        _view.SayThatPlayerMustDiscardThisCard(playerPlayingRoundSuperstar.Name, cardPlayed.Title);
        _view.SayThatPlayerDrawCards(playerPlayingRoundSuperstar.Name, 1);
    }
    
    public void CheckIfGameShouldEnd()
    {
        if (!_playerPlayingRound.HasCardsInArsenal() || !_playerNotPlayingRound.HasCardsInArsenal())
        {
            _gameShouldEnd = true;
        }
    }

    public Player GetGameWinner()
    {
        Player winner;
        if (!_playerPlayingRound.HasCardsInArsenal() && !_playerNotPlayingRound.HasCardsInArsenal())
        {
            winner = _playerPlayingRound;
        }
        else if (!_playerPlayingRound.HasCardsInArsenal())
        {
            winner = _playerNotPlayingRound;
        }
        else if (!_playerNotPlayingRound.HasCardsInArsenal())
        {
            winner = _playerPlayingRound;
        }
        else
        {
            winner = _playerNotPlayingRound;
        }

        return winner;
    }
    
}