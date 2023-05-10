using RawDealView.Options;
using RawDealView;
namespace RawDeal;

abstract class SuperstarAbility
{
    protected View View;
    protected SuperstarAbility (View view)
    {
        View = view;
    }
    
    public abstract void UseEffect(Player playerPlayingRound);
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