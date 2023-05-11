using RawDealView;

namespace RawDeal;

public static class AbilitiesManager
{
    private static Dictionary<Player, SuperstarAbility> PlayersAbilities { get; set; } = new();
    public static View View { get; set; }

    public static void SetAbility(Player player)
    {
        Dictionary<string, SuperstarAbility> abilities = new Dictionary<string, SuperstarAbility>
        {
            {"THE ROCK", new TheRockAbility(View)},
            {"CHRIS JERICHO", new ChrisJerichoAbility(View)},
            {"KANE", new KaneAbility(View)},
            {"MANKIND", new MankindAbility(View)},
            {"THE UNDERTAKER", new TheUndertakerAbility(View)},
            {"STONE COLD STEVE AUSTIN", new StoneColdAbility(View)},
            {"HHH", new HHHAbility(View)}
        };
        // Donde dejo este diccionario
        Superstar playersSuperstar = player.Superstar;
        Console.WriteLine(playersSuperstar.Name);
        PlayersAbilities.Add(player, abilities[playersSuperstar.Name]);
    }
    
    public static void ManageAbilityBeforeDraw(Player playerPlayingRound, Player playerNotPlayingRound)
    {
        Superstar playerPlayingRoundSuperstar = playerPlayingRound.Superstar;
        SuperstarAbility playerPlayingRoundSuperstarAbility = PlayersAbilities[playerPlayingRound];
        if (playerPlayingRoundSuperstarAbility.MustUseAbilityAtStartOfTurn(playerPlayingRound))
        {
            View.SayThatPlayerIsGoingToUseHisAbility(playerPlayingRoundSuperstar.Name,
                playerPlayingRoundSuperstar.SuperstarAbility);
            playerPlayingRoundSuperstarAbility.UseAbility(playerPlayingRound, playerNotPlayingRound);
        }

        if (playerPlayingRoundSuperstarAbility.NeedToAskToUseAbilityAtBeginningOfTurn(playerPlayingRound) &&
            playerPlayingRoundSuperstarAbility.MeetsTheRequirementsForUsingEffect(playerPlayingRound))
        {
            bool wantsToUse = View.DoesPlayerWantToUseHisAbility(playerPlayingRoundSuperstar.Name);
            if (wantsToUse)
            {
                playerPlayingRoundSuperstarAbility.UseAbility(playerPlayingRound, playerNotPlayingRound);
            }
        }
    }

    public static void UseAbilityDuringDrawSegment(Player playerPlayingRound, Player playerNotPlayingRound)
    {
        SuperstarAbility playerPlayingRoundSuperstarAbility = PlayersAbilities[playerPlayingRound];
        if (playerPlayingRoundSuperstarAbility.MustUseEffectDuringDrawSegment(playerPlayingRound) &&
            playerPlayingRoundSuperstarAbility.MeetsTheRequirementsForUsingEffect(playerPlayingRound))
        {
            playerPlayingRoundSuperstarAbility.UseAbility(playerPlayingRound, playerNotPlayingRound);
        }
        else
        {
            throw new CantUseAbilityException();
        }

    }

    public static bool CheckIfUserCanUseAbilityDuringTurn(Player playerPlayingRound)
    {
        SuperstarAbility playerPlayingRoundSuperstarAbility = PlayersAbilities[playerPlayingRound];
        if (playerPlayingRoundSuperstarAbility.NeedToAskToUseAbilityDuringTurn(playerPlayingRound) &&
            playerPlayingRoundSuperstarAbility.MeetsTheRequirementsForUsingEffect(playerPlayingRound))
        {
            return true;
        }
        return false;
    }

    public static void UseAbilityDuringTurn(Player playerPlayingRound, Player playerNotPlayingRound)
    {
        SuperstarAbility playerPlayingRoundSuperstarAbility = PlayersAbilities[playerPlayingRound]; 
        Superstar playerPlayingRoundSuperstar = playerPlayingRound.Superstar;
        View.SayThatPlayerIsGoingToUseHisAbility(playerPlayingRoundSuperstar.Name,
            playerPlayingRoundSuperstar.SuperstarAbility);
        playerPlayingRoundSuperstarAbility.UseAbility(playerPlayingRound, playerNotPlayingRound);
    }

    public static bool CheckIfHasAbilityWhenReceivingDamage(Player playerReceivingDamage)
    {
        SuperstarAbility playerReceivingDamageSuperstarAbility = PlayersAbilities[playerReceivingDamage];
        return playerReceivingDamageSuperstarAbility.MustUseEffectWhileReceivingDamage(playerReceivingDamage);
    }
}