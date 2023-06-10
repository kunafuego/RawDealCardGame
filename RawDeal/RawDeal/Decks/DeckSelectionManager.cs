using RawDealView;
namespace RawDeal;

public class DeckSelectionManager
{
    private readonly View _view;
    private readonly string _deckFolder;
    private LastPlay _lastPlayInstance;

    public DeckSelectionManager(View view, string deckFolder, LastPlay lastPlayInstance)
    {
        _view = view;
        _deckFolder = deckFolder;
        _lastPlayInstance = lastPlayInstance;
    }

    public void SelectDeck(Player player)
    {
        List<string> listOfStringsWithNamesOfCardsInDeck = AskPlayerToSelectDeck();
        DeckCreator deckCreator = new DeckCreator(_lastPlayInstance);
        Deck deck = deckCreator.InitializeDeck(listOfStringsWithNamesOfCardsInDeck);
        Superstar superstar = deckCreator.GetDeckSuperstar();
        ValidateDeck(deck, superstar);
        AssignDeckToPlayer(player, deck);
        AssignSuperstarToPlayer(player, superstar);
        DrawCardsToHandForFirstTime(player);
    }

    private List<string> AskPlayerToSelectDeck()
    {
        string deckPath = _view.AskUserToSelectDeck(_deckFolder);
        string[] deckText = File.ReadAllLines(deckPath);
        return new List<string>(deckText);
    }

    private void ValidateDeck(Deck deck, Superstar superstar)
    {
        if (!deck.CheckIfDeckIsValid(superstar.Logo))
        {
            throw new InvalidDeckException("Invalid deck selected!");
        }
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