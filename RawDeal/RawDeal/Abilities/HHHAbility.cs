using RawDealView;

namespace RawDeal;

internal class HHHAbility : SuperstarAbility
{
    public HHHAbility(View view) : base(view)
    {
    }

    public override void UseAbility(Player playerPlayingRound, Player playerNotPlayingRound)
    {
    }

    public override bool MeetsTheRequirementsForUsingEffect(Player player)
    {
        return true;
    }
}