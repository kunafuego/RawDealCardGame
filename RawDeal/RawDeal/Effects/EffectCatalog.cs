namespace RawDeal.Effects;

public static class EffectCatalog
{
    private static Dictionary<string, Effect> _effectCatalog;

    static EffectCatalog()
    {
        // TODO:As an action, you may discard this card to draw 1 card. Doing this will not cause any damage to opponent.
        _effectCatalog["Chop"] = new NoEffect();

        // TODO:When successfully played, discard 1 card of your choice from your hand.
        _effectCatalog["Head Butt"] = new NoEffect();

        // TODO:When successfully played, all Strike maneuvers are +1D for the rest of this turn.
        _effectCatalog["Haymaker"] = new NoEffect();

        // TODO:The card titled Irish Whip must be played before playing this card. When successfully played, you may either draw 2 cards, or force opponent to discard 2 cards.
        _effectCatalog["Back Body Drop"] = new NoEffect();

        // TODO:May only reverse a maneuver played after the card titled Irish Whip.
        _effectCatalog["Shoulder Block"] = new NoEffect();

        // TODO:When successfully played, you must take the top card of your Arsenal and put it into your Ringside pile.
        _effectCatalog["Kick"] = new NoEffect();

        // TODO:The card titled Irish Whip must be played before playing this card. May only reverse a maneuver played after the card titled Irish Whip.
        _effectCatalog["Cross Body Block"] = new NoEffect();

        // TODO:May only reverse the maneuver titled Kick.
        _effectCatalog["Ensugiri"] = new NoEffect();

        // TODO:When successfully played, you must take the top card of your Arsenal and put it into your Ringside pile.
        _effectCatalog["Running Elbow Smash"] = new NoEffect();

        // TODO:May only reverse the maneuver titled Drop Kick.
        _effectCatalog["Drop Kick"] = new NoEffect();

        // TODO:Reversals to this maneuver are +2D.
        _effectCatalog["Discus Punch"] = new NoEffect();

        // TODO:When successfully played, +5D if played after a 5D or greater maneuver.
        _effectCatalog["Superkick"] = new NoEffect();

        // TODO:When successfully played, opponent must discard 1 card.
        _effectCatalog["Spinning Heel Kick"] = new NoEffect();

        // TODO:May only reverse a maneuver played after the card titled Irish Whip.
        _effectCatalog["Spear"] = new NoEffect();

        // TODO:When successfully played, if your next card played this turn is a maneuver it is +2D.
        _effectCatalog["Clothesline"] = new NoEffect();

        // TODO:As an action, you may discard this card to draw 1 card. Doing this will not cause any damage to opponent.
        _effectCatalog["Arm Bar Takedown"] = new NoEffect();

        // TODO:When successfully played, discard 1 card of your choice from your hand.
        _effectCatalog["Arm Drag"] = new NoEffect();

        // TODO:When successfully played, if your next card played this turn is a Strike maneuver it is +2D.
        _effectCatalog["Snap Mare"] = new NoEffect();

        // TODO:When successfully played, you may draw 1 card.
        _effectCatalog["Double Leg Takedown"] = new NoEffect();

        // TODO:When successfully played, you may look at your opponent's hand.
        _effectCatalog["Fireman's Carry"] = new NoEffect();

        // TODO:When successfully played, opponent must draw 1 card.
        _effectCatalog["Headlock Takedown"] = new NoEffect();

        // TODO:May only reverse the maneuver titled Belly to Belly Suplex.
        _effectCatalog["Belly to Belly Suplex"] = new NoEffect();

        // TODO:When successfully played, if your next card played this turn is a maneuver it is +2D.
        _effectCatalog["Atomic Drop"] = new NoEffect();

        // TODO:May only reverse the maneuver titled Vertical Suplex.
        _effectCatalog["Vertical Suplex"] = new NoEffect();

        // TODO:May only reverse the maneuver titled Belly to Back Suplex.
        _effectCatalog["Belly to Back Suplex"] = new NoEffect();

        // TODO:When successfully played, opponent must discard 2 cards.
        _effectCatalog["Pump Handle Slam"] = new NoEffect();

        // TODO:When successfully played, you may draw 1 card.
        _effectCatalog["Reverse DDT"] = new NoEffect();

        // TODO:When successfully played, opponent must discard 1 card.
        _effectCatalog["Samoan Drop"] = new NoEffect();

        // TODO:When successfully played, discard 1 card of your choice from your hand. Look at opponent's hand, then choose and discard 1 card from his hand.
        _effectCatalog["Bulldog"] = new NoEffect();

        // TODO:When successfully played, you must take the top card of your Arsenal and put it into your Ringside pile. You may draw 1 card.
        _effectCatalog["Fisherman's Suplex"] = new NoEffect();

        // TODO:When successfully played, you must take the top card of your Arsenal and put it into your Ringside pile. Opponent must discard 2 cards._effectCatalog["DDT"] = new NoEffect ();

        // TODO:When successfully played, opponent must discard 1 card.
        _effectCatalog["Power Slam"] = new NoEffect();

        // TODO:When successfully played, you may draw 1 card. Add +1D for every maneuver with the word "slam" in its title in your Ring area.
        _effectCatalog["Powerbomb"] = new NoEffect();

        // TODO:When successfully played, you must take the top card of your Arsenal and put it into your Ringside pile. Opponent must discard 2 cards._effectCatalog["Press Slam"] = new NoEffect ();

        // TODO:As an action, you may discard this card to draw 1 card. Doing this will not cause any damage to opponent.
        _effectCatalog["Collar & Elbow Lockup"] = new NoEffect();

        // TODO:When successfully played, discard 1 card of your choice from your hand.
        _effectCatalog["Arm Bar"] = new NoEffect();

        // TODO:When successfully played, opponent must discard 1 card.
        _effectCatalog["Bear Hug"] = new NoEffect();

        // TODO:When successfully played, look through your Arsenal for the card titled Maintain Hold and place that card in your hand, then shuffle your Arsenal.
        _effectCatalog["Full Nelson"] = new NoEffect();

        // TODO:When successfully played, opponent must discard 1 card.
        _effectCatalog["Choke Hold"] = new NoEffect();

        // TODO:When successfully played, opponent must discard 1 card.
        _effectCatalog["Ankle Lock"] = new NoEffect();

        // TODO:When successfully played, opponent must draw 1 card.
        _effectCatalog["Standing Side Headlock"] = new NoEffect();

        // TODO:When successfully played, look through your Arsenal for the card titled Maintain Hold and place that card in your hand, then shuffle your Arsenal.
        _effectCatalog["Cobra Clutch"] = new NoEffect();

        // TODO:When successfully played, shuffle 2 cards from your Ringside pile into your Arsenal.
        _effectCatalog["Chicken Wing"] = new NoEffect();

        // TODO:When successfully played, look through your Arsenal for the card titled Maintain Hold and place that card in your hand, then shuffle your Arsenal.
        _effectCatalog["Sleeper"] = new NoEffect();

        // TODO:When successfully played, look through your Arsenal for the card titled Maintain Hold and place that card in your hand, then shuffle your Arsenal.
        _effectCatalog["Camel Clutch"] = new NoEffect();

        // TODO:When successfully played, opponent must discard 1 card.
        _effectCatalog["Boston Crab"] = new NoEffect();

        // TODO:When successfully played, opponent must discard 1 card and you may draw 1 card.
        _effectCatalog["Guillotine Stretch"] = new NoEffect();

        // TODO:When successfully played, you may discard 3 cards, search through your Arsenal and put 1 card into your hand, then shuffle your Arsenal.
        _effectCatalog["Abdominal Stretch"] = new NoEffect();

        // TODO:When successfully played, opponent must discard 1 card.
        _effectCatalog["Torture Rack"] = new NoEffect();

        // TODO:When successfully played, opponent must discard 1 card.
        _effectCatalog["Figure Four Leg Lock"] = new NoEffect();

        // LISTO
        _effectCatalog["Step Aside"] = new NoEffect();

        // TODO:Reverse any Grapple maneuver and end your opponent's turn.
        _effectCatalog["Escape Move"] = new NoEffect();

        // TODO:Reverse any Submission maneuver and end your opponent's turn.
        _effectCatalog["Break the Hold"] = new NoEffect();

        // TODO:Can only reverse a Grapple that does 7D or less. End your opponent's turn. # = D of maneuver card being reversed. Read as 0 when in your Ring area.
        _effectCatalog["Rolling Takedown"] = new NoEffect();

        // TODO:Can only reverse a Strike that does 7D or less. End your opponent's turn. # = D of maneuver card being reversed. Read as 0 when in your Ring area.
        _effectCatalog["Knee to the Gut"] = new NoEffect();

        // TODO:May reverse any maneuver that does 7D or less. End your opponent's turn.
        _effectCatalog["Elbow to the Face"] = new NoEffect();

        // TODO:If played from your hand, may reverse the card titled Jockeying for Position. Opponent must discard 4 cards. End your opponent's tur Draw 1 card.
        _effectCatalog["Clean Break"] = new NoEffect();

        // TODO:Reverse any maneuver and end your opponent's turn. If played from your hand draw 1 card.
        _effectCatalog["Manager Interferes"] = new NoEffect();

        // TODO:Reverse any HEEL maneuver or reversal card if that opponent has 5 or more HEEL cards in his Ring area. Opponent is Disqualified and you win the game. This effect happens even if the card titled Disqualification is placed into your Ringside pile while applying damage from a HEEL maneuver or reversal card.
        _effectCatalog["Disqualification!"] = new NoEffect();

        // TODO:Reverse any ACTION card being played by your opponent from his hand. It is unsuccessful, goes to his Ringside pile, has no effect and ends his turn.
        _effectCatalog["No Chance in Hell"] = new NoEffect();

        // TODO:Look at the top 5 cards of your Arsenal. You may either arrange them in any order or shuffle your Arsenal.
        _effectCatalog["Hmmm"] = new NoEffect();

        // TODO:Look at the top 5 cards of an opponent's Arsenal. You may either arrange them in any order or make him shuffle his Arsenal.
        _effectCatalog["Don't Think Too Hard"] = new NoEffect();

        // TODO:Look at opponent's hand.
        _effectCatalog["Whaddya Got?"] = new NoEffect();

        // TODO:Take a card in your hand, shuffle it into your Arsenal, then draw 2 cards.
        _effectCatalog["Not Yet"] = new NoEffect();

        // TODO:As an action, if your next card played is a Grapple maneuver, declare whether it will be +4D or your opponent's reversal to it will be 
        // +8F. As a reversal, may only reverse the card titled Jockeying for Position. If so, you end opponent's turn; and if your next card played on your turn is a Grapple maneuver, declare whether it will be +4D or your opponent's reversal to it will be +8F.
        _effectCatalog["Jockeying for Position"] = new NoEffect();

        // TODO:As an action, if your next card played is a Strike maneuver it is +5D. As a reversal, may only reverse the card titled Irish Whip. If so, you end opponent's turn; and if your next card played on your turn is a Strike maneuver it is +5D.
        _effectCatalog["Irish Whip"] = new NoEffect();

        // TODO:Draw 1 card. Look at opponent's hand, and then make him discard all HEEL cards.
        _effectCatalog["Flash in the Pan"] = new NoEffect();

        // TODO:Draw 1 card. Look at opponent's hand, and then make him discard all FACE cards.
        _effectCatalog["View of Villainy"] = new NoEffect();

        // TODO:Playable only if your Fortitude Rating is less than your opponent's Fortitude Rating. Remove any 1 card in opponent's Ring area with a 
        // D value less than or equal to your total Fortitude Rating and place it into his Ringside pile.
        _effectCatalog["Shake It Off"] = new NoEffect();

        // TODO:Draw up to 3 cards, then discard 1 card.
        _effectCatalog["Offer Handshake"] = new NoEffect();

        // TODO:Discard up to 2 cards from your hand to your Ringside pile. Return an equal number of cards of your choice to your hand from your Ringside pile.
        _effectCatalog["Roll Out of the Ring"] = new NoEffect();

        // TODO:Draw 1 card. Look at opponent's hand. If he has any cards titled Disqualification! he must discard them.
        _effectCatalog["Distract the Ref"] = new NoEffect();

        // TODO:Shuffle any 2 cards from your Ringside pile back into your Arsenal. Then draw 1 card.
        _effectCatalog["Recovery"] = new NoEffect();

        // TODO:Can only be played when you have 2 or more cards in your hand. Discard 1 card and then your opponent discards 4 cards.
        _effectCatalog["Spit At Opponent"] = new NoEffect();

        // TODO:Draw 1 card. Your next maneuver this turn is +4D and opponent's reversals are +12F.
        _effectCatalog["Get Crowd Support"] = new NoEffect();

        // TODO:Discard 3 cards of your choice from your hand. All players compare their current Fortitude Rating. The player(s) with a higher Fortitude Rating must remove maneuver and/or reversal cards from their Ring area, putting them into his Ringside pile, until that player's Fortitude Rating is less than or equal to the others.
        _effectCatalog["Comeback!"] = new NoEffect();

        // TODO:Your next card played is -5F. If opponent forces you to discard a card from your hand, you may choose to discard this card from your hand and then draw up to 2 cards.
        _effectCatalog["Ego Boost"] = new NoEffect();

        // TODO:Draw 4 cards. At end of turn, discard your hand.
        _effectCatalog["Deluding Yourself"] = new NoEffect();

        // TODO:Play after a successfully played maneuver. If your next card played this turn is a maneuver of 7D or less your opponent can not reverse it.
        _effectCatalog["Stagger"] = new NoEffect();

        // TODO:Your next maneuver may not be reversed.
        _effectCatalog["Diversion"] = new NoEffect();

        // TODO:Choose one: Look through your Arsenal and put 1 card in your hand, shuffle it and end your turn; or go through opponent's Arsenal and put any 3 cards into his Ringside pile, then shuffle his Arsenal.
        _effectCatalog["Marking Out"] = new NoEffect();

        // TODO:Shuffle up to 5 cards from your Ringside pile into your Arsenal. Then draw 2 cards.
        _effectCatalog["Puppies! Puppies!"] = new NoEffect();

        // TODO:While this card is in your Ring area, at the start of your turn, before your draw segment, opponent must take the top card from his Arsenal and place it into his Ringside pile.
        _effectCatalog["Shane O'Mac"] = new NoEffect();

        // TODO:Play after a successful Submission maneuver not reversed and end your turn. You and your opponent may not play maneuvers or actions until your opponent reverses the maintained Submission. On your turn, during the main segment, the Submission does its damage, along with any other abilities on the card. If a reversal is played by your opponent or is revealed while applying damage, both Maintain Hold and the Submission maneuver remain in your Ring area, but you may no longer use the ability of this Maintain Hold card.
        _effectCatalog["Maintain Hold"] = new NoEffect();

        // TODO:Opponent skips his next turn.
        _effectCatalog["Pat & Gerry"] = new NoEffect();

        // TODO:May not be reversed. Can only be played after a maneuver that does 5D or greater.
        _effectCatalog["Austin Elbow Smash"] = new NoEffect();

        // TODO:If played from your hand, may reverse a maneuver played after the card titled Irish Whip. End your opponent's turn. You may draw 1 card.
        _effectCatalog["Lou Thesz Press"] = new NoEffect();

        // TODO:Reverse any Strike, Grapple or Submission maneuver. End your opponent's turn. If played from your hand, opponent must discard 2 cards, 
        // then take the top 2 cards from his Arsenal and put them into his Ringside pile.
        _effectCatalog["Double Digits"] = new NoEffect();

        // TODO:-6F on this card if played after the maneuver titled Kick.
        _effectCatalog["Stone Cold Stunner"] = new NoEffect();

        // TODO:Your next maneuver this turn is +6D, and your opponent's reversal to it is +20F. Draw a card.
        _effectCatalog["Open Up a Can of Whoop-A%$"] = new NoEffect();

        // TODO:Can only be played after a 5D or greater maneuver. Reversals to this maneuver are +6D.
        _effectCatalog["Undertaker's Flying Clothesline"] = new NoEffect();

        // TODO:Reverse any maneuver and end your opponent's turn. If played from your hand, take the top 4 cards from your Arsenal and put them in your Ringside pile. Opponent must discard 1 card; next turn, all your maneuvers are +2D, and all opponent's reversals are +25F.
        _effectCatalog["Undertaker Sits Up!"] = new NoEffect();

        // TODO:As an action, this card is -30F and -25D, discard this card and draw 1 card.
        _effectCatalog["Undertaker's Tombstone Piledriver"] = new NoEffect();

        // TODO:+5D to all your maneuvers and +20F to all of opponent's reversals for the rest of this turn.
        _effectCatalog["Power of Darkness"] = new NoEffect();

        // TODO:Reverse any Strike, Grapple or Submission maneuver. End your opponent's turn. If played from your hand, opponent discards all cards in 
        // his hand.
        _effectCatalog["Have a Nice Day!"] = new NoEffect();

        // TODO:May only reverse the maneuver titled Back Body Drop.
        _effectCatalog["Double Arm DDT"] = new NoEffect();

        // TODO:May not be reversed. When successfully played, opponent must discard 2 cards.
        _effectCatalog["Tree of Woe"] = new NoEffect();

        // TODO:-6F on this card if Mr. Socko card is in your Ring area. You may play the card titled Maintain Hold after this card as if it were a Submission maneuver.
        _effectCatalog["Mandible Claw"] = new NoEffect();

        // TODO:Take 1 card from either your Arsenal or Ringside pile and place it into your hand, then shuffle your Arsenal. While Mr. Socko is in your Ring area, all your maneuvers are +1D.
        _effectCatalog["Mr. Socko"] = new NoEffect();

        // TODO:May not be reversed. You must play the card titled Irish Whip before playing this card. When successfully played, opponent discards 1 card.
        _effectCatalog["Leaping Knee to the Face"] = new NoEffect();

        // TODO:If played from your hand, may reverse a maneuver played after the card titled Irish Whip. End your opponent's turn. You may draw 2 cards.
        _effectCatalog["Facebuster"] = new NoEffect();

        // TODO:All your maneuvers are +3D for the rest of this turn. Draw 2 cards, or force opponent to discard 2 cards.
        _effectCatalog["I Am the Game"] = new NoEffect();

        // TODO:When successfully played, +2D if played after a Strike maneuver. May only reverse the maneuver titled Back Body Drop.
        _effectCatalog["Pedigree"] = new NoEffect();

        // TODO:Reverses any maneuver and ends your opponent's turn. If played from your hand, draw 2 cards.
        _effectCatalog["Chyna Interferes"] = new NoEffect();

        // TODO:Draw 1 card. Look at your opponent's hand. Your next maneuver this turn is +6D.
        _effectCatalog["Smackdown Hotel"] = new NoEffect();

        // TODO:Reverse any Strike, Grapple or Submission maneuver. End your opponent's turn. If played from your hand, shuffle up to 5 cards from your Ringside pile into your Arsenal.
        _effectCatalog[
                "Take That Move, Shine It Up Real Nice, Turn That Sumb*tch Sideways, and Stick It Straight Up Your Roody Poo Candy A%$!"] =
            new NoEffect();

        // TODO:When successfully played, look through your Ringside pile and Arsenal for the card titled The People's Elbow and place that card in your hand, then shuffle your Arsenal. May only reverse a Grapple maneuver, but you must first discard 1 card.
        _effectCatalog["Rock Bottom"] = new NoEffect();

        // TODO:Take any 2 cards from your Ringside pile and put them into your hand. Then take any 2 cards from your Ringside pile and shuffle them back into your Arsenal.
        _effectCatalog["The People's Eyebrow"] = new NoEffect();

        // TODO:As a maneuver, this card can only be played if the card titled Rock Bottom is in your Ring area. As an action, place this card back in 
        // your Arsenal, shuffle, then draw 2 cards. Doing this will not cause any damage to opponent.
        _effectCatalog["The People's Elbow"] = new NoEffect();

        // TODO:Can only be played after a 4D or greater maneuver. Reversals to this maneuver are +6D.
        _effectCatalog["Kane's Flying Clothesline"] = new NoEffect();

        // TODO:Reverse any maneuver and end your opponent's turn. If played from your hand, take the top 4 cards from your Arsenal and put them in your Ringside pile; next turn, all your maneuvers are +2D, and all opponents reversals are +15F.
        _effectCatalog["Kane's Return!"] = new NoEffect();

        // TODO:-6F on this card if played after the card titled Kane's Choke Slam.
        _effectCatalog["Kane's Tombstone Piledriver"] = new NoEffect();

        // TODO:All players discard all cards from their hands. Your opponent places the top 5 cards of his Arsenal into his Ringside pile.
        _effectCatalog["Hellfire & Brimstone"] = new NoEffect();

        // TODO:Can only be played after a 4D or greater maneuver. When successfully played, opponent must discard 1 card.
        _effectCatalog["Lionsault"] = new NoEffect();

        // TODO:Draw up to 5 Cards or force opponent to discard up to 5 cards.
        _effectCatalog["Y2J"] = new NoEffect();

        // TODO:Reverse any Strike, Grapple or Submission maneuver. End your opponent's turn. If played from your hand, opponent must discard 2 cards and all your maneuvers next turn are +2D.
        _effectCatalog["Don't You Never... EVER!"] = new NoEffect();

        // TODO:You may play the card titled Maintain Hold after this card as if it were a Submission maneuver.
        _effectCatalog["Walls of Jericho"] = new NoEffect();

        // TODO:Look at your opponent's hand. For the rest of this turn, your opponent's reversals revealed from his Arsenal while applying damage may not reverse your maneuvers.
        _effectCatalog["Ayatollah of Rock 'n' Roll-a"] = new NoEffect();
    }

    public static Effect GetEffect(string cardTitle)
    {
        if (_effectCatalog.ContainsKey(cardTitle))
        {
            return _effectCatalog[cardTitle];
        }

        return new NoEffect();
    }
}


