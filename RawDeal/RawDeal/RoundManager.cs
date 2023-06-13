using RawDeal.Bonus;
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
    private LastPlay _lastPlayInstance;
    private BonusManager _bonusManager;
    
    public RoundManager(Player playerPlayingRound, Player playerNotPlayingRound, View view, 
        EffectForNextMove effectForNextMove, LastPlay lastPlayInstance, BonusManager bonusManager)
    {
        _gameShouldEnd = false;
        _turnEnded = false;
        _playerPlayingRound = playerPlayingRound;
        _playerNotPlayingRound = playerNotPlayingRound;
        _view = view;
        _nextMoveEffect = effectForNextMove;
        _lastPlayInstance = lastPlayInstance;
        _bonusManager = bonusManager;
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
        _view.SayThatATurnBegins(_playerPlayingRound.GetSuperstarName());
        AbilitiesManager.ManageAbilityBeforeDraw(_playerPlayingRound, _playerNotPlayingRound);
        PlayerDrawCards();
        bool effectUsed = false;
        do
        {
            NextPlay nextPlayOptionChosen;
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

        _lastPlayInstance.WasItPlayedOnSameTurnThanActualPlay = _lastPlayInstance.WasItASuccesfulReversal;
        _lastPlayInstance.WasItASuccesfulReversal = false;
    }
    
    private void PlayerDrawCards()
    {
        bool userCanUseAbility = AbilitiesManager.CheckIfUserCanUseAbilityDuringDrawSection(_playerPlayingRound);
        if(userCanUseAbility)
        {
            AbilitiesManager.UseAbilityDuringDrawSegment(_playerPlayingRound, _playerNotPlayingRound);
        }
        else
        {
            _playerPlayingRound.DrawSingleCard();
        }
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
        CardsShower cardsShower = new CardsShower(_view, _playerPlayingRound, _playerNotPlayingRound);
        cardsShower.ManageShowingCards();
    }

    private void ManagePlayingCards()
    {
        CardPlayer cardPlayer = new CardPlayer(_view, _playerPlayingRound, _playerNotPlayingRound, _nextMoveEffect, _lastPlayInstance, _bonusManager);
        cardPlayer.ManagePlayingCards();
        _nextMoveEffect = cardPlayer.NextMoveEffect;
        _turnEnded = cardPlayer.TurnEnded;
        _gameShouldEnd = cardPlayer.GameShouldEnd;
    }

    private void UseAbilityDuringTurn()
    {
        AbilitiesManager.UseAbilityDuringTurn(_playerPlayingRound, _playerNotPlayingRound);
    }

    private void EndTurn()
    {
        _turnEnded = true;
        _nextMoveEffect = new EffectForNextMove(0, 0);
    }

    private void GiveUp()
    {
        _turnEnded = true;
        _gameShouldEnd = true;
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
        if (!_playerNotPlayingRound.HasCardsInArsenal())
        {
            return _playerPlayingRound;
        }
        return _playerNotPlayingRound;
    }
    
}