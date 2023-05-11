using RawDealView.Options;
using RawDealView;
namespace RawDeal;

class KaneAbility : SuperstarAbility
{
    public KaneAbility(View view) : base(view) {}

    public override void UseAbility(Player playerPlayingRound, Player playerNotPlayingRound)
    {
        View.SayThatOpponentWillTakeSomeDamage(playerNotPlayingRound.Superstar.Name, 1);
        Card cardThatWentToRingSide = playerNotPlayingRound.ReceivesDamage();
        View.ShowCardOverturnByTakingDamage(cardThatWentToRingSide.ToString(), 1, 1);
        playerPlayingRound.UpdateFortitude();
    }

    public override bool MeetsTheRequirementsForUsingEffect(Player player)
    {
        return true;

    }

}