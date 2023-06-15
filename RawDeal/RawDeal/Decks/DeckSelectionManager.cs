using RawDeal.Bonus;
using RawDealView;

namespace RawDeal;

public class DeckSelectionManager
{
    private readonly string _deckFolder;
    private readonly View _view;
    private readonly BonusManager _bonusManager;
    private readonly LastPlay _lastPlayInstance;

    public DeckSelectionManager(View view, string deckFolder, LastPlay lastPlayInstance,
        BonusManager bonusManager)
    {
        _view = view;
        _deckFolder = deckFolder;
        _lastPlayInstance = lastPlayInstance;
        _bonusManager = bonusManager;
    }

    public void SelectDeck(Player player)
    {
        var listOfStringsWithNamesOfCardsInDeck = AskPlayerToSelectDeck();
        var deckCreator = new DeckCreator(_lastPlayInstance, _bonusManager, _view);
        var deck = deckCreator.InitializeDeck(listOfStringsWithNamesOfCardsInDeck);
        var superstar = deckCreator.GetDeckSuperstar();
        ValidateDeck(deck, superstar);
        AssignDeckToPlayer(player, deck);
        AssignSuperstarToPlayer(player, superstar);
        DrawCardsToHandForFirstTime(player);
    }

    private List<string> AskPlayerToSelectDeck()
    {
        var deckPath = _view.AskUserToSelectDeck(_deckFolder);
        var deckText = File.ReadAllLines(deckPath);
        return new List<string>(deckText);
    }

    private void ValidateDeck(Deck deck, Superstar superstar)
    {
        if (!deck.CheckIfDeckIsValid(superstar.Logo))
            throw new InvalidDeckException("Invalid deck selected!");
    }

    private void AssignDeckToPlayer(Player player, Deck deck)
    {
        player.AssignArsenal(deck);
    }

    private void AssignSuperstarToPlayer(Player player, Superstar superstar)
    {
        player.AssignSuperstar(superstar);
    }

    private void DrawCardsToHandForFirstTime(Player player)
    {
        player.DrawCardsFromArsenalToHandAtStart();
    }
}