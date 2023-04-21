using System.Text.Json;

namespace RawDeal;

public class Deck
{
    private readonly  List<Card> _cards;
    private Superstar _superstar;

    public Deck(List<Card> cards)
    {
        _cards = cards;
    }

    public void AssignSuperstar(Superstar superstar)
    {
        _superstar = superstar;
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
    public bool IsValid()
    {
        bool checkQuantity = QuantityOk();
        bool checkSubTypes = SubTypesOk();
        if (checkQuantity && checkSubTypes)
        {
            return true;
        }

        return false;
    }


    public bool QuantityOk()
    {
        if (_cards.Count == 60)
        {
            return true;
        }

        return false;
    }

    public bool SubTypesOk()
    {
        List<string> superstarLogos = GetSuperstarLogos();
        foreach (Card card in _cards)
        {
            List<string> subtypes = card.SubTypes;
            List<Card> cardsWithSameTitle = _cards.FindAll(x => x.Title == card.Title);
            if (cardsWithSameTitle.Count > 1 && subtypes.Contains("Unique") ||
                cardsWithSameTitle.Count > 3 && subtypes.Contains("SetUp") == false ||
                subtypes.Contains("Heel") && _cards.Any(x => x.SubTypes.Contains("Face")) ||
                subtypes.Contains("Face") && _cards.Any(y => y.SubTypes.Contains("Heel")))
            {
                return false;
            }

            foreach (var subtype in subtypes)
            {
                if (superstarLogos.Contains(subtype) && subtype != _superstar.Logo)
                {
                    return false;
                }
            }
        }

        return true;
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