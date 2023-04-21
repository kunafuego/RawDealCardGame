using RawDealView;
namespace RawDeal;

class MankindAbility : SuperstarAbility
{
    public MankindAbility(Player player1, Player player2, View view) : base(player1, player2, view) {}

    public override void UseEffect(Player playerPlayingRound, Player playerNotPlayingRound, View view)
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