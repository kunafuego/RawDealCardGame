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
    private EffectForNextMove _nextMoveEffect;
    
    public RoundManager(Player playerPlayingRound, Player playerNotPlayingRound, View view, EffectForNextMove effectForNextMove)
    {
        _gameShouldEnd = false;
        _turnEnded = false;
        _playerPlayingRound = playerPlayingRound;
        _playerNotPlayingRound = playerNotPlayingRound;
        _view = view;
        _nextMoveEffect = effectForNextMove;
    }

    public EffectForNextMove NextMoveEffect
    {
        get { return _nextMoveEffect; }
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
            ManageShowingCards();
            // Crear otra clase altoquee
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
            _nextMoveEffect = new EffectForNextMove(0, 0);
        }
        else if (optionChosen == NextPlay.GiveUp)
        {
            _turnEnded = true;
            _gameShouldEnd = true;
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
            try
            {
                TryToPlayCard(playsToShow[chosenPlay]);
            }
            catch (CardWasReversedException error)
            {
                // Console.WriteLine(error.Message);
                if (error.Message == "Jockeying for Position")
                {
                    SelectedEffect chosenOption = _view.AskUserToSelectAnEffectForJockeyForPosition(_playerNotPlayingRound.GetSuperstarName());
                    _nextMoveEffect = (chosenOption == SelectedEffect.NextGrappleIsPlus4D)
                        ? new EffectForNextMove(4, 0)
                        : new EffectForNextMove(0, 8);
                }
                else
                {
                    _nextMoveEffect = new EffectForNextMove(0, 0);
                }
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
        _view.SayThatPlayerIsTryingToPlayThisCard(_playerPlayingRound.GetSuperstarName(), chosenPlay.ToString());
        TryToReversePlay(chosenPlay);
        _view.SayThatPlayerSuccessfullyPlayedACard();
        if (chosenPlay.PlayedAs == "MANEUVER")
        {
            PlayManeuver(cardPlayed);
        }
        else if (chosenPlay.PlayedAs == "ACTION")
        {
            PlayAction(cardPlayed);
        }
    }

    private void TryToReversePlay(Play playOpponentIsTryingToMake)
    {
        // Console.WriteLine("Trying To reveerse Maneuver with");
        // Console.WriteLine(_nextMoveEffect.FortitudeChange);
        List<Card> reversalCardsThatPlayerCanPlay = _playerNotPlayingRound.GetReversalCardsThatPlayerCanPlay(_nextMoveEffect, playOpponentIsTryingToMake.Card);
        List<Card> reversalCardsThatPlayerCanPlayOnThisCard = GetReversalCardsThatPlayerCanPlayOnThisCard(reversalCardsThatPlayerCanPlay, playOpponentIsTryingToMake);
        if (reversalCardsThatPlayerCanPlayOnThisCard.Any())
        {
            List<Play> reversalPlays = GetPlaysOfAvailablesCards(reversalCardsThatPlayerCanPlayOnThisCard);
            List<string> reversalPlaysString = reversalPlays.Select(play => play.ToString()).ToList();
            int usersChoice = _view.AskUserToSelectAReversal(_playerNotPlayingRound.GetSuperstarName(), reversalPlaysString);
            if (usersChoice != -1)
            {
                Play reversalSelected = reversalPlays[usersChoice];
                Card reversalCardSelected = reversalSelected.Card;
                playOpponentIsTryingToMake.PlayedAs = "Reversed From Hand";
                _view.SayThatPlayerReversedTheCard(_playerNotPlayingRound.GetSuperstarName(), reversalSelected.ToString());
                _playerPlayingRound.MoveCardFromHandToRingside(playOpponentIsTryingToMake.Card);
                SetDamageThatReversalShouldMake(reversalCardSelected, playOpponentIsTryingToMake.Card);
                ReversalManager reversalManager = new ReversalManager(_view);
                reversalManager.PerformEffect(playOpponentIsTryingToMake, reversalSelected.Card, _playerNotPlayingRound, _playerPlayingRound);
                throw new CardWasReversedException(reversalCardSelected.Title);
            }
        }
    }
    private List<Card> GetReversalCardsThatPlayerCanPlayOnThisCard(List<Card> reversalCardsThatPlayerCanPlay, Play playOpponentIsTryingToMake)
    {
        Card cardOpponentIsTryingToMake = playOpponentIsTryingToMake.Card;
        ReversalManager reversalManager = new ReversalManager(_view);
        List<Card> reversalCardsThatPlayerCanPlayOnThisCard = reversalCardsThatPlayerCanPlay
            .Where(cardThatCanPossibleReverse => reversalManager.CheckIfCanReverseThisPlay(cardThatCanPossibleReverse, playOpponentIsTryingToMake, "Hand", cardOpponentIsTryingToMake.GetDamage() + _nextMoveEffect.DamageChange)).ToList();
        return reversalCardsThatPlayerCanPlayOnThisCard;
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

    private void SetDamageThatReversalShouldMake(Card reversalCardSelected, Card cardOpponentWasTryingToPlay)
    {
        if (reversalCardSelected.Title == "Rolling Takedown" || reversalCardSelected.Title == "Knee to the Gut")
        {
            cardOpponentWasTryingToPlay.ReversalDamage = cardOpponentWasTryingToPlay.GetDamage() + _nextMoveEffect.DamageChange;
            Console.WriteLine("El reversal damage de " + Convert.ToString(cardOpponentWasTryingToPlay.Title) + " quedó seteado a " + Convert.ToString(reversalCardSelected.GetDamage()) + " + " + Convert.ToString(_nextMoveEffect.DamageChange));
        }
    }

    private void PlayManeuver(Card cardPlayed)
    {
        // Console.WriteLine("228 Round Manager" + Convert.ToString(_nextMoveEffect.FortitudeChange));
        ManeuverPlayer maneuverPlayer = new ManeuverPlayer(_view, _playerPlayingRound, _playerNotPlayingRound, _nextMoveEffect);
        maneuverPlayer.PlayManeuver(cardPlayed);
        _nextMoveEffect = new EffectForNextMove(0, 0);
        _turnEnded = maneuverPlayer.TurnEnded;
        _gameShouldEnd = maneuverPlayer.GameShouldEnd;
    }

    private void PlayAction(Card cardPlayed)
    {
        if (cardPlayed.Title == "Jockeying for Position")
        {
            SelectedEffect chosenOption = _view.AskUserToSelectAnEffectForJockeyForPosition(_playerPlayingRound.GetSuperstarName());
            // Console.WriteLine(chosenOption);
            _nextMoveEffect = (chosenOption == SelectedEffect.NextGrappleIsPlus4D)
                ? new EffectForNextMove(4, 0)
                : new EffectForNextMove(0, 8);
            // Console.WriteLine(_nextMoveEffect.FortitudeChange);
            _playerPlayingRound.MoveCardFromHandToRingArea(cardPlayed);
        }
        else
        {
            _playerPlayingRound.DrawSingleCard();
            _view.SayThatPlayerMustDiscardThisCard(_playerPlayingRound.GetSuperstarName(), cardPlayed.Title);
            _view.SayThatPlayerDrawCards(_playerPlayingRound.GetSuperstarName(), 1);
            _playerPlayingRound.MoveCardFromHandToRingside(cardPlayed);
        }
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