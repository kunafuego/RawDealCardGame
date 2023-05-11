using RawDealView.Options;
using RawDealView;
namespace RawDeal;

class MankindAbility : SuperstarAbility
{
    public MankindAbility(View view) : base(view) {}

    public override void UseAbility(Player playerPlayingRound, Player playerNotPlayingRound)
    {
        for (int i = 0; i < 2; i++)
        {
            playerPlayingRound.DrawSingleCard();
        }
    }

    public override bool MeetsTheRequirementsForUsingEffect(Player player)
    {
        return player.HasMoreThanOneCardInArsenal();
    }
}