using RawDealView;
namespace RawDeal;

abstract class SuperstarAbility
{
    private Player _player1;
    private Player _player2;
    private View _view;
    protected SuperstarAbility (Player player1, Player player2, View view)
    {
        _player1 = player1;
        _player2 = player2;
        _view = view;
    }
    
    public abstract void UseEffect(Player playerPlayingRound, Player playerNotPlayingRound, View view);
    public abstract bool MeetsTheRequirementsForUsingEffect(Player player);

    public bool NeedToAskToUseAbilityAtBeginningOfTurn(Player playerInTurn)
    {
        return playerInTurn.NeedToAskToUseAbilityAtBeginningOfTurn();
    }

    public bool MustUseEffectAtStartOfTurn(Player playerInTurn)
    {
        return playerInTurn.MustUseEffectAtStartOfTurn();
    }

    public bool NeedToAskToUseAbilityDuringTurn(Player playerInTurn)
    {
        return playerInTurn.NeedToAskToUseAbilityDuringTurn();
    }
    
    public bool MustUseEffectDuringDrawSegment(Player playerInTurn)
    {
        return playerInTurn.MustUseEffectDuringDrawSegment();
    }
    
    public bool MustUseEffectWhileReceivingDamage(Player playerInTurn)
    {
        return playerInTurn.MustUseEffectWhileReceivingDamage();
    }
}