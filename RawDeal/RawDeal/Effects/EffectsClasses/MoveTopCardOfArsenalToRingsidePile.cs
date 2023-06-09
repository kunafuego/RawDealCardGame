using RawDealView;
namespace RawDeal.Effects.EffectsClasses;

public class MoveTopCardOfArsenalToRingsidePile : Effect
{
    private const int AmountOfDamageToMake = 1;
    public override void Apply(Play playThatIsBeingReversed, View view, Player playerThatPlayedCard, Player opponent)
    {
        view.SayThatPlayerDamagedHimself(playerThatPlayedCard.GetSuperstarName(), AmountOfDamageToMake);
        view.SayThatSuperstarWillTakeSomeDamage(playerThatPlayedCard.GetSuperstarName(), AmountOfDamageToMake);
        playerThatPlayedCard.MoveArsenalTopCardToRingside();
        Card cardThatWentToRingside = playerThatPlayedCard.GetCardOnTopOfRingside();
        view.ShowCardOverturnByTakingDamage(cardThatWentToRingside.ToString(), AmountOfDamageToMake, 
            AmountOfDamageToMake);
        if (!playerThatPlayedCard.HasCardsInArsenal())
        {
            view.SayThatPlayerLostDueToSelfDamage(playerThatPlayedCard.GetSuperstarName());
            throw new GameEndedBecauseOfCollateralDamage("");
        }
    }
}