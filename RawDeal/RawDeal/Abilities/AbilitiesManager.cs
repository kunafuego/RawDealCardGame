using RawDealView;

namespace RawDeal;

public static class AbilitiesManager
{
    private static Dictionary<Player, SuperstarAbility> PlayersAbilities { get; } = new();
    public static View View { get; set; }

    public static void SetAbility(Player player)
    {
        var abilities = new Dictionary<string, SuperstarAbility>
        {
            { "THE ROCK", new TheRockAbility(View) },
            { "CHRIS JERICHO", new ChrisJerichoAbility(View) },
            { "KANE", new KaneAbility(View) },
            { "MANKIND", new MankindAbility(View) },
            { "THE UNDERTAKER", new TheUndertakerAbility(View) },
            { "STONE COLD STEVE AUSTIN", new StoneColdAbility(View) },
            { "HHH", new HHHAbility(View) }
        };
        PlayersAbilities.Add(player, abilities[player.GetSuperstarName()]);
    }

    public static void ManageAbilityBeforeDraw(Player playerPlayingRound,
        Player playerNotPlayingRound)
    {
        var playerPlayingRoundSuperstar = playerPlayingRound.Superstar;
        var playerPlayingRoundSuperstarAbility = PlayersAbilities[playerPlayingRound];
        if (playerPlayingRoundSuperstarAbility.MustUseAbilityAtStartOfTurn(playerPlayingRound))
        {
            View.SayThatPlayerIsGoingToUseHisAbility(playerPlayingRoundSuperstar.Name,
                playerPlayingRoundSuperstar.SuperstarAbility);
            playerPlayingRoundSuperstarAbility.UseAbility(playerPlayingRound,
                playerNotPlayingRound);
        }

        if (playerPlayingRoundSuperstarAbility.NeedToAskToUseAbilityAtBeginningOfTurn(
                playerPlayingRound) &&
            playerPlayingRoundSuperstarAbility.MeetsTheRequirementsForUsingEffect(
                playerPlayingRound))
        {
            var wantsToUse = View.DoesPlayerWantToUseHisAbility(playerPlayingRoundSuperstar.Name);
            if (wantsToUse)
                playerPlayingRoundSuperstarAbility.UseAbility(playerPlayingRound,
                    playerNotPlayingRound);
        }
    }

    public static void UseAbilityDuringDrawSegment(Player playerPlayingRound,
        Player playerNotPlayingRound)
    {
        var playerPlayingRoundSuperstarAbility = PlayersAbilities[playerPlayingRound];
        if (playerPlayingRoundSuperstarAbility.MustUseEffectDuringDrawSegment(playerPlayingRound) &&
            playerPlayingRoundSuperstarAbility.MeetsTheRequirementsForUsingEffect(
                playerPlayingRound))
            playerPlayingRoundSuperstarAbility.UseAbility(playerPlayingRound,
                playerNotPlayingRound);
        else
            throw new CantUseAbilityException();
    }

    public static bool CheckIfUserCanUseAbilityDuringDrawSection(Player playerPlayingRound)
    {
        var playerPlayingRoundSuperstarAbility = PlayersAbilities[playerPlayingRound];
        if (playerPlayingRoundSuperstarAbility.MustUseEffectDuringDrawSegment(playerPlayingRound) &&
            playerPlayingRoundSuperstarAbility.MeetsTheRequirementsForUsingEffect(
                playerPlayingRound))
            return true;

        return false;
    }

    public static bool CheckIfUserCanUseAbilityDuringTurn(Player playerPlayingRound)
    {
        var playerPlayingRoundSuperstarAbility = PlayersAbilities[playerPlayingRound];
        if (playerPlayingRoundSuperstarAbility
                .NeedToAskToUseAbilityDuringTurn(playerPlayingRound) &&
            playerPlayingRoundSuperstarAbility.MeetsTheRequirementsForUsingEffect(
                playerPlayingRound))
            return true;
        return false;
    }

    public static void UseAbilityDuringTurn(Player playerPlayingRound, Player playerNotPlayingRound)
    {
        var playerPlayingRoundSuperstarAbility = PlayersAbilities[playerPlayingRound];
        var playerPlayingRoundSuperstar = playerPlayingRound.Superstar;
        View.SayThatPlayerIsGoingToUseHisAbility(playerPlayingRoundSuperstar.Name,
            playerPlayingRoundSuperstar.SuperstarAbility);
        playerPlayingRoundSuperstarAbility.UseAbility(playerPlayingRound, playerNotPlayingRound);
    }

    public static bool CheckIfHasAbilityWhenReceivingDamage(Player playerReceivingDamage)
    {
        var playerReceivingDamageSuperstarAbility = PlayersAbilities[playerReceivingDamage];
        return playerReceivingDamageSuperstarAbility.MustUseEffectWhileReceivingDamage(
            playerReceivingDamage);
    }
}