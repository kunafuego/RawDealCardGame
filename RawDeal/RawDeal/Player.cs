using RawDealView.Options;
using RawDealView.Formatters;
using RawDealView;
namespace RawDeal;

public class Player
{
    private Superstar _superstar;
    private int _fortitude = 0;
    private Dictionary<CardSet, Deck> _cardSetToDeck; 
    private Deck _arsenal = new (new List<Card>());
    private Deck _ringSide = new (new List<Card>());
    private Deck _ringArea = new (new List<Card>());
    private Deck _hand = new (new List<Card>());
    
    public Superstar Superstar{ get { return _superstar; }}

    public Player()
    {
        _cardSetToDeck = new Dictionary<CardSet, Deck>
    {
        {CardSet.Hand,   _hand},
        {CardSet.RingArea, _ringArea},
        {CardSet.RingsidePile, _ringSide},
        {CardSet.Arsenal, _arsenal}
    };
    }

    public void UpdateFortitude()
    {
        _fortitude = 0;
        foreach (var card in _ringArea.Cards)
        {
            _fortitude += card.Damage;
        }
    }
    
    public void AssignSuperstar(Superstar superstar)
    {
        _superstar = superstar;
    }
    
    public void AssignArsenal(Deck arsenal)
    {
        _arsenal = arsenal;
    }
    
    public void DrawCardsFromArsenalToHand()
    {
        List<Card> removableCards = new List<Card>();
        for (int i = _arsenal.Cards.Count; i > _arsenal.Cards.Count - _superstar.HandSize ; i--)
        {
            Card cardToPass = _arsenal.Cards[i - 1];
            removableCards.Add(cardToPass);
        }
        MoveManyCardsFromArsenalToHand(removableCards);
    }

    private void MoveManyCardsFromArsenalToHand(List<Card> removableCards)
    {
        foreach (Card card in removableCards)
        {
            _hand.AddCard(card);
            _arsenal.RemoveCard(card);
        }
    }

    public void MovesCardFromArsenalToHandInDrawSegment()
    {
        Card cardToSteal = _arsenal.Cards.Last();
        _hand.AddCard(cardToSteal);
        _arsenal.RemoveCard(cardToSteal);
    }

    public PlayerInfo GetInfo()
    {
        PlayerInfo info = new PlayerInfo(_superstar.Name, _fortitude,
            _hand.Cards.Count,
            _arsenal.Cards.Count);
        return info;
    }

    public List<Card> GetCardsToShow(CardSet cardSetChosenForShowing)
    {
        List<Card> cardsToShow = new List<Card>();
        if (cardSetChosenForShowing == CardSet.Hand)
        {
            cardsToShow = _hand.Cards;
        }
        else if (cardSetChosenForShowing == CardSet.RingArea || 
                 cardSetChosenForShowing == CardSet.OpponentsRingArea)
        {
            cardsToShow = _ringArea.Cards;
        }
        else if (cardSetChosenForShowing == CardSet.RingsidePile || 
                cardSetChosenForShowing == CardSet.OpponentsRingsidePile)
        {
            cardsToShow = _ringSide.Cards;
        }
        return cardsToShow;
    }

 
    public List<Play> GetAvailablePlays()
    {
        List<Play> playsThatCanBePlayed = new List<Play>();
        foreach (Card card in _hand.Cards)
        {
            if (CanPlayCard(card))
            {
                AddPlayablePlays(card, playsThatCanBePlayed);
            }
        }
        return playsThatCanBePlayed;
    }

    private bool CanPlayCard(Card card)
    {
        return card.Fortitude <= _fortitude;
    }

    private void AddPlayablePlays(Card card, List<Play> playsThatCanBePlayed)
    {
        foreach (string type in card.Types)
        {
            if (type != "Reversal")
            {
                Play play = new Play(card, type);
                playsThatCanBePlayed.Add(play);
            }
        }
    }

    public Card ReceivesDamage()
    {
        Card cardToRemove = _arsenal.GetLastCard();
        _arsenal.RemoveCard(cardToRemove);
        _ringSide.AddCard(cardToRemove);
        return cardToRemove;
    }

    public bool HasCards(CardSet deckToCheck)
    {
        Deck cardsToCheck = _cardSetToDeck[deckToCheck];
        return cardsToCheck.HasCards();
    }
    public bool HasCardsInArsenal()
    {
        return _arsenal.HasCards();
    }
    
    public void MoveCardFromHandToRingArea(Play play)
    {
        Card cardToMove = play.Card;
        _hand.RemoveCard(cardToMove);
        _ringArea.AddCard(cardToMove);
    }
    public void MoveCardFromRingsideToArsenal(Card cardToMove)
    {
        _ringSide.RemoveCard(cardToMove);
        _arsenal.AddCardAtBottom(cardToMove);
    }

    public void MoveCardFromHandToRingside(Card cardToMove)
    {
        _hand.RemoveCard(cardToMove);
        _ringSide.AddCard(cardToMove);
    }

    public void MoveCardFromRingsideToHand(Card cardToMove)
    {
        _ringSide.RemoveCard(cardToMove);
        _hand.AddCard(cardToMove);
    }

    public void MoveCardFromHandToArsenal(Card cardToMove)
    {
        _hand.RemoveCard(cardToMove);
        _arsenal.AddCardAtBottom(cardToMove);
    }

    public bool HasMoreThanOneCardInArsenal()
    {
        return _arsenal.HasMoreThanOneCard();
    }
    
    public bool HasMoreThanOneCard(CardSet deckToCheck)
    {
        Deck cardsToCheck = _cardSetToDeck[deckToCheck];
        return cardsToCheck.HasMoreThanOneCard();
    }

    // Abiliies methods
    
    public bool NeedToAskToUseAbilityAtBeginningOfTurn()
    {
        return _superstar.NeedToAskToUseAbilityAtBeginningOfTurn();
    }

    public bool MustUseEffectAtStartOfTurn()
    {
        return _superstar.MustUseEffectAtStartOfTurn();
    }

    public bool NeedToAskToUseAbilityDuringTurn()
    {
        return _superstar.NeedToAskToUseAbilityDuringTurn();
    }
    
    public bool MustUseEffectDuringDrawSegment()
    {
        return _superstar.MustUseEffectDuringDrawSegment();
    }
    
    public bool MustUseEffectWhileReceivingDamage()
    {
        return _superstar.MustUseEffectWhileReceivingDamage();
    }
}