using RawDealView.Options;
using RawDealView;
namespace RawDeal;

class MankindAbility : SuperstarAbility
{
    private const int AmountOfCardsToDraw = 2;
    public MankindAbility(View view) : base(view) {}

    public override void UseAbility(Player playerPlayingRound, Player playerNotPlayingRound)
    {
        for (int i = 0; i < AmountOfCardsToDraw; i++)
        {
            playerPlayingRound.DrawSingleCard();
        }
    }

    public override bool MeetsTheRequirementsForUsingEffect(Player player)
    {
        return player.CheckIfPlayerHasMoreThanOneCardInArsenal();
    }
}