using RawDealView;
namespace RawDeal;

class KaneAbility : SuperstarAbility
{
    public KaneAbility(Player player1, Player player2, View view) : base(player1, player2, view) {}

    public override void UseEffect(Player playerPlayingRound, Player playerNotPlayingRound, View view)
    {
        Console.WriteLine("Dentro del use effect");
        view.SayThatOpponentWillTakeSomeDamage(playerNotPlayingRound.Superstar.Name, 1);
        Card cardThatWentToRingSide = playerNotPlayingRound.ReceivesDamage();
        view.ShowCardOverturnByTakingDamage(cardThatWentToRingSide.ToString(), 1, 1);
        playerPlayingRound.UpdateFortitude();
    }

    public override bool MeetsTheRequirementsForUsingEffect(Player player)
    {
        return true;

    }

}