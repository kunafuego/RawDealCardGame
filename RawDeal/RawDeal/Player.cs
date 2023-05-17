using RawDealView.Options;
using RawDealView.Formatters;
using RawDealView;
namespace RawDeal;

public class Player
{
    private Superstar _superstar;
    private int _fortitude = 0;
    private Dictionary<CardSet, Deck> _cardSetToDeck;
    private Deck _arsenal = new(new List<Card>());
    private Deck _ringSide = new(new List<Card>());
    private Deck _ringArea = new(new List<Card>());
    private Deck _hand = new(new List<Card>());

    public Superstar Superstar
    {
        get { return _superstar; }
    }

    public Player()
    {
        _cardSetToDeck = new Dictionary<CardSet, Deck>
        {
            { CardSet.Hand, _hand },
            { CardSet.RingArea, _ringArea },
            { CardSet.RingsidePile, _ringSide },
            { CardSet.Arsenal, _arsenal }
        };
    }

    public bool CheckIfHasReversalCardInHand()
    {
        return _hand.CheckIfHasReversalCard();
    }

    public string GetSuperstarName()
    {
        return _superstar.Name;
    }

    public List<Card> GetReversalCardsThatPlayerCanPlay(EffectForNextMove effectFromPastMove, Card cardOpponentIsTryingToPlay)
    {
        List<Card> reversalCards = _hand.GetReversalCards();
        List<Card> reversalCardsThatCanBePlayed = reversalCards.Where(card => cardOpponentIsTryingToPlay.CheckIfSubtypesContain("Grapple")
            ? _fortitude >= card.Fortitude + effectFromPastMove.FortitudeChange
            :  _fortitude >= card.Fortitude).ToList();
        return reversalCardsThatCanBePlayed;
    }

    public void UpdateFortitude()
    {
        _fortitude = 0;
        foreach (var card in _ringArea.Cards)
        {
            string cardDamage = card.Damage;
            if (cardDamage == "#") cardDamage = "0";
            _fortitude += Convert.ToInt16(cardDamage);
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

    public void DrawCardsFromArsenalToHandAtStart()
    {
        List<Card> removableCards = new List<Card>();
        for (int i = _arsenal.Cards.Count; i > _arsenal.Cards.Count - _superstar.HandSize; i--)
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

    public void DrawSingleCard()
    {
        Card cardToDraw = _arsenal.Cards.Last();
        _hand.AddCard(cardToDraw);
        _arsenal.RemoveCard(cardToDraw);
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

    public void MoveArsenalTopCardToRingside()
    {
        Card cardToRemove = _arsenal.GetCardOnTop();
        _arsenal.RemoveCard(cardToRemove);
        _ringSide.AddCard(cardToRemove);
    }

    public bool HasCards(CardSet deckToCheck)
    {
        Deck cardsToCheck = _cardSetToDeck[deckToCheck];
        return cardsToCheck.CheckIfHasCards();
    }

    public bool HasCardsInArsenal()
    {
        return _arsenal.CheckIfHasCards();
    }

    public void MoveCardFromHandToRingArea(Card cardToMove)
    {
        _hand.RemoveCard(cardToMove);
        _ringArea.AddCard(cardToMove);
        UpdateFortitude();
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

    public bool CheckIfPlayerHasMoreThanOneCardInArsenal()
    {
        return _arsenal.CheckIfHasMoreThanOneCard();
    }

    public bool CheckIfPlayerHasMoreThanOneCard(CardSet deckToCheck)
    {
        Deck cardsToCheck = _cardSetToDeck[deckToCheck];
        return cardsToCheck.CheckIfHasMoreThanOneCard();
    }
    
    public bool CheckIfNeededToAskToUseAbilityAtBeginningOfTurn()
    {
        return _superstar.CheckIfNeedToAskToUseAbilityAtBeginningOfTurn();
    }

    public bool MustUseAbilityAtStartOfTurn()
    {
        return _superstar.CheckIfHasToUseAbilityAtStartOfTurn();
    }

    public bool NeedToAskToUseAbilityDuringTurn()
    {
        return _superstar.CheckIfNeedToAskToUseAbilityDuringTurn();
    }

    public bool MustUseAbilityDuringDrawSegment()
    {
        return _superstar.CheckIfHasToUseAbilityDuringDrawSegment();
    }

    public bool MustUseAbilityWhileReceivingDamage()
    {
        return _superstar.CheckIfHasToUseAbilityWhileReceivingDamage();
    }

    public bool CheckIfHasHigherFortitudeThanGiven(int fortitude)
    {
        return _fortitude >= fortitude;
    }

    public Card GetCardOnTopOfRingside()
    {
        return _ringSide.GetCardOnTop();
    }

    public Card GetCardOnTopOfArsenal()
    {
        return _arsenal.GetCardOnTop();
    }

}