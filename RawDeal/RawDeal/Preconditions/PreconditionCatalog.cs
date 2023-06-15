using RawDeal.Preconditions.PreconditionClasses;

namespace RawDeal.Preconditions;

public static class PreconditionCatalog
{
    private static readonly Dictionary<string, Precondition> _preconditionsCatalog = new();

    public static void SetPreconditionCatalog(LastPlay lastPlayInstance)
    {
        // TODO:As an action, you may discard this card to draw 1 card. Doing this will not cause any damage to opponent.
        _preconditionsCatalog["Chop"] = new NoPrecondition();

        // TODO:When successfully played, discard 1 card of your choice from your hand.
        _preconditionsCatalog["Head Butt"] = new NoPrecondition();

        // TODO:When successfully played, all Strike maneuvers are +1D for the rest of this turn.
        _preconditionsCatalog["Haymaker"] = new NoPrecondition();

        // TODO:The card titled Irish Whip must be played before playing this card. When successfully played, you may either draw 2 cards, or force opponent to discard 2 cards.
        _preconditionsCatalog["Back Body Drop"] = new AfterPlayingIrishWhip(lastPlayInstance);

        // TODO:May only reverse a maneuver played after the card titled Irish Whip.
        _preconditionsCatalog["Shoulder Block"] =
            new ReverseManeuverPlayAfterIrishWhip(lastPlayInstance, false);

        // TODO:When successfully played, you must take the top card of your Arsenal and put it into your Ringside pile.
        _preconditionsCatalog["Kick"] = new NoPrecondition();

        // TODO:The card titled Irish Whip must be played before playing this card. May only reverse a maneuver played after the card titled Irish Whip.
        _preconditionsCatalog["Cross Body Block"] = new CrossBodyBlock(lastPlayInstance);

        // TODO:May only reverse the maneuver titled Kick.
        _preconditionsCatalog["Ensugiri"] = new ReverseSpecificTitle("Kick", lastPlayInstance);

        // TODO:When successfully played, you must take the top card of your Arsenal and put it into your Ringside pile.
        _preconditionsCatalog["Running Elbow Smash"] = new NoPrecondition();

        // TODO:May only reverse the maneuver titled Drop Kick.
        _preconditionsCatalog["Drop Kick"] =
            new ReverseSpecificTitle("Drop Kick", lastPlayInstance);

        // TODO:Reversals to this maneuver are +2D.
        _preconditionsCatalog["Discus Punch"] = new NoPrecondition();

        // TODO:When successfully played, +5D if played after a 5D or greater maneuver.
        _preconditionsCatalog["Superkick"] = new NoPrecondition();

        // TODO:When successfully played, opponent must discard 1 card.
        _preconditionsCatalog["Spinning Heel Kick"] = new NoPrecondition();

        // TODO:May only reverse a maneuver played after the card titled Irish Whip.
        _preconditionsCatalog["Spear"] =
            new ReverseManeuverPlayAfterIrishWhip(lastPlayInstance, false);

        // TODO:When successfully played, if your next card played this turn is a maneuver it is +2D.
        _preconditionsCatalog["Clothesline"] = new NoPrecondition();

        // TODO:As an action, you may discard this card to draw 1 card. Doing this will not cause any damage to opponent.
        _preconditionsCatalog["Arm Bar Takedown"] = new NoPrecondition();

        // TODO:When successfully played, discard 1 card of your choice from your hand.
        _preconditionsCatalog["Arm Drag"] = new NoPrecondition();

        // TODO:When successfully played, if your next card played this turn is a Strike maneuver it is +2D.
        _preconditionsCatalog["Snap Mare"] = new NoPrecondition();

        // TODO:When successfully played, you may draw 1 card.
        _preconditionsCatalog["Double Leg Takedown"] = new NoPrecondition();

        // TODO:When successfully played, you may look at your opponent's hand.
        _preconditionsCatalog["Fireman's Carry"] = new NoPrecondition();

        // TODO:When successfully played, opponent must draw 1 card.
        _preconditionsCatalog["Headlock Takedown"] = new NoPrecondition();

        // TODO:May only reverse the maneuver titled Belly to Belly Suplex.
        _preconditionsCatalog["Belly to Belly Suplex"] =
            new ReverseSpecificTitle("Belly to Belly Suplex", lastPlayInstance);

        // TODO:When successfully played, if your next card played this turn is a maneuver it is +2D.
        _preconditionsCatalog["Atomic Drop"] = new NoPrecondition();

        // TODO:May only reverse the maneuver titled Vertical Suplex.
        _preconditionsCatalog["Vertical Suplex"] =
            new ReverseSpecificTitle("Vertical Suplex", lastPlayInstance);

        // TODO:May only reverse the maneuver titled Belly to Back Suplex.
        _preconditionsCatalog["Belly to Back Suplex"] =
            new ReverseSpecificTitle("Belly to Back Suplex", lastPlayInstance);

        // TODO:When successfully played, opponent must discard 2 cards.
        _preconditionsCatalog["Pump Handle Slam"] = new NoPrecondition();

        // TODO:When successfully played, you may draw 1 card.
        _preconditionsCatalog["Reverse DDT"] = new NoPrecondition();

        // TODO:When successfully played, opponent must discard 1 card.
        _preconditionsCatalog["Samoan Drop"] = new NoPrecondition();

        // TODO:When successfully played, discard 1 card of your choice from your hand. Look at opponent's hand, then choose and discard 1 card from his hand.
        _preconditionsCatalog["Bulldog"] = new NoPrecondition();

        // TODO:When successfully played, you must take the top card of your Arsenal and put it into your Ringside pile. You may draw 1 card.
        _preconditionsCatalog["Fisherman's Suplex"] = new NoPrecondition();

        // TODO:When successfully played, you must take the top card of your Arsenal and put it into your Ringside pile. Opponent must discard 2 cards._preconditionsCatalog["DDT"] = new NoPrecondition ();

        // TODO:When successfully played, opponent must discard 1 card.
        _preconditionsCatalog["Power Slam"] = new NoPrecondition();

        // TODO:When successfully played, you may draw 1 card. Add +1D for every maneuver with the word "slam" in its title in your Ring area.
        _preconditionsCatalog["Powerbomb"] = new NoPrecondition();

        // TODO:When successfully played, you must take the top card of your Arsenal and put it into your Ringside pile. Opponent must discard 2 cards._preconditionsCatalog["Press Slam"] = new NoPrecondition ();

        // TODO:As an action, you may discard this card to draw 1 card. Doing this will not cause any damage to opponent.
        _preconditionsCatalog["Collar & Elbow Lockup"] = new NoPrecondition();

        // TODO:When successfully played, discard 1 card of your choice from your hand.
        _preconditionsCatalog["Arm Bar"] = new NoPrecondition();

        // TODO:When successfully played, opponent must discard 1 card.
        _preconditionsCatalog["Bear Hug"] = new NoPrecondition();

        // TODO:When successfully played, look through your Arsenal for the card titled Maintain Hold and place that card in your hand, then shuffle your Arsenal.
        _preconditionsCatalog["Full Nelson"] = new NoPrecondition();

        // TODO:When successfully played, opponent must discard 1 card.
        _preconditionsCatalog["Choke Hold"] = new NoPrecondition();

        // TODO:When successfully played, opponent must discard 1 card.
        _preconditionsCatalog["Ankle Lock"] = new NoPrecondition();

        // TODO:When successfully played, opponent must draw 1 card.
        _preconditionsCatalog["Standing Side Headlock"] = new NoPrecondition();

        // TODO:When successfully played, look through your Arsenal for the card titled Maintain Hold and place that card in your hand, then shuffle your Arsenal.
        _preconditionsCatalog["Cobra Clutch"] = new NoPrecondition();

        // TODO:When successfully played, shuffle 2 cards from your Ringside pile into your Arsenal.
        _preconditionsCatalog["Chicken Wing"] = new NoPrecondition();

        // TODO:When successfully played, look through your Arsenal for the card titled Maintain Hold and place that card in your hand, then shuffle your Arsenal.
        _preconditionsCatalog["Sleeper"] = new NoPrecondition();

        // TODO:When successfully played, look through your Arsenal for the card titled Maintain Hold and place that card in your hand, then shuffle your Arsenal.
        _preconditionsCatalog["Camel Clutch"] = new NoPrecondition();

        // TODO:When successfully played, opponent must discard 1 card.
        _preconditionsCatalog["Boston Crab"] = new NoPrecondition();

        // TODO:When successfully played, opponent must discard 1 card and you may draw 1 card.
        _preconditionsCatalog["Guillotine Stretch"] = new NoPrecondition();

        // TODO:When successfully played, you may discard 3 cards, search through your Arsenal and put 1 card into your hand, then shuffle your Arsenal.
        _preconditionsCatalog["Abdominal Stretch"] = new NoPrecondition();

        // TODO:When successfully played, opponent must discard 1 card.
        _preconditionsCatalog["Torture Rack"] = new NoPrecondition();

        // TODO:When successfully played, opponent must discard 1 card.
        _preconditionsCatalog["Figure Four Leg Lock"] = new NoPrecondition();

        // LISTO
        _preconditionsCatalog["Step Aside"] = new ReverseAnyManeuverStrike(lastPlayInstance);

        // TODO:Reverse any Grapple maneuver and end your opponent's turn.
        _preconditionsCatalog["Escape Move"] = new ReverseAnyManeuverGrapple(lastPlayInstance);

        // TODO:Reverse any Submission maneuver and end your opponent's turn.
        _preconditionsCatalog["Break the Hold"] =
            new ReverseAnyManeuverSubmission(lastPlayInstance);

        // TODO:Can only reverse a Grapple that does 7D or less. End your opponent's turn. # = D of maneuver card being reversed. Read as 0 when in your Ring area.
        _preconditionsCatalog["Rolling Takedown"] =
            new ReverseGrappleWithLessThan7(lastPlayInstance);

        // TODO:Can only reverse a Strike that does 7D or less. End your opponent's turn. # = D of maneuver card being reversed. Read as 0 when in your Ring area.
        _preconditionsCatalog["Knee to the Gut"] = new ReverseStrikeWithLessThan7(lastPlayInstance);

        // TODO:May reverse any maneuver that does 7D or less. End your opponent's turn.
        _preconditionsCatalog["Elbow to the Face"] = new ManeuverWithLessThan7(lastPlayInstance);

        // TODO:If played from your hand, may reverse the card titled Jockeying for Position. Opponent must discard 4 cards. End your opponent's tur Draw 1 card.
        _preconditionsCatalog["Clean Break"] = new ReverseJockeyingFromHand(lastPlayInstance);

        // TODO:Reverse any maneuver and end your opponent's turn. If played from your hand draw 1 card.
        _preconditionsCatalog["Manager Interferes"] = new ReverseAnyManeuver(lastPlayInstance);

        // TODO:Reverse any HEEL maneuver or reversal card if that opponent has 5 or more HEEL cards in his Ring area. Opponent is Disqualified and you win the game. This effect happens even if the card titled Disqualification is placed into your Ringside pile while applying damage from a HEEL maneuver or reversal card.
        _preconditionsCatalog["Disqualification!"] = new NoPrecondition();

        // TODO:Reverse any ACTION card being played by your opponent from his hand. It is unsuccessful, goes to his Ringside pile, has no effect and ends his turn.
        _preconditionsCatalog["No Chance in Hell"] = new ReverseAnyAction(lastPlayInstance);

        // TODO:Look at the top 5 cards of your Arsenal. You may either arrange them in any order or shuffle your Arsenal.
        _preconditionsCatalog["Hmmm"] = new NoPrecondition();

        // TODO:Look at the top 5 cards of an opponent's Arsenal. You may either arrange them in any order or make him shuffle his Arsenal.
        _preconditionsCatalog["Don't Think Too Hard"] = new NoPrecondition();

        // TODO:Look at opponent's hand.
        _preconditionsCatalog["Whaddya Got?"] = new NoPrecondition();

        // TODO:Take a card in your hand, shuffle it into your Arsenal, then draw 2 cards.
        _preconditionsCatalog["Not Yet"] = new NoPrecondition();

        // TODO:As an action, if your next card played is a Grapple maneuver, declare whether it will be +4D or your opponent's reversal to it will be 
        // +8F. As a reversal, may only reverse the card titled Jockeying for Position. If so, you end opponent's turn; and if your next card played on your turn is a Grapple maneuver, declare whether it will be +4D or your opponent's reversal to it will be +8F.
        _preconditionsCatalog["Jockeying for Position"] =
            new ReverseSpecificTitle("Jockeying for Position", lastPlayInstance);

        // TODO:As an action, if your next card played is a Strike maneuver it is +5D. As a reversal, may only reverse the card titled Irish Whip. If so, you end opponent's turn; and if your next card played on your turn is a Strike maneuver it is +5D.
        _preconditionsCatalog["Irish Whip"] =
            new ReverseSpecificTitle("Irish Whip", lastPlayInstance);

        // TODO:Draw 1 card. Look at opponent's hand, and then make him discard all HEEL cards.
        _preconditionsCatalog["Flash in the Pan"] = new NoPrecondition();

        // TODO:Draw 1 card. Look at opponent's hand, and then make him discard all FACE cards.
        _preconditionsCatalog["View of Villainy"] = new NoPrecondition();

        // TODO:Playable only if your Fortitude Rating is less than your opponent's Fortitude Rating. Remove any 1 card in opponent's Ring area with a 
        // D value less than or equal to your total Fortitude Rating and place it into his Ringside pile.
        _preconditionsCatalog["Shake It Off"] = new NoPrecondition();

        // TODO:Draw up to 3 cards, then discard 1 card.
        _preconditionsCatalog["Offer Handshake"] = new NoPrecondition();

        // TODO:Discard up to 2 cards from your hand to your Ringside pile. Return an equal number of cards of your choice to your hand from your Ringside pile.
        _preconditionsCatalog["Roll Out of the Ring"] = new NoPrecondition();

        // TODO:Draw 1 card. Look at opponent's hand. If he has any cards titled Disqualification! he must discard them.
        _preconditionsCatalog["Distract the Ref"] = new NoPrecondition();

        // TODO:Shuffle any 2 cards from your Ringside pile back into your Arsenal. Then draw 1 card.
        _preconditionsCatalog["Recovery"] = new NoPrecondition();

        // TODO:Can only be played when you have 2 or more cards in your hand. Discard 1 card and then your opponent discards 4 cards.
        _preconditionsCatalog["Spit At Opponent"] = new TwoOrMoreCardsInHand();

        // TODO:Draw 1 card. Your next maneuver this turn is +4D and opponent's reversals are +12F.
        _preconditionsCatalog["Get Crowd Support"] = new NoPrecondition();

        // TODO:Discard 3 cards of your choice from your hand. All players compare their current Fortitude Rating. The player(s) with a higher Fortitude Rating must remove maneuver and/or reversal cards from their Ring area, putting them into his Ringside pile, until that player's Fortitude Rating is less than or equal to the others.
        _preconditionsCatalog["Comeback!"] = new NoPrecondition();

        // TODO:Your next card played is -5F. If opponent forces you to discard a card from your hand, you may choose to discard this card from your hand and then draw up to 2 cards.
        _preconditionsCatalog["Ego Boost"] = new NoPrecondition();

        // TODO:Draw 4 cards. At end of turn, discard your hand.
        _preconditionsCatalog["Deluding Yourself"] = new NoPrecondition();

        // TODO:Play after a successfully played maneuver. If your next card played this turn is a maneuver of 7D or less your opponent can not reverse it.
        _preconditionsCatalog["Stagger"] = new NoPrecondition();

        // TODO:Your next maneuver may not be reversed.
        _preconditionsCatalog["Diversion"] = new NoPrecondition();

        // TODO:Choose one: Look through your Arsenal and put 1 card in your hand, shuffle it and end your turn; or go through opponent's Arsenal and put any 3 cards into his Ringside pile, then shuffle his Arsenal.
        _preconditionsCatalog["Marking Out"] = new NoPrecondition();

        // TODO:Shuffle up to 5 cards from your Ringside pile into your Arsenal. Then draw 2 cards.
        _preconditionsCatalog["Puppies! Puppies!"] = new NoPrecondition();

        // TODO:While this card is in your Ring area, at the start of your turn, before your draw segment, opponent must take the top card from his Arsenal and place it into his Ringside pile.
        _preconditionsCatalog["Shane O'Mac"] = new NoPrecondition();

        // TODO:Play after a successful Submission maneuver not reversed and end your turn. You and your opponent may not play maneuvers or actions until your opponent reverses the maintained Submission. On your turn, during the main segment, the Submission does its damage, along with any other abilities on the card. If a reversal is played by your opponent or is revealed while applying damage, both Maintain Hold and the Submission maneuver remain in your Ring area, but you may no longer use the ability of this Maintain Hold card.
        _preconditionsCatalog["Maintain Hold"] = new NoPrecondition();

        // TODO:Opponent skips his next turn.
        _preconditionsCatalog["Pat & Gerry"] = new NoPrecondition();

        // TODO:May not be reversed. Can only be played after a maneuver that does 5D or greater.
        _preconditionsCatalog["Austin Elbow Smash"] =
            new AfterXDOrGreaterManeuver(lastPlayInstance, 5);

        // TODO:If played from your hand, may reverse a maneuver played after the card titled Irish Whip. End your opponent's turn. You may draw 1 card.
        _preconditionsCatalog["Lou Thesz Press"] =
            new ReverseManeuverPlayAfterIrishWhip(lastPlayInstance, true);

        // TODO:Reverse any Strike, Grapple or Submission maneuver. End your opponent's turn. If played from your hand, opponent must discard 2 cards, 
        // then take the top 2 cards from his Arsenal and put them into his Ringside pile.
        _preconditionsCatalog["Double Digits"] = new NoPrecondition();

        // TODO:-6F on this card if played after the maneuver titled Kick.
        _preconditionsCatalog["Stone Cold Stunner"] = new NoPrecondition();

        // TODO:Your next maneuver this turn is +6D, and your opponent's reversal to it is +20F. Draw a card.
        _preconditionsCatalog["Open Up a Can of Whoop-A%$"] = new NoPrecondition();

        // TODO:Can only be played after a 5D or greater maneuver. Reversals to this maneuver are +6D.
        _preconditionsCatalog["Undertaker's Flying Clothesline"] = new NoPrecondition();

        // TODO:Reverse any maneuver and end your opponent's turn. If played from your hand, take the top 4 cards from your Arsenal and put them in your Ringside pile. Opponent must discard 1 card; next turn, all your maneuvers are +2D, and all opponent's reversals are +25F.
        _preconditionsCatalog["Undertaker Sits Up!"] = new NoPrecondition();

        // TODO:As an action, this card is -30F and -25D, discard this card and draw 1 card.
        _preconditionsCatalog["Undertaker's Tombstone Piledriver"] = new NoPrecondition();

        // TODO:+5D to all your maneuvers and +20F to all of opponent's reversals for the rest of this turn.
        _preconditionsCatalog["Power of Darkness"] = new NoPrecondition();

        // TODO:Reverse any Strike, Grapple or Submission maneuver. End your opponent's turn. If played from your hand, opponent discards all cards in 
        // his hand.
        _preconditionsCatalog["Have a Nice Day!"] = new NoPrecondition();

        // TODO:May only reverse the maneuver titled Back Body Drop.
        _preconditionsCatalog["Double Arm DDT"] =
            new ReverseSpecificTitle("Back Body Drop", lastPlayInstance);

        // TODO:May not be reversed. When successfully played, opponent must discard 2 cards.
        _preconditionsCatalog["Tree of Woe"] = new NoPrecondition();

        // TODO:-6F on this card if Mr. Socko card is in your Ring area. You may play the card titled Maintain Hold after this card as if it were a Submission maneuver.
        _preconditionsCatalog["Mandible Claw"] = new NoPrecondition();

        // TODO:Take 1 card from either your Arsenal or Ringside pile and place it into your hand, then shuffle your Arsenal. While Mr. Socko is in your Ring area, all your maneuvers are +1D.
        _preconditionsCatalog["Mr. Socko"] = new NoPrecondition();

        // TODO:May not be reversed. You must play the card titled Irish Whip before playing this card. When successfully played, opponent discards 1 card.
        _preconditionsCatalog["Leaping Knee to the Face"] =
            new AfterPlayingIrishWhip(lastPlayInstance);

        // TODO:If played from your hand, may reverse a maneuver played after the card titled Irish Whip. End your opponent's turn. You may draw 2 cards.
        _preconditionsCatalog["Facebuster"] =
            new ReverseManeuverPlayAfterIrishWhip(lastPlayInstance, true);

        // TODO:All your maneuvers are +3D for the rest of this turn. Draw 2 cards, or force opponent to discard 2 cards.
        _preconditionsCatalog["I Am the Game"] = new NoPrecondition();

        // TODO:When successfully played, +2D if played after a Strike maneuver. May only reverse the maneuver titled Back Body Drop.
        _preconditionsCatalog["Pedigree"] = new NoPrecondition();

        // TODO:Reverses any maneuver and ends your opponent's turn. If played from your hand, draw 2 cards.
        _preconditionsCatalog["Chyna Interferes"] = new ReverseAnyManeuver(lastPlayInstance);

        // TODO:Draw 1 card. Look at your opponent's hand. Your next maneuver this turn is +6D.
        _preconditionsCatalog["Smackdown Hotel"] = new NoPrecondition();

        // TODO:Reverse any Strike, Grapple or Submission maneuver. End your opponent's turn. If played from your hand, shuffle up to 5 cards from your Ringside pile into your Arsenal.
        _preconditionsCatalog[
                "Take That Move, Shine It Up Real Nice, Turn That Sumb*tch Sideways, and Stick It Straight Up Your Roody Poo Candy A%$!"] =
            new NoPrecondition();

        // TODO:When successfully played, look through your Ringside pile and Arsenal for the card titled The People's Elbow and place that card in your hand, then shuffle your Arsenal. May only reverse a Grapple maneuver, but you must first discard 1 card.
        _preconditionsCatalog["Rock Bottom"] = new NoPrecondition();

        // TODO:Take any 2 cards from your Ringside pile and put them into your hand. Then take any 2 cards from your Ringside pile and shuffle them back into your Arsenal.
        _preconditionsCatalog["The People's Eyebrow"] = new NoPrecondition();

        // TODO:As a maneuver, this card can only be played if the card titled Rock Bottom is in your Ring area. As an action, place this card back in 
        // your Arsenal, shuffle, then draw 2 cards. Doing this will not cause any damage to opponent.
        _preconditionsCatalog["The People's Elbow"] = new NoPrecondition();

        // TODO:Can only be played after a 4D or greater maneuver. Reversals to this maneuver are +6D.
        _preconditionsCatalog["Kane's Flying Clothesline"] = new NoPrecondition();

        // TODO:Reverse any maneuver and end your opponent's turn. If played from your hand, take the top 4 cards from your Arsenal and put them in your Ringside pile; next turn, all your maneuvers are +2D, and all opponents reversals are +15F.
        _preconditionsCatalog["Kane's Return!"] = new NoPrecondition();

        // TODO:-6F on this card if played after the card titled Kane's Choke Slam.
        _preconditionsCatalog["Kane's Tombstone Piledriver"] = new NoPrecondition();

        // TODO:All players discard all cards from their hands. Your opponent places the top 5 cards of his Arsenal into his Ringside pile.
        _preconditionsCatalog["Hellfire & Brimstone"] = new NoPrecondition();

        // TODO:Can only be played after a 4D or greater maneuver. When successfully played, opponent must discard 1 card.
        _preconditionsCatalog["Lionsault"] = new AfterXDOrGreaterManeuver(lastPlayInstance, 4);

        // TODO:Draw up to 5 Cards or force opponent to discard up to 5 cards.
        _preconditionsCatalog["Y2J"] = new NoPrecondition();

        // TODO:Reverse any Strike, Grapple or Submission maneuver. End your opponent's turn. If played from your hand, opponent must discard 2 cards and all your maneuvers next turn are +2D.
        _preconditionsCatalog["Don't You Never... EVER!"] = new NoPrecondition();

        // TODO:You may play the card titled Maintain Hold after this card as if it were a Submission maneuver.
        _preconditionsCatalog["Walls of Jericho"] = new NoPrecondition();

        // TODO:Look at your opponent's hand. For the rest of this turn, your opponent's reversals revealed from his Arsenal while applying damage may not reverse your maneuvers.
        _preconditionsCatalog["Ayatollah of Rock 'n' Roll-a"] = new NoPrecondition();
    }

    public static Precondition GetPrecondition(string cardTitle)
    {
        if (_preconditionsCatalog.ContainsKey(cardTitle)) return _preconditionsCatalog[cardTitle];

        return new NoPrecondition();
    }
}