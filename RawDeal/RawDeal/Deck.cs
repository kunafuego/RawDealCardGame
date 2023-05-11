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