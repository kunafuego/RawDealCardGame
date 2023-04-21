using RawDealView;
namespace RawDeal;

class MankindAbility : SuperstarAbility
{
    public override void UseEffect(Player playerPlayingRound, Player playerNotPlayingRound, View view)
    {
        for (int i = 0; i < 2; i++)
        {
            playerPlayingRound.MovesCardFromArsenalToHandInDrawSegment();
        }
    }

    public override bool MeetsTheRequirementsForUsingEffect(Player player)
    {
        if (player.HasMoreThanOneCardInArsenal())
        {
            return true;
        }

        return false;
    }
}