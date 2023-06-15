namespace RawDeal;

public class DeckValidator
{
    private const int AmountOfCardsToBeEqualUnique = 1;
    private const int AmountOfCardsToBeEqualSetup = 3;
    private const int CorrectAmountOfCards = 60;
    private readonly List<Card> _cards;
    private readonly string _superstarLogo;
    private readonly SuperstarUtils _superstarUtils;

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
        return _cards.Count == CorrectAmountOfCards;
    }

    private bool CheckIfSubtypesOfCardsAreOk()
    {
        foreach (var card in _cards)
        {
            var subtypes = card.SubTypes;
            var cardsWithSameTitle = _cards.FindAll(x => x.Title == card.Title);
            if ((cardsWithSameTitle.Count > AmountOfCardsToBeEqualUnique &&
                 subtypes.Contains("Unique")) ||
                (cardsWithSameTitle.Count > AmountOfCardsToBeEqualSetup &&
                 subtypes.Contains("SetUp") == false) ||
                (subtypes.Contains("Heel") && _cards.Any(x => x.SubTypes.Contains("Face"))) ||
                (subtypes.Contains("Face") && _cards.Any(y => y.SubTypes.Contains("Heel"))) ||
                CheckIfCardIsFromAnotherSuperstar(subtypes))
                return false;
        }

        return true;
    }

    private bool CheckIfCardIsFromAnotherSuperstar(List<string> subtypes)
    {
        var listOfAllSuperstarsLogos = _superstarUtils.GetSuperstarLogos();
        return subtypes.Any(subtype =>
            listOfAllSuperstarsLogos.Contains(subtype) && subtype != _superstarLogo);
    }
}