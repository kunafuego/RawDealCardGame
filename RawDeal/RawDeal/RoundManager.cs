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
        _view.SayThatATurnBegins(_playerPlayingRound.GetSuperstarName());
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
        if (optionChosen == NextPlay.ShowCards)
        {
            CardsShower cardsShower = new CardsShower(_view, _playerPlayingRound, _playerNotPlayingRound);
            cardsShower.ManageShowingCards();
        }
        else if (optionChosen == NextPlay.PlayCard)
        {
            CardPlayer cardPlayer = new CardPlayer(_view, _playerPlayingRound, _playerNotPlayingRound, _nextMoveEffect);
            cardPlayer.ManagePlayingCards();
            _nextMoveEffect = cardPlayer.NextMoveEffect;
            _turnEnded = cardPlayer.TurnEnded;
            _gameShouldEnd = cardPlayer.GameShouldEnd;
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