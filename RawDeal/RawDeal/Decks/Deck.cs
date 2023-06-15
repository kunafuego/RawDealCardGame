namespace RawDeal;

public class Deck
{
    public Deck(List<Card> cards)
    {
        Cards = cards;
    }

    public List<Card> Cards { get; }

    public void AddCard(Card card)
    {
        Cards.Add(card);
    }

    public void RemoveCard(Card card)
    {
        Cards.Remove(card);
    }

    public bool CheckIfDeckIsValid(string superstarLogo)
    {
        var deckValidator = new DeckValidator(superstarLogo, Cards);
        return deckValidator.CheckIfDeckIsValid();
    }

    public bool CheckIfHasReversalCard()
    {
        foreach (var card in Cards)
        {
            var types = card.Types;
            if (types.Contains("Reversal")) return true;
        }

        return false;
    }

    public List<Card> GetReversalCards()
    {
        return Cards.Where(card => card.HasReversalType()).ToList();
    }

    public Card GetCardOnTop()
    {
        return Cards.Last();
    }

    public void AddCardAtBottom(Card cardToInsert)
    {
        Cards.Insert(0, cardToInsert);
    }

    public bool CheckIfHasCards()
    {
        return Cards.Any();
    }

    public bool CheckIfHasMoreThanOneCard()
    {
        return Cards.Count() > 1;
    }
}