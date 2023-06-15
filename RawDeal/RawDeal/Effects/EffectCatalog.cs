using RawDeal.Bonus;
using RawDeal.Bonus.BonusClasses;
using RawDeal.Effects.EffectsClasses;
using RawDeal.Preconditions.PreconditionClasses;
using RawDealView;

namespace RawDeal.Effects;

public static class EffectCatalog
{
    private static Dictionary<string, List<Effect>> _effectCatalog = new Dictionary<string, List<Effect>>();

    public static void SetEffectCatalog(LastPlay lastPlayInstance, BonusManager bonusManager, View view)
    {
        _effectCatalog["Chop"] = new List<Effect>() {new DiscardCardToDrawOne(view)};
        _effectCatalog["Head Butt"] = new List<Effect>() {new DiscardOwnCardEffect(1, view)};
        _effectCatalog["Haymaker"] = new List<Effect>() {new AddBonusEffect(bonusManager, new EndOfTurnStrikeBonus(1))};
        _effectCatalog["Back Body Drop"] = new List<Effect>() {new ChooseBetweenDrawOrDiscard(2, view)};
        _effectCatalog["Shoulder Block"] = new List<Effect>() {new ReturnPredeterminedDamageWhenReversedFromHandEffect(lastPlayInstance, bonusManager, view)};
        _effectCatalog["Kick"] = new List<Effect>() {new MoveTopCardOfArsenalToRingsidePile(view)};
        _effectCatalog["Cross Body Block"] = new List<Effect>() {new ReturnPredeterminedDamageWhenReversedFromHandEffect(lastPlayInstance, bonusManager, view)};
        _effectCatalog["Ensugiri"] = new List<Effect>() {new ReturnPredeterminedDamageWhenReversedFromHandEffect(lastPlayInstance, bonusManager, view)};
        _effectCatalog["Running Elbow Smash"] = new List<Effect>() {new MoveTopCardOfArsenalToRingsidePile(view)};
        _effectCatalog["Drop Kick"] = new List<Effect>() {new ReturnPredeterminedDamageWhenReversedFromHandEffect(lastPlayInstance, bonusManager, view)};
        // TODO:Reversals to this maneuver are +2D.
        _effectCatalog["Discus Punch"] = new List<Effect>() {new NoEffect()};
        _effectCatalog["Superkick"] = new List<Effect>() {new AddBonusAfterXDamage(bonusManager, lastPlayInstance)};
        _effectCatalog["Spinning Heel Kick"] = new List<Effect>() {new OpponentDiscardCardEffect(1, view)};
        _effectCatalog["Spear"] = new List<Effect>() {new ReturnPredeterminedDamageWhenReversedFromHandEffect(lastPlayInstance, bonusManager, view)};
        _effectCatalog["Clothesline"] = new List<Effect>() {new AddBonusEffect(bonusManager, new NextManeuverBonusDamage(2, lastPlayInstance))};
        _effectCatalog["Arm Bar Takedown"] = new List<Effect>() {new DiscardCardToDrawOne(view)};
        _effectCatalog["Arm Drag"] = new List<Effect>() {new DiscardOwnCardEffect(1, view)};
        _effectCatalog["Snap Mare"] = new List<Effect>() {new AddBonusEffect(bonusManager, new NextManeuverStrikeDamage(2, lastPlayInstance))};
        _effectCatalog["Double Leg Takedown"] = new List<Effect>() {new MayDrawCardsEffect(1, view)};
        // TODO:When successfully played, you may look at your opponent's hand.
        _effectCatalog["Fireman's Carry"] = new List<Effect>() {new NoEffect()};
        _effectCatalog["Headlock Takedown"] = new List<Effect>() {new OpponentDrawCardEffect(1, view)};
        _effectCatalog["Belly to Belly Suplex"] = new List<Effect>() {new ReturnPredeterminedDamageWhenReversedFromHandEffect(lastPlayInstance, bonusManager, view)};
        _effectCatalog["Atomic Drop"] = new List<Effect>() {new AddBonusEffect(bonusManager, new NextManeuverBonusDamage(2, lastPlayInstance))};
        _effectCatalog["Vertical Suplex"] = new List<Effect>() {new ReturnPredeterminedDamageWhenReversedFromHandEffect(lastPlayInstance, bonusManager, view)};
        _effectCatalog["Belly to Back Suplex"] = new List<Effect>() {new ReturnPredeterminedDamageWhenReversedFromHandEffect(lastPlayInstance, bonusManager, view)};
        _effectCatalog["Pump Handle Slam"] = new List<Effect>() {new OpponentDiscardCardEffect(2, view)};
        _effectCatalog["Reverse DDT"] = new List<Effect>() {new MayDrawCardsEffect(1, view)};
        _effectCatalog["Samoan Drop"] = new List<Effect>() {new OpponentDiscardCardEffect(1, view)};
        _effectCatalog["Bulldog"] = new List<Effect>() {new DiscardOwnCardEffect(1, view), new ChooseCardThatOpponentWillDiscardEffect(1, view)};
        _effectCatalog["Fisherman's Suplex"] = new List<Effect>() {new MoveTopCardOfArsenalToRingsidePile(view), new MayDrawCardsEffect(1, view)};
        _effectCatalog["DDT"] = new List<Effect>() {new MoveTopCardOfArsenalToRingsidePile(view), new OpponentDiscardCardEffect(2, view)};
        _effectCatalog["Power Slam"] = new List<Effect>() {new OpponentDiscardCardEffect(1, view)};
        // TODO:When successfully played, you may draw 1 card. Add +1D for every maneuver with the word "slam" in its title in your Ring area.
        _effectCatalog["Powerbomb"] = new List<Effect>() {new NoEffect()};
        _effectCatalog["Press Slam"] = new List<Effect>() {new MoveTopCardOfArsenalToRingsidePile(view), new OpponentDiscardCardEffect(2, view)};
        _effectCatalog["Collar & Elbow Lockup"] = new List<Effect>() {new DiscardCardToDrawOne(view)};
        _effectCatalog["Arm Bar"] = new List<Effect>() {new DiscardOwnCardEffect(1, view)};
        _effectCatalog["Bear Hug"] = new List<Effect>() {new OpponentDiscardCardEffect(1, view)};
        // TODO:When successfully played, look through your Arsenal for the card titled Maintain Hold and place that card in your hand, then shuffle your Arsenal.
        _effectCatalog["Full Nelson"] = new List<Effect>() {new NoEffect()};
        _effectCatalog["Choke Hold"] = new List<Effect>() {new OpponentDiscardCardEffect(1, view)};
        _effectCatalog["Ankle Lock"] = new List<Effect>() {new OpponentDiscardCardEffect(1, view)};
        _effectCatalog["Standing Side Headlock"] = new List<Effect>() {new OpponentDrawCardEffect(1, view)};
        // TODO:When successfully played, look through your Arsenal for the card titled Maintain Hold and place that card in your hand, then shuffle your Arsenal.
        _effectCatalog["Cobra Clutch"] = new List<Effect>() {new NoEffect()};
        _effectCatalog["Chicken Wing"] = new List<Effect>() {new MoveFromRingsidePileToArsenal(2, view)};
        // TODO:When successfully played, look through your Arsenal for the card titled Maintain Hold and place that card in your hand, then shuffle your Arsenal.
        _effectCatalog["Sleeper"] = new List<Effect>() {new NoEffect()};
        // TODO:When successfully played, look through your Arsenal for the card titled Maintain Hold and place that card in your hand, then shuffle your Arsenal.
        _effectCatalog["Camel Clutch"] = new List<Effect>() {new NoEffect()};
        _effectCatalog["Boston Crab"] = new List<Effect>() {new OpponentDiscardCardEffect(1, view)};;
        _effectCatalog["Guillotine Stretch"] = new List<Effect>() {new OpponentDiscardCardEffect(1, view), new MayDrawCardsEffect(1, view)};;
        // TODO:When successfully played, you may discard 3 cards, search through your Arsenal and put 1 card into your hand, then shuffle your Arsenal.
        _effectCatalog["Abdominal Stretch"] = new List<Effect>() {new NoEffect()};
        _effectCatalog["Torture Rack"] = new List<Effect>() {new OpponentDiscardCardEffect(1, view)};
        _effectCatalog["Figure Four Leg Lock"] = new List<Effect>() {new OpponentDiscardCardEffect(1, view)};
        _effectCatalog["Step Aside"] = new List<Effect>() {new NoEffect()};
        _effectCatalog["Escape Move"] = new List<Effect>() {new NoEffect()};
        _effectCatalog["Break the Hold"] = new List<Effect>() {new NoEffect()};
        _effectCatalog["Rolling Takedown"] = new List<Effect>() {new ReturnHashtagDamageEffect(lastPlayInstance, bonusManager, view)};
        _effectCatalog["Knee to the Gut"] = new List<Effect>() {new ReturnHashtagDamageEffect(lastPlayInstance, bonusManager, view)};
        _effectCatalog["Elbow to the Face"] = new List<Effect>() {new ReturnPredeterminedDamageWhenReversedFromHandEffect(lastPlayInstance, bonusManager, view)};
        _effectCatalog["Clean Break"] = new List<Effect>() {new OpponentDiscardCardEffect(4, view), new DrawCardEffect(1, view)};
        _effectCatalog["Manager Interferes"] = new List<Effect>() {new DrawCardWhenReversedFromHandEffect(1, view), 
                                                                    new ReturnPredeterminedDamageWhenReversedFromHandEffect(lastPlayInstance, bonusManager, view)};
        // TODO:Reverse any HEEL maneuver or reversal card if that opponent has 5 or more HEEL cards in his Ring area. Opponent is Disqualified and you win the game. This effect happens even if the card titled Disqualification is placed into your Ringside pile while applying damage from a HEEL maneuver or reversal card.
        _effectCatalog["Disqualification!"] = new List<Effect>() {new NoEffect()};
        _effectCatalog["No Chance in Hell"] = new List<Effect>() {new NoEffect()};
        // TODO:Look at the top 5 cards of your Arsenal. You may either arrange them in any order or shuffle your Arsenal.
        _effectCatalog["Hmmm"] = new List<Effect>() {new NoEffect()};
        // TODO:Look at the top 5 cards of an opponent's Arsenal. You may either arrange them in any order or make him shuffle his Arsenal.
        _effectCatalog["Don't Think Too Hard"] = new List<Effect>() {new NoEffect()};
        // TODO:Look at opponent's hand.
        _effectCatalog["Whaddya Got?"] = new List<Effect>() {new NoEffect()};
        // TODO:Take a card in your hand, shuffle it into your Arsenal, then draw 2 cards.
        _effectCatalog["Not Yet"] = new List<Effect>() {new NoEffect()};
        _effectCatalog["Jockeying for Position"] = new List<Effect>() {new JockeyingEffect(bonusManager, view)};
        _effectCatalog["Irish Whip"] = new List<Effect>() {new IrishWhipEffect(bonusManager)};
        // TODO:Draw 1 card. Look at opponent's hand, and then make him discard all HEEL cards.
        _effectCatalog["Flash in the Pan"] = new List<Effect>() {new NoEffect()};
        // TODO:Draw 1 card. Look at opponent's hand, and then make him discard all FACE cards.
        _effectCatalog["View of Villainy"] = new List<Effect>() {new NoEffect()};
        // TODO:Playable only if your Fortitude Rating is less than your opponent's Fortitude Rating. Remove any 1 card in opponent's Ring area with a 
        // D value less than or equal to your total Fortitude Rating and place it into his Ringside pile.
        _effectCatalog["Shake It Off"] = new List<Effect>() {new NoEffect()};
        _effectCatalog["Offer Handshake"] = new List<Effect>() {new MayDrawCardsEffect(3, view), new DiscardOwnCardEffect(1, view)};
        _effectCatalog["Roll Out of the Ring"] = new List<Effect>() {new DiscardCardsAndReturnToHand(view)};

        // TODO:Draw 1 card. Look at opponent's hand. If he has any cards titled Disqualification! he must discard them.
        _effectCatalog["Distract the Ref"] = new List<Effect>() {new NoEffect()};
        _effectCatalog["Recovery"] = new List<Effect>() {new MoveFromRingsidePileToArsenal(2, view), new DrawCardEffect(1, view)};

        _effectCatalog["Spit At Opponent"] = new List<Effect>() {new DiscardOwnCardEffect(1, view), new OpponentDiscardCardEffect(4, view)};

        // TODO:Draw 1 card. Your next maneuver this turn is +4D and opponent's reversals are +12F.
        _effectCatalog["Get Crowd Support"] = new List<Effect>() {new NoEffect()};

        // TODO:Discard 3 cards of your choice from your hand. All players compare their current Fortitude Rating. The player(s) with a higher Fortitude Rating must remove maneuver and/or reversal cards from their Ring area, putting them into his Ringside pile, until that player's Fortitude Rating is less than or equal to the others.
        _effectCatalog["Comeback!"] = new List<Effect>() {new NoEffect()};

        // TODO:Your next card played is -5F. If opponent forces you to discard a card from your hand, you may choose to discard this card from your hand and then draw up to 2 cards.
        _effectCatalog["Ego Boost"] = new List<Effect>() {new NoEffect()};

        // TODO:Draw 4 cards. At end of turn, discard your hand.
        _effectCatalog["Deluding Yourself"] = new List<Effect>() {new NoEffect()};

        // TODO:Play after a successfully played maneuver. If your next card played this turn is a maneuver of 7D or less your opponent can not reverse it.
        _effectCatalog["Stagger"] = new List<Effect>() {new NoEffect()};

        // TODO:Your next maneuver may not be reversed.
        _effectCatalog["Diversion"] = new List<Effect>() {new NoEffect()};

        // TODO:Choose one: Look through your Arsenal and put 1 card in your hand, shuffle it and end your turn; or go through opponent's Arsenal and put any 3 cards into his Ringside pile, then shuffle his Arsenal.
        _effectCatalog["Marking Out"] = new List<Effect>() {new NoEffect()};

        _effectCatalog["Puppies! Puppies!"] = new List<Effect>() {new MoveFromRingsidePileToArsenal(5, view), new DrawCardEffect(2, view)};

        // TODO:While this card is in your Ring area, at the start of your turn, before your draw segment, opponent must take the top card from his Arsenal and place it into his Ringside pile.
        _effectCatalog["Shane O'Mac"] = new List<Effect>() {new NoEffect()};

        // TODO:Play after a successful Submission maneuver not reversed and end your turn. You and your opponent may not play maneuvers or actions until your opponent reverses the maintained Submission. On your turn, during the main segment, the Submission does its damage, along with any other abilities on the card. If a reversal is played by your opponent or is revealed while applying damage, both Maintain Hold and the Submission maneuver remain in your Ring area, but you may no longer use the ability of this Maintain Hold card.
        _effectCatalog["Maintain Hold"] = new List<Effect>() {new NoEffect()};

        // TODO:Opponent skips his next turn.
        _effectCatalog["Pat & Gerry"] = new List<Effect>() {new NoEffect()};

        // TODO:May not be reversed. Can only be played after a maneuver that does 5D or greater.
        _effectCatalog["Austin Elbow Smash"] = new List<Effect>() {new NoEffect()};

        _effectCatalog["Lou Thesz Press"] = new List<Effect>() {new MayDrawCardsEffect(1, view), new ReturnPredeterminedDamageWhenReversedFromHandEffect(lastPlayInstance, bonusManager, view)};

        // TODO:Reverse any Strike, Grapple or Submission maneuver. End your opponent's turn. If played from your hand, opponent must discard 2 cards, 
        // then take the top 2 cards from his Arsenal and put them into his Ringside pile.
        _effectCatalog["Double Digits"] = new List<Effect>() {new NoEffect()};

        // TODO:-6F on this card if played after the maneuver titled Kick.
        _effectCatalog["Stone Cold Stunner"] = new List<Effect>() {new NoEffect()};

        // TODO:Your next maneuver this turn is +6D, and your opponent's reversal to it is +20F. Draw a card.
        _effectCatalog["Open Up a Can of Whoop-A%$"] = new List<Effect>() {new NoEffect()};

        // TODO:Can only be played after a 5D or greater maneuver. Reversals to this maneuver are +6D.
        _effectCatalog["Undertaker's Flying Clothesline"] = new List<Effect>() {new NoEffect()};

        // TODO:Reverse any maneuver and end your opponent's turn. If played from your hand, take the top 4 cards from your Arsenal and put them in your Ringside pile. Opponent must discard 1 card; next turn, all your maneuvers are +2D, and all opponent's reversals are +25F.
        _effectCatalog["Undertaker Sits Up!"] = new List<Effect>() {new NoEffect()};

        _effectCatalog["Undertaker's Tombstone Piledriver"] = new List<Effect>() {new DiscardCardToDrawOne(view)};

        // TODO:+5D to all your maneuvers and +20F to all of opponent's reversals for the rest of this turn.
        _effectCatalog["Power of Darkness"] = new List<Effect>() {new NoEffect()};

        // TODO:Reverse any Strike, Grapple or Submission maneuver. End your opponent's turn. If played from your hand, opponent discards all cards in 
        // his hand.
        _effectCatalog["Have a Nice Day!"] = new List<Effect>() {new NoEffect()};

        _effectCatalog["Double Arm DDT"] = new List<Effect>() {new ReturnPredeterminedDamageWhenReversedFromHandEffect(lastPlayInstance, bonusManager, view)};
        _effectCatalog["Tree of Woe"] = new List<Effect>() {new OpponentDiscardCardEffect(2, view)};

        // TODO:-6F on this card if Mr. Socko card is in your Ring area. You may play the card titled Maintain Hold after this card as if it were a Submission maneuver.
        _effectCatalog["Mandible Claw"] = new List<Effect>() {new NoEffect()};

        // TODO:Take 1 card from either your Arsenal or Ringside pile and place it into your hand, then shuffle your Arsenal. While Mr. Socko is in your Ring area, all your maneuvers are +1D.
        _effectCatalog["Mr. Socko"] = new List<Effect>() {new NoEffect()};

        _effectCatalog["Leaping Knee to the Face"] = new List<Effect>() {new OpponentDiscardCardEffect(1, view)};

        _effectCatalog["Facebuster"] = new List<Effect>() {new MayDrawCardsEffect(2, view), new ReturnPredeterminedDamageWhenReversedFromHandEffect(lastPlayInstance, bonusManager, view)};

        _effectCatalog["I Am the Game"] = new List<Effect>() {new AddBonusEffect(bonusManager, new EndOfTurnManeuverBonus(3)),new ChooseBetweenDrawOrDiscard(2, view)};

        // TODO:When successfully played, +2D if played after a Strike maneuver. May only reverse the maneuver titled Back Body Drop.
        _effectCatalog["Pedigree"] = new List<Effect>() {new NoEffect()};

        _effectCatalog["Chyna Interferes"] = new List<Effect>() { new DrawCardWhenReversedFromHandEffect(2, view), new ReturnPredeterminedDamageWhenReversedFromHandEffect(lastPlayInstance, bonusManager, view) };

        // TODO:Draw 1 card. Look at your opponent's hand. Your next maneuver this turn is +6D.
        _effectCatalog["Smackdown Hotel"] = new List<Effect>() {new NoEffect()};

        // TODO:Reverse any Strike, Grapple or Submission maneuver. End your opponent's turn. If played from your hand, shuffle up to 5 cards from your Ringside pile into your Arsenal.
        _effectCatalog[
                "Take That Move, Shine It Up Real Nice, Turn That Sumb*tch Sideways, and Stick It Straight Up Your Roody Poo Candy A%$!"] =
            new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, look through your Ringside pile and Arsenal for the card titled The People's Elbow and place that card in your hand, then shuffle your Arsenal. May only reverse a Grapple maneuver, but you must first discard 1 card.
        _effectCatalog["Rock Bottom"] = new List<Effect>() {new NoEffect()};

        // TODO:Take any 2 cards from your Ringside pile and put them into your hand. Then take any 2 cards from your Ringside pile and shuffle them back into your Arsenal.
        _effectCatalog["The People's Eyebrow"] = new List<Effect>() {new NoEffect()};

        // TODO:As a maneuver, this card can only be played if the card titled Rock Bottom is in your Ring area. As an action, place this card back in 
        // your Arsenal, shuffle, then draw 2 cards. Doing this will not cause any damage to opponent.
        _effectCatalog["The People's Elbow"] = new List<Effect>() {new NoEffect()};

        // TODO:Can only be played after a 4D or greater maneuver. Reversals to this maneuver are +6D.
        _effectCatalog["Kane's Flying Clothesline"] = new List<Effect>() {new NoEffect()};

        // TODO:Reverse any maneuver and end your opponent's turn. If played from your hand, take the top 4 cards from your Arsenal and put them in your Ringside pile; next turn, all your maneuvers are +2D, and all opponents reversals are +15F.
        _effectCatalog["Kane's Return!"] = new List<Effect>() {new NoEffect()};

        // TODO:-6F on this card if played after the card titled Kane's Choke Slam.
        _effectCatalog["Kane's Tombstone Piledriver"] = new List<Effect>() {new NoEffect()};

        // TODO:All players discard all cards from their hands. Your opponent places the top 5 cards of his Arsenal into his Ringside pile.
        _effectCatalog["Hellfire & Brimstone"] = new List<Effect>() {new NoEffect()};

        _effectCatalog["Lionsault"] = new List<Effect>() {new OpponentDiscardCardEffect(1, view)};
        _effectCatalog["Y2J"] = new List<Effect>() {new ChooseBetweenDrawOrDiscard(5, view)};

        // TODO:Reverse any Strike, Grapple or Submission maneuver. End your opponent's turn. If played from your hand, opponent must discard 2 cards and all your maneuvers next turn are +2D.
        _effectCatalog["Don't You Never... EVER!"] = new List<Effect>() {new NoEffect()};

        // TODO:You may play the card titled Maintain Hold after this card as if it were a Submission maneuver.
        _effectCatalog["Walls of Jericho"] = new List<Effect>() {new NoEffect()};

        // TODO:Look at your opponent's hand. For the rest of this turn, your opponent's reversals revealed from his Arsenal while applying damage may not reverse your maneuvers.
        _effectCatalog["Ayatollah of Rock 'n' Roll-a"] = new List<Effect>() {new NoEffect()};
    }

    public static List<Effect> GetEffect(string cardTitle)
    {
        if (_effectCatalog.ContainsKey(cardTitle))
        {
            return _effectCatalog[cardTitle];
        }

        return new List<Effect>() {new NoEffect()};
    }
}


