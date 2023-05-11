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
    
    public abstract void UseAbility(Player playerPlayingRound, Player playerNotPlayingRound);
    public abstract bool MeetsTheRequirementsForUsingEffect(Player player);

    public bool NeedToAskToUseAbilityAtBeginningOfTurn(Player playerInTurn)
    {
        return playerInTurn.NeedToAskToUseAbilityAtBeginningOfTurn();
    }

    public bool MustUseAbilityAtStartOfTurn(Player playerInTurn)
    {
        return playerInTurn.MustUseAbilityAtStartOfTurn();
    }

    public bool NeedToAskToUseAbilityDuringTurn(Player playerInTurn)
    {
        return playerInTurn.NeedToAskToUseAbilityDuringTurn();
    }
    
    public bool MustUseEffectDuringDrawSegment(Player playerInTurn)
    {
        return playerInTurn.MustUseAbilityDuringDrawSegment();
    }
    
    public bool MustUseEffectWhileReceivingDamage(Player playerInTurn)
    {
        return playerInTurn.MustUseAbilityWhileReceivingDamage();
    }
}