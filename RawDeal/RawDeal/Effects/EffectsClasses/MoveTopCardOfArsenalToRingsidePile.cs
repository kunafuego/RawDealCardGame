using RawDealView;
namespace RawDeal.Effects.EffectsClasses;

public class MoveTopCardOfArsenalToRingsidePile : Effect
{
    private const int AmountOfDamageToMake = 1;
    private View _view;

    public MoveTopCardOfArsenalToRingsidePile(View view)
    {
        _view = view;
    }
    
    public override void Apply(Play actualPlay, Player playerThatPlayedCard, Player opponent)
    {
        _view.SayThatPlayerDamagedHimself(playerThatPlayedCard.GetSuperstarName(), AmountOfDamageToMake);
        _view.SayThatSuperstarWillTakeSomeDamage(playerThatPlayedCard.GetSuperstarName(), AmountOfDamageToMake);
        CheckIfGameIsFinished(playerThatPlayedCard);
        playerThatPlayedCard.MoveArsenalTopCardToRingside();
        Card cardThatWentToRingside = playerThatPlayedCard.GetCardOnTopOfRingside();
        _view.ShowCardOverturnByTakingDamage(cardThatWentToRingside.ToString(), AmountOfDamageToMake, 
            AmountOfDamageToMake);
    }

    private void CheckIfGameIsFinished(Player playerThatPlayedCard)
    {
        if (playerThatPlayedCard.HasCardsInArsenal()) return;
        _view.SayThatPlayerLostDueToSelfDamage(playerThatPlayedCard.GetSuperstarName());
        throw new GameEndedBecauseOfCollateralDamage("");
    }
}