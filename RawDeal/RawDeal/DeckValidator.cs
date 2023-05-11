using System.Text.Json;

namespace RawDeal;

public class DeckValidator
{
    private static string _superstarLogo;
    private static List<Card> _cards;

    public static bool CheckIfDeckIsValid(string superstarLogo, List<Card> cards)
    {
        _superstarLogo = superstarLogo;
        _cards = cards;
        if (!CheckIfAmountOfCardsIsOk()) return false;
        if (!CheckIfSubtypesOfCardsAreOk()) return false;
        return true;
    }
    
    private static bool CheckIfAmountOfCardsIsOk()
    {
        return _cards.Count == 60;
    }
    
    private static bool CheckIfSubtypesOfCardsAreOk()
    {
        foreach (Card card in _cards)
        {
            List<string> subtypes = card.SubTypes;
            List<Card> cardsWithSameTitle = _cards.FindAll(x => x.Title == card.Title);
            if (cardsWithSameTitle.Count > 1 && subtypes.Contains("Unique") ||
                cardsWithSameTitle.Count > 3 && subtypes.Contains("SetUp") == false ||
                subtypes.Contains("Heel") && _cards.Any(x => x.SubTypes.Contains("Face")) ||
                subtypes.Contains("Face") && _cards.Any(y => y.SubTypes.Contains("Heel")) ||
                CheckIfCardIsFromAnotherSuperstar(subtypes))
            {
                return false;
            }
        }
        return true;
    }
    private static bool CheckIfCardIsFromAnotherSuperstar(List<string> subtypes)
    {
        List<string> listOfAllSuperstarsLogos = GetSuperstarLogos();
        foreach (var subtype in subtypes)
        {
            if (listOfAllSuperstarsLogos.Contains(subtype) && subtype != _superstarLogo)
            {
                return true;
            }
        }

        return false;
    }
    
    private static
        List<string> GetSuperstarLogos()
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
}