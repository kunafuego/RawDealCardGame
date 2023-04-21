using RawDealView;
namespace RawDeal;

class MankindAbility : SuperstarAbility
{
    public MankindAbility(View view) : base(view) {}

    public override void UseEffect(Player playerPlayingRound)
    {
        for (int i = 0; i < 2; i++)
        {
            playerPlayingRound.MovesCardFromArsenalToHandInDrawSegment();
        }
    }

    public override bool MeetsTheRequirementsForUsingEffect(Player player)
    {
        return player.HasMoreThanOneCardInArsenal();
    }
}