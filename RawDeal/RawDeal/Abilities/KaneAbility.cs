using RawDealView;

namespace RawDeal;

internal class KaneAbility : SuperstarAbility
{
    public KaneAbility(View view) : base(view)
    {
    }

    public override void UseAbility(Player playerPlayingRound, Player playerNotPlayingRound)
    {
        View.SayThatSuperstarWillTakeSomeDamage(playerNotPlayingRound.GetSuperstarName(), 1);
        playerNotPlayingRound.MoveArsenalTopCardToRingside();
        var cardThatWentToRingside = playerNotPlayingRound.GetCardOnTopOfRingside();
        View.ShowCardOverturnByTakingDamage(cardThatWentToRingside.ToString(), 1, 1);
    }

    public override bool MeetsTheRequirementsForUsingEffect(Player player)
    {
        return true;
    }
}