using System.Text.Json;

namespace RawDeal;

public static class DeckCreator
{
    private static Superstar _superstar;

    public static Superstar GetDeckSuperstar()
    {
        return _superstar;
    }
    
    public static Deck InitializeDeck(List<string> listOfStringsWithNamesOfCardsInDeck)
    {
        _superstar = GetDecksSuperstar(listOfStringsWithNamesOfCardsInDeck);
        RemoveSuperstarFromListOfDecksCards(listOfStringsWithNamesOfCardsInDeck);
        Deck deck = CreateDeckObject(listOfStringsWithNamesOfCardsInDeck);
        return deck;
    } 
    
    private static Superstar GetDecksSuperstar(List<string> listOfStringsWithDeckContent)
    {
        string superstarName = listOfStringsWithDeckContent[0];
        Superstar superstar = InitializeSuperstar(superstarName);
        return superstar;
    }
    
    private static Superstar InitializeSuperstar(string superstarName)
    {
        string superstarInfo = ReadSuperstarInfo();
        DeserializedSuperstars serializedSuperstar = FindSerializedSuperstar(superstarName, superstarInfo);
        Superstar superstarObject = CreateSuperstarObject(serializedSuperstar);
        return superstarObject;
    }
    
    private static string ReadSuperstarInfo()
    {
        string superstarPath = Path.Combine("data", "superstar.json");
        string superstarInfo = File.ReadAllText(superstarPath);
        return superstarInfo;
    }
    
    private static DeserializedSuperstars FindSerializedSuperstar(string superstarName, string superstarInfo)
    {
        var superstarSerializer = JsonSerializer.Deserialize<List<DeserializedSuperstars>>(superstarInfo);
        var serializedSuperstar = superstarSerializer.Find(x => superstarName.Contains(x.Name));
        return serializedSuperstar;
    }
    
    private static Superstar CreateSuperstarObject(DeserializedSuperstars serializedSuperstar)
    {
        Superstar superstarObject = new Superstar(serializedSuperstar.Name, serializedSuperstar.Logo, serializedSuperstar.HandSize, serializedSuperstar.SuperstarValue,
            serializedSuperstar.SuperstarAbility);
        return superstarObject;
    }
    
    private static void RemoveSuperstarFromListOfDecksCards(List<string> listOfStringsWithNamesOfCardsInDeck)
    {
        listOfStringsWithNamesOfCardsInDeck.RemoveAt(0);
    }
    
    private static Deck CreateDeckObject(List<string> deckContent)
    {
        var deckListWithCardsNames = new List<string>(deckContent);
        Deck deck = CreateDeck(deckListWithCardsNames);
        return deck;
    }
    
    private static Deck CreateDeck(List<string> deckListNamesWithCardNames)
    {
        var listWithDeserializedCards = LoadCards();
        List<Card> deckCards = new List<Card>();
        foreach (var card in deckListNamesWithCardNames)
        {
            DeserializedCards deserializedCard = listWithDeserializedCards.Find(x => x.Title == card);
            Card cardObject = CreateCard(deserializedCard);
            deckCards.Add(cardObject);
        }   
        Deck deckObject = new Deck(deckCards);
        return deckObject;
    }
    
    private static List<DeserializedCards> LoadCards()
    {
        string cardsPath = Path.Combine("data", "cards.json");
        string cardsInfo = File.ReadAllText(cardsPath);
        var cardsSerializer = JsonSerializer.Deserialize<List<DeserializedCards>>(cardsInfo);
        return cardsSerializer;
    }
    
    private static Card CreateCard(DeserializedCards deserializedCard)
    {
        return new Card(
            deserializedCard.Title, 
            deserializedCard.Types, 
            deserializedCard.Subtypes, 
            deserializedCard.Fortitude, 
            deserializedCard.Damage, 
            deserializedCard.StunValue, 
            deserializedCard.CardEffect);
    }
}