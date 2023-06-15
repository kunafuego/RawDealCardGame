using System.Text.Json;
using RawDeal.Bonus;
using RawDealView;

namespace RawDeal;

public class DeckCreator
{
    private Superstar _superstar;
    private readonly CardLoader _cardLoader;
    private readonly CardFactory _cardFactory;
    private readonly SuperstarFactory _superstarFactory;

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
        Deck deck = CreateDeckObject(namesOfCardsInDeck);
        return deck;
    } 
    
    private void InitializeSuperstar(List<string> listOfStringsWithDeckContent)
    {
        string superstarName = listOfStringsWithDeckContent[0];
        _superstar = _superstarFactory.CreateSuperstar(superstarName);
    }
    
    private void RemoveSuperstarFromListOfDecksCards(List<string> namesOfCardsInDeck)
    {
        namesOfCardsInDeck.RemoveAt(0);
    }
    
    private Deck CreateDeckObject(List<string> deckContent)
    {
        List<string> deckListWithCardsNames = new List<string>(deckContent);
        List<Card> deckCards = CreateCards(deckListWithCardsNames);
        Deck deckObject = new Deck(deckCards);
        return deckObject;
    }
    
    private List<Card> CreateCards(List<string> deckListNamesWithCardNames)
    {
        List<Card> deckCards = new List<Card>();
        List<DeserializedCards> listWithDeserializedCards = _cardLoader.LoadCards();
        
        foreach (var card in deckListNamesWithCardNames)
        {
            DeserializedCards deserializedCard = listWithDeserializedCards.Find(x => x.Title == card);
            Card cardObject = _cardFactory.CreateCard(deserializedCard);
            deckCards.Add(cardObject);
        }
        
        return deckCards;
    }
}