using RawDeal.Effects.EffectsClasses;

namespace RawDeal.Effects;

public static class EffectCatalog
{
    private static Dictionary<string, List<Effect>> _effectCatalog = new Dictionary<string, List<Effect>>();

    static EffectCatalog()
    {
        // TODO:As an action, you may discard this card to draw 1 card. Doing this will not cause any damage to opponent.
        _effectCatalog["Chop"] = new List<Effect>() {new NoEffect()};

        _effectCatalog["Head Butt"] = new List<Effect>() {new DiscardOwnCardEffect(1)};

        // TODO:When successfully played, all Strike maneuvers are +1D for the rest of this turn.
        _effectCatalog["Haymaker"] = new List<Effect>() {new NoEffect()};

        // TODO:The card titled Irish Whip must be played before playing this card. When successfully played, you may either draw 2 cards, or force opponent to discard 2 cards.
        _effectCatalog["Back Body Drop"] = new List<Effect>() {new NoEffect()};

        // TODO:May only reverse a maneuver played after the card titled Irish Whip.
        _effectCatalog["Shoulder Block"] = new List<Effect>() {new NoEffect()};

        _effectCatalog["Kick"] = new List<Effect>() {new MoveTopCardOfArsenalToRingsidePile()};

        // TODO:The card titled Irish Whip must be played before playing this card. May only reverse a maneuver played after the card titled Irish Whip.
        _effectCatalog["Cross Body Block"] = new List<Effect>() {new NoEffect()};

        // TODO:May only reverse the maneuver titled Kick.
        _effectCatalog["Ensugiri"] = new List<Effect>() {new NoEffect()};

        _effectCatalog["Running Elbow Smash"] = new List<Effect>() {new MoveTopCardOfArsenalToRingsidePile()};

        // TODO:May only reverse the maneuver titled Drop Kick.
        _effectCatalog["Drop Kick"] = new List<Effect>() {new NoEffect()};

        // TODO:Reversals to this maneuver are +2D.
        _effectCatalog["Discus Punch"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, +5D if played after a 5D or greater maneuver.
        _effectCatalog["Superkick"] = new List<Effect>() {new NoEffect()};

        _effectCatalog["Spinning Heel Kick"] = new List<Effect>() {new OpponentDiscardCardEffect(1)};

        // TODO:May only reverse a maneuver played after the card titled Irish Whip.
        _effectCatalog["Spear"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, if your next card played this turn is a maneuver it is +2D.
        _effectCatalog["Clothesline"] = new List<Effect>() {new NoEffect()};

        // TODO:As an action, you may discard this card to draw 1 card. Doing this will not cause any damage to opponent.
        _effectCatalog["Arm Bar Takedown"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, discard 1 card of your choice from your hand.
        _effectCatalog["Arm Drag"] = new List<Effect>() {new DiscardOwnCardEffect(1)};

        // TODO:When successfully played, if your next card played this turn is a Strike maneuver it is +2D.
        _effectCatalog["Snap Mare"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, you may draw 1 card.
        _effectCatalog["Double Leg Takedown"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, you may look at your opponent's hand.
        _effectCatalog["Fireman's Carry"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, opponent must draw 1 card.
        _effectCatalog["Headlock Takedown"] = new List<Effect>() {new NoEffect()};

        // TODO:May only reverse the maneuver titled Belly to Belly Suplex.
        _effectCatalog["Belly to Belly Suplex"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, if your next card played this turn is a maneuver it is +2D.
        _effectCatalog["Atomic Drop"] = new List<Effect>() {new NoEffect()};

        // TODO:May only reverse the maneuver titled Vertical Suplex.
        _effectCatalog["Vertical Suplex"] = new List<Effect>() {new NoEffect()};

        // TODO:May only reverse the maneuver titled Belly to Back Suplex.
        _effectCatalog["Belly to Back Suplex"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, opponent must discard 2 cards.
        _effectCatalog["Pump Handle Slam"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, you may draw 1 card.
        _effectCatalog["Reverse DDT"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, opponent must discard 1 card.
        _effectCatalog["Samoan Drop"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, discard 1 card of your choice from your hand. Look at opponent's hand, then choose and discard 1 card from his hand.
        _effectCatalog["Bulldog"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, you must take the top card of your Arsenal and put it into your Ringside pile. You may draw 1 card.
        _effectCatalog["Fisherman's Suplex"] = new List<Effect>() {new NoEffect()};

        _effectCatalog["DDT"] = new List<Effect>() {new MoveTopCardOfArsenalToRingsidePile(), new OpponentDiscardCardEffect(2)};

        // TODO:When successfully played, opponent must discard 1 card.
        _effectCatalog["Power Slam"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, you may draw 1 card. Add +1D for every maneuver with the word "slam" in its title in your Ring area.
        _effectCatalog["Powerbomb"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, you must take the top card of your Arsenal and put it into your Ringside pile. Opponent must discard 2 cards.
        _effectCatalog["Press Slam"] = new List<Effect>() {new MoveTopCardOfArsenalToRingsidePile(), new OpponentDiscardCardEffect(2)};

        // TODO:As an action, you may discard this card to draw 1 card. Doing this will not cause any damage to opponent.
        _effectCatalog["Collar & Elbow Lockup"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, discard 1 card of your choice from your hand.
        _effectCatalog["Arm Bar"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, opponent must discard 1 card.
        _effectCatalog["Bear Hug"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, look through your Arsenal for the card titled Maintain Hold and place that card in your hand, then shuffle your Arsenal.
        _effectCatalog["Full Nelson"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, opponent must discard 1 card.
        _effectCatalog["Choke Hold"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, opponent must discard 1 card.
        _effectCatalog["Ankle Lock"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, opponent must draw 1 card.
        _effectCatalog["Standing Side Headlock"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, look through your Arsenal for the card titled Maintain Hold and place that card in your hand, then shuffle your Arsenal.
        _effectCatalog["Cobra Clutch"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, shuffle 2 cards from your Ringside pile into your Arsenal.
        _effectCatalog["Chicken Wing"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, look through your Arsenal for the card titled Maintain Hold and place that card in your hand, then shuffle your Arsenal.
        _effectCatalog["Sleeper"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, look through your Arsenal for the card titled Maintain Hold and place that card in your hand, then shuffle your Arsenal.
        _effectCatalog["Camel Clutch"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, opponent must discard 1 card.
        _effectCatalog["Boston Crab"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, opponent must discard 1 card and you may draw 1 card.
        _effectCatalog["Guillotine Stretch"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, you may discard 3 cards, search through your Arsenal and put 1 card into your hand, then shuffle your Arsenal.
        _effectCatalog["Abdominal Stretch"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, opponent must discard 1 card.
        _effectCatalog["Torture Rack"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, opponent must discard 1 card.
        _effectCatalog["Figure Four Leg Lock"] = new List<Effect>() {new NoEffect()};


        _effectCatalog["Step Aside"] = new List<Effect>() {new NoEffect()};

        // TODO:Reverse any Grapple maneuver and end your opponent's turn.
        _effectCatalog["Escape Move"] = new List<Effect>() {new NoEffect()};

        // TODO:Reverse any Submission maneuver and end your opponent's turn.
        _effectCatalog["Break the Hold"] = new List<Effect>() {new NoEffect()};

        // TODO:Can only reverse a Grapple that does 7D or less. End your opponent's turn. # = D of maneuver card being reversed. Read as 0 when in your Ring area.
        _effectCatalog["Rolling Takedown"] = new List<Effect>() {new ReturnHashtagDamageEffect()};

        // TODO:Can only reverse a Strike that does 7D or less. End your opponent's turn. # = D of maneuver card being reversed. Read as 0 when in your Ring area.
        _effectCatalog["Knee to the Gut"] = new List<Effect>() {new ReturnHashtagDamageEffect()};

        // TODO:May reverse any maneuver that does 7D or less. End your opponent's turn.
        _effectCatalog["Elbow to the Face"] = new List<Effect>() {new ReturnPredeterminedDamageWhenReversedFromHandEffect()};

        // TODO:If played from your hand, may reverse the card titled Jockeying for Position. Opponent must discard 4 cards. End your opponent's turn. Draw 1 card.
        _effectCatalog["Clean Break"] = new List<Effect>() {new OpponentDiscardCardEffect(4), new DrawCardEffect(1)};

        // TODO:Reverse any maneuver and end your opponent's turn. If played from your hand draw 1 card.
        _effectCatalog["Manager Interferes"] = new List<Effect>() {new DrawCardWhenReversedFromHandEffect(1), 
                                                                    new ReturnPredeterminedDamageWhenReversedFromHandEffect()};

        // TODO:Reverse any HEEL maneuver or reversal card if that opponent has 5 or more HEEL cards in his Ring area. Opponent is Disqualified and you win the game. This effect happens even if the card titled Disqualification is placed into your Ringside pile while applying damage from a HEEL maneuver or reversal card.
        _effectCatalog["Disqualification!"] = new List<Effect>() {new NoEffect()};

        // TODO:Reverse any ACTION card being played by your opponent from his hand. It is unsuccessful, goes to his Ringside pile, has no effect and ends his turn.
        _effectCatalog["No Chance in Hell"] = new List<Effect>() {new NoEffect()};

        // TODO:Look at the top 5 cards of your Arsenal. You may either arrange them in any order or shuffle your Arsenal.
        _effectCatalog["Hmmm"] = new List<Effect>() {new NoEffect()};

        // TODO:Look at the top 5 cards of an opponent's Arsenal. You may either arrange them in any order or make him shuffle his Arsenal.
        _effectCatalog["Don't Think Too Hard"] = new List<Effect>() {new NoEffect()};

        // TODO:Look at opponent's hand.
        _effectCatalog["Whaddya Got?"] = new List<Effect>() {new NoEffect()};

        // TODO:Take a card in your hand, shuffle it into your Arsenal, then draw 2 cards.
        _effectCatalog["Not Yet"] = new List<Effect>() {new NoEffect()};

        // TODO:As an action, if your next card played is a Grapple maneuver, declare whether it will be +4D or your opponent's reversal to it will be 
        // +8F. As a reversal, may only reverse the card titled Jockeying for Position. If so, you end opponent's turn; and if your next card played on your turn is a Grapple maneuver, declare whether it will be +4D or your opponent's reversal to it will be +8F.
        _effectCatalog["Jockeying for Position"] = new List<Effect>() {new NoEffect()};

        // TODO:As an action, if your next card played is a Strike maneuver it is +5D. As a reversal, may only reverse the card titled Irish Whip. If so, you end opponent's turn; and if your next card played on your turn is a Strike maneuver it is +5D.
        _effectCatalog["Irish Whip"] = new List<Effect>() {new NoEffect()};

        // TODO:Draw 1 card. Look at opponent's hand, and then make him discard all HEEL cards.
        _effectCatalog["Flash in the Pan"] = new List<Effect>() {new NoEffect()};

        // TODO:Draw 1 card. Look at opponent's hand, and then make him discard all FACE cards.
        _effectCatalog["View of Villainy"] = new List<Effect>() {new NoEffect()};

        // TODO:Playable only if your Fortitude Rating is less than your opponent's Fortitude Rating. Remove any 1 card in opponent's Ring area with a 
        // D value less than or equal to your total Fortitude Rating and place it into his Ringside pile.
        _effectCatalog["Shake It Off"] = new List<Effect>() {new NoEffect()};
        
        _effectCatalog["Offer Handshake"] = new List<Effect>() {new DrawCardEffect(3), new DiscardOwnCardEffect(1)};

        // TODO:Discard up to 2 cards from your hand to your Ringside pile. Return an equal number of cards of your choice to your hand from your Ringside pile.
        _effectCatalog["Roll Out of the Ring"] = new List<Effect>() {new NoEffect()};

        // TODO:Draw 1 card. Look at opponent's hand. If he has any cards titled Disqualification! he must discard them.
        _effectCatalog["Distract the Ref"] = new List<Effect>() {new NoEffect()};

        // TODO:Shuffle any 2 cards from your Ringside pile back into your Arsenal. Then draw 1 card.
        _effectCatalog["Recovery"] = new List<Effect>() {new NoEffect()};

        // TODO:Can only be played when you have 2 or more cards in your hand. Discard 1 card and then your opponent discards 4 cards.
        _effectCatalog["Spit At Opponent"] = new List<Effect>() {new NoEffect()};

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

        // TODO:Shuffle up to 5 cards from your Ringside pile into your Arsenal. Then draw 2 cards.
        _effectCatalog["Puppies! Puppies!"] = new List<Effect>() {new NoEffect()};

        // TODO:While this card is in your Ring area, at the start of your turn, before your draw segment, opponent must take the top card from his Arsenal and place it into his Ringside pile.
        _effectCatalog["Shane O'Mac"] = new List<Effect>() {new NoEffect()};

        // TODO:Play after a successful Submission maneuver not reversed and end your turn. You and your opponent may not play maneuvers or actions until your opponent reverses the maintained Submission. On your turn, during the main segment, the Submission does its damage, along with any other abilities on the card. If a reversal is played by your opponent or is revealed while applying damage, both Maintain Hold and the Submission maneuver remain in your Ring area, but you may no longer use the ability of this Maintain Hold card.
        _effectCatalog["Maintain Hold"] = new List<Effect>() {new NoEffect()};

        // TODO:Opponent skips his next turn.
        _effectCatalog["Pat & Gerry"] = new List<Effect>() {new NoEffect()};

        // TODO:May not be reversed. Can only be played after a maneuver that does 5D or greater.
        _effectCatalog["Austin Elbow Smash"] = new List<Effect>() {new NoEffect()};

        // TODO:If played from your hand, may reverse a maneuver played after the card titled Irish Whip. End your opponent's turn. You may draw 1 card.
        _effectCatalog["Lou Thesz Press"] = new List<Effect>() {new NoEffect()};

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

        // TODO:As an action, this card is -30F and -25D, discard this card and draw 1 card.
        _effectCatalog["Undertaker's Tombstone Piledriver"] = new List<Effect>() {new NoEffect()};

        // TODO:+5D to all your maneuvers and +20F to all of opponent's reversals for the rest of this turn.
        _effectCatalog["Power of Darkness"] = new List<Effect>() {new NoEffect()};

        // TODO:Reverse any Strike, Grapple or Submission maneuver. End your opponent's turn. If played from your hand, opponent discards all cards in 
        // his hand.
        _effectCatalog["Have a Nice Day!"] = new List<Effect>() {new NoEffect()};

        // TODO:May only reverse the maneuver titled Back Body Drop.
        _effectCatalog["Double Arm DDT"] = new List<Effect>() {new NoEffect()};

        // TODO:May not be reversed. When successfully played, opponent must discard 2 cards.
        _effectCatalog["Tree of Woe"] = new List<Effect>() {new NoEffect()};

        // TODO:-6F on this card if Mr. Socko card is in your Ring area. You may play the card titled Maintain Hold after this card as if it were a Submission maneuver.
        _effectCatalog["Mandible Claw"] = new List<Effect>() {new NoEffect()};

        // TODO:Take 1 card from either your Arsenal or Ringside pile and place it into your hand, then shuffle your Arsenal. While Mr. Socko is in your Ring area, all your maneuvers are +1D.
        _effectCatalog["Mr. Socko"] = new List<Effect>() {new NoEffect()};

        // TODO:May not be reversed. You must play the card titled Irish Whip before playing this card. When successfully played, opponent discards 1 card.
        _effectCatalog["Leaping Knee to the Face"] = new List<Effect>() {new ReturnHashtagDamageEffect()};

        // TODO:If played from your hand, may reverse a maneuver played after the card titled Irish Whip. End your opponent's turn. You may draw 2 cards.
        _effectCatalog["Facebuster"] = new List<Effect>() {new NoEffect()};

        // TODO:All your maneuvers are +3D for the rest of this turn. Draw 2 cards, or force opponent to discard 2 cards.
        _effectCatalog["I Am the Game"] = new List<Effect>() {new NoEffect()};

        // TODO:When successfully played, +2D if played after a Strike maneuver. May only reverse the maneuver titled Back Body Drop.
        _effectCatalog["Pedigree"] = new List<Effect>() {new NoEffect()};

        // TODO:Reverses any maneuver and ends your opponent's turn. If played from your hand, draw 2 cards.
        _effectCatalog["Chyna Interferes"] = new List<Effect>() { new DrawCardWhenReversedFromHandEffect(2), new ReturnPredeterminedDamageWhenReversedFromHandEffect() };

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

        // TODO:Can only be played after a 4D or greater maneuver. When successfully played, opponent must discard 1 card.
        _effectCatalog["Lionsault"] = new List<Effect>() {new NoEffect()};

        // TODO:Draw up to 5 Cards or force opponent to discard up to 5 cards.
        _effectCatalog["Y2J"] = new List<Effect>() {new NoEffect()};

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


