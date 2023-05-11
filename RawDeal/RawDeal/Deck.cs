using System.Text.Json;

namespace RawDeal;

public class Deck
{
    private readonly  List<Card> _cards;

    public Deck(List<Card> cards)
    {
        _cards = cards;
    }

    public List<Card> Cards
    {
        get { return _cards; }
    }

    public void AddCard(Card card)
    {
        _cards.Add(card);
    }

    public void RemoveCard(Card card)
    {
        _cards.Remove(card);
    }
    public bool IsValid(string superstarLogo)
    {
        if (!CheckIfAmountOfCardsIsOk()) return false;
        if (!CheckIfSubtypesOfCardsAreOk(superstarLogo)) return false;
        return true;
    }

    public bool CheckIfHasReversalCard()
    {
        foreach (Card card in _cards)
        {
            List<string> types = card.Types;
            if (types.Contains("Reversal"))
            {
                return true;
            }
        }

        return false;
    }

    public List<Card> GetReversalCards()
    {
        return _cards.Where(card => card.HasReversalType()).ToList();
    }

    private bool CheckIfAmountOfCardsIsOk()
    {
        return _cards.Count == 60;
    }

    private bool CheckIfSubtypesOfCardsAreOk(string superstarLogo)
    {
        foreach (Card card in _cards)
        {
            List<string> subtypes = card.SubTypes;
            List<Card> cardsWithSameTitle = _cards.FindAll(x => x.Title == card.Title);
            if (cardsWithSameTitle.Count > 1 && subtypes.Contains("Unique") ||
                cardsWithSameTitle.Count > 3 && subtypes.Contains("SetUp") == false ||
                subtypes.Contains("Heel") && _cards.Any(x => x.SubTypes.Contains("Face")) ||
                subtypes.Contains("Face") && _cards.Any(y => y.SubTypes.Contains("Heel")) ||
                CheckIfCardIsFromAnotherSuperstar(subtypes, superstarLogo))
            {
                return false;
            }
        }
        return true;
    }
    
    private bool CheckIfCardIsFromAnotherSuperstar(List<string> subtypes, string superstarLogo)
    {
        List<string> listOfAllSuperstarsLogos = GetSuperstarLogos();
        foreach (var subtype in subtypes)
        {
            if (listOfAllSuperstarsLogos.Contains(subtype) && subtype != superstarLogo)
            {
                return true;
            }
        }

        return false;
    }

    private List<string> GetSuperstarLogos()
    {
        string superstarPath = Path.Combine("data", "superstar.json");
        string superstarInfo = File.ReadAllText(superstarPath);
        var superstarSerializer = JsonSerializer.Deserialize<List<DeserializedSuperstars>>(superstarInfo);
        List<string> superstarLogos = new List<string>();
        foreach (var superstarSerialized in superstarSerializer)
        {
            superstarLogos.Add(superstarSerialized.Logo);
        }

        return superstarLogos;
    }

    public Card GetLastCard()
    {
        return _cards.Last();
    }

    public void AddCardAtBottom(Card cardToInsert)
    {
        _cards.Insert(0, cardToInsert);   
    }

    public bool HasCards()
    {
        Console.WriteLine("Player has " + _cards.Count() + "In Arsenal");
        return _cards.Any();
    }

    public bool HasMoreThanOneCard()
    {
        return _cards.Count() > 1;
    }
    
}