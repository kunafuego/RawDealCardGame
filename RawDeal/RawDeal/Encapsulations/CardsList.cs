using System.Collections;

namespace RawDeal;

public class CardsList : IEnumerable<Card>
{
    private List<Card> cards;

    public CardsList()
    {
        cards = new List<Card>();
    }

    public void Add(Card card)
    {
        cards.Add(card);
    }

    public void Remove(Card card)
    {
        cards.Remove(card);
    }
    
    public CardsList FilterCards(Func<Card, bool> predicate)
    {
        return ToCardsList(cards.Where(predicate));
    }

    public void Insert(int position, Card card)
    {
        cards.Insert(position, card);
    }

    public IEnumerator<Card> GetEnumerator()
    {
        return cards.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    private CardsList ToCardsList(IEnumerable<Card> cards)
    {
        CardsList cardList = new CardsList();
        foreach (Card card in cards)
        {
            cardList.Add(card);
        }
        return cardList;
    }
    
    public CardsList FindAll(Func<Card, bool> predicate)
    {
        CardsList resultList = new CardsList();
        foreach (Card card in cards)
        {
            if (predicate(card))
            {
                resultList.Add(card);
            }
        }
        return resultList;
    }
    
    public int Count()
    {
        return cards.Count;
    }
    
    public Card this[int index]
    {
        get
        {
            if (index >= 0 && index < cards.Count)
                return cards[index];
            throw new IndexOutOfRangeException("Index is out of range.");
        }
        set
        {
            if (index >= 0 && index < cards.Count)
                cards[index] = value;
            else
                throw new IndexOutOfRangeException("Index is out of range.");
        }
    }

    public void Clear()
    {
        cards.Clear();
    }
    
}
