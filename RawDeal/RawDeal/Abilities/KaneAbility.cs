using RawDealView.Options;
using RawDealView;
namespace RawDeal;

class KaneAbility : SuperstarAbility
{
    private readonly Player _player1;
    private readonly Player _player2;
    public KaneAbility(Player player1, Player player2, View view) : base(view)
    {
        _player1 = player1;
        _player2 = player2;
    }

    public override void UseEffect(Player playerPlayingRound)
    {
        Player playerNotPlayingRound = GetPlayerNotPlayingRound(playerPlayingRound);
        View.SayThatOpponentWillTakeSomeDamage(playerNotPlayingRound.Superstar.Name, 1);
        Card cardThatWentToRingSide = playerNotPlayingRound.ReceivesDamage();
        View.ShowCardOverturnByTakingDamage(cardThatWentToRingSide.ToString(), 1, 1);
        playerPlayingRound.UpdateFortitude();
    }

    private Player GetPlayerNotPlayingRound(Player playerPlayingRound)
    {
        Player playerNotPlayingRound = (playerPlayingRound == _player1) ? _player2 : _player1;
        return playerNotPlayingRound;
    }

    public override bool MeetsTheRequirementsForUsingEffect(Player player)
    {
        return true;

    }

}