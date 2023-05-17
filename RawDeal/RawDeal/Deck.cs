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
    public bool CheckIfDeckIsValid(string superstarLogo)
    {
        return DeckValidator.CheckIfDeckIsValid(superstarLogo, _cards);
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

    public Card GetCardOnTop()
    {
        return _cards.Last();
    }

    public void AddCardAtBottom(Card cardToInsert)
    {
        _cards.Insert(0, cardToInsert);   
    }

    public bool CheckIfHasCards()
    {
        return _cards.Any();
    }

    public bool CheckIfHasMoreThanOneCard()
    {
        return _cards.Count() > 1;
    }
}