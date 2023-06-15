using RawDeal.Bonus;
using RawDealView;

namespace RawDeal;

public class DeckCreator
{
    private readonly CardFactory _cardFactory;
    private readonly CardLoader _cardLoader;
    private readonly SuperstarFactory _superstarFactory;
    private Superstar _superstar;

    public DeckCreator(LastPlay lastPlayInstance, BonusManager bonusManager, View view)
    {
        _cardLoader = new CardLoader();
        _cardFactory = new CardFactory(lastPlayInstance, bonusManager, view);
        _superstarFactory = new SuperstarFactory();
    }

    public Superstar GetDeckSuperstar()
    {
        return _superstar;
    }

    public Deck InitializeDeck(List<string> namesOfCardsInDeck)
    {
        InitializeSuperstar(namesOfCardsInDeck);
        RemoveSuperstarFromListOfDecksCards(namesOfCardsInDeck);
        var deck = CreateDeckObject(namesOfCardsInDeck);
        return deck;
    }

    private void InitializeSuperstar(List<string> listOfStringsWithDeckContent)
    {
        var superstarName = listOfStringsWithDeckContent[0];
        _superstar = _superstarFactory.CreateSuperstar(superstarName);
    }

    private void RemoveSuperstarFromListOfDecksCards(List<string> namesOfCardsInDeck)
    {
        namesOfCardsInDeck.RemoveAt(0);
    }

    private Deck CreateDeckObject(List<string> deckContent)
    {
        var deckListWithCardsNames = new List<string>(deckContent);
        var deckCards = CreateCards(deckListWithCardsNames);
        var deckObject = new Deck(deckCards);
        return deckObject;
    }

    private CardsList CreateCards(List<string> deckListNamesWithCardNames)
    {
        var deckCards = new CardsList();
        var listWithDeserializedCards = _cardLoader.LoadCards();

        foreach (var card in deckListNamesWithCardNames)
        {
            var deserializedCard = listWithDeserializedCards.Find(x => x.Title == card);
            var cardObject = _cardFactory.CreateCard(deserializedCard);
            deckCards.Add(cardObject);
        }

        return deckCards;
    }
}