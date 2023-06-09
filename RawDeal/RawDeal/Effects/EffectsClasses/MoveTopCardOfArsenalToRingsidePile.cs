using RawDealView;
namespace RawDeal.Effects.EffectsClasses;

public class MoveTopCardOfArsenalToRingsidePile : Effect
{
    private const int AmountOfDamageToMake = 1;
    public override void Apply(Play playThatIsBeingReversed, View view, Player playerThatPlayedCard, Player opponent)
    {
        view.SayThatPlayerDamagedHimself(playerThatPlayedCard.GetSuperstarName(), AmountOfDamageToMake);
        view.SayThatSuperstarWillTakeSomeDamage(playerThatPlayedCard.GetSuperstarName(), AmountOfDamageToMake);
        CheckIfGameIsFinished(view, playerThatPlayedCard);
        playerThatPlayedCard.MoveArsenalTopCardToRingside();
        Card cardThatWentToRingside = playerThatPlayedCard.GetCardOnTopOfRingside();
        view.ShowCardOverturnByTakingDamage(cardThatWentToRingside.ToString(), AmountOfDamageToMake, 
            AmountOfDamageToMake);
    }

    private void CheckIfGameIsFinished(View view, Player playerThatPlayedCard)
    {
        if (playerThatPlayedCard.HasCardsInArsenal()) return;
        view.SayThatPlayerLostDueToSelfDamage(playerThatPlayedCard.GetSuperstarName());
        throw new GameEndedBecauseOfCollateralDamage("");
    }
}