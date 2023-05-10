using RawDealView.Options;
using RawDealView;
namespace RawDeal;

class HHHAbility : SuperstarAbility
{
    public HHHAbility(View view) : base(view) {}

    public override void UseEffect(Player playerPlayingRound)
    {
        
    }

    public override bool MeetsTheRequirementsForUsingEffect(Player player)
    {
        return true;

    }
}