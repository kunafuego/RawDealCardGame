namespace RawDeal;

public class DeckValidator
{
    private const int AmountOfCardsToBeEqualUnique = 1;
    private const int AmountOfCardsToBeEqualSetup = 3;
    private string _superstarLogo;
    private List<Card> _cards;
    private SuperstarUtils _superstarUtils;
    int correctAmountOfCards = 60;

    public DeckValidator(string superstarLogo, List<Card> cards)
    {
        _superstarLogo = superstarLogo;
        _cards = cards;
        _superstarUtils = new SuperstarUtils();
    }
    
    public bool CheckIfDeckIsValid()
    {
        if (!CheckIfAmountOfCardsIsOk()) return false;
        if (!CheckIfSubtypesOfCardsAreOk()) return false;
        return true;
    }
    
    private bool CheckIfAmountOfCardsIsOk()
    {
        return _cards.Count == correctAmountOfCards;
    }
    
    private bool CheckIfSubtypesOfCardsAreOk()
    {
        foreach (Card card in _cards)
        {
            List<string> subtypes = card.SubTypes;
            List<Card> cardsWithSameTitle = _cards.FindAll(x => x.Title == card.Title);
            if (cardsWithSameTitle.Count > AmountOfCardsToBeEqualUnique && subtypes.Contains("Unique") ||
                cardsWithSameTitle.Count > AmountOfCardsToBeEqualSetup && subtypes.Contains("SetUp") == false ||
                subtypes.Contains("Heel") && _cards.Any(x => x.SubTypes.Contains("Face")) ||
                subtypes.Contains("Face") && _cards.Any(y => y.SubTypes.Contains("Heel")) ||
                CheckIfCardIsFromAnotherSuperstar(subtypes))
            {
                return false;
            }
        }
        return true;
    }
    private bool CheckIfCardIsFromAnotherSuperstar(List<string> subtypes)
    {
        List<string> listOfAllSuperstarsLogos = _superstarUtils.GetSuperstarLogos();
        return subtypes.Any(subtype => listOfAllSuperstarsLogos.Contains(subtype) && subtype != _superstarLogo);
    }
}