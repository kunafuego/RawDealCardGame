using RawDealView;
namespace RawDeal;

class HHHAbility : SuperstarAbility
{
    public HHHAbility(Player player1, Player player2, View view) : base(player1, player2, view) {}

    public override void UseEffect(Player playerPlayingRound, Player playerNotPlayingRound, View view)
    {
        
    }

    public override bool MeetsTheRequirementsForUsingEffect(Player player)
    {
        return true;

    }
}