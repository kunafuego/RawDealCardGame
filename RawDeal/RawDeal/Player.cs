using RawDeal.Bonus;
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

    public Superstar Superstar { get { return _superstar; } }
    
    public Deck Hand 
    { get { return _hand; } }
    
    public int Fortitude
    { get { return  _fortitude;} } 
    
    public void AssignSuperstar(Superstar superstar) { _superstar = superstar; }

    public void AssignArsenal(Deck arsenal) { _arsenal = arsenal; }

    public string GetSuperstarName() { return _superstar.Name; }
    
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

    public List<Card> GetReversalCardsThatPlayerCanPlay(BonusManager bonusManager, Card cardOpponentIsTryingToPlay)
    {
        List<Card> reversalCards = _hand.GetReversalCards();
        List<Card> reversalCardsThatCanBePlayed =
            reversalCards.Where(card => _fortitude >= bonusManager.GetPlayFortitude(cardOpponentIsTryingToPlay, card)).ToList();
        if(cardOpponentIsTryingToPlay.Title is "Tree of Woe" or "Austin Elbow Smash" or "Leaping Knee to the Face") reversalCardsThatCanBePlayed.Clear();
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


    public void DrawCardsFromArsenalToHandAtStart()
    {
        List<Card> removableCards = new List<Card>();
        List<Card> arsenalCards = _arsenal.Cards;
        for (int i = arsenalCards.Count; i > arsenalCards.Count - _superstar.HandSize; i--)
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
    public bool HasCardsInArsenal() { return _arsenal.CheckIfHasCards(); }
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

    public bool CheckIfPlayerHasMoreThanOneCard(CardSet deckToCheck)
    {
        Deck cardsToCheck = _cardSetToDeck[deckToCheck];
        return cardsToCheck.CheckIfHasMoreThanOneCard();
    }
    public List<Play> GetAvailablePlays() { return PlayerUtils.GetAvailablePlays(this); }
    public bool CheckIfPlayerHasMoreThanOneCardInArsenal() { return _arsenal.CheckIfHasMoreThanOneCard(); }
    public bool CheckIfNeededToAskToUseAbilityAtBeginningOfTurn() { return _superstar.CheckIfNeedToAskToUseAbilityAtBeginningOfTurn(); }
    public bool MustUseAbilityAtStartOfTurn() { return _superstar.CheckIfHasToUseAbilityAtStartOfTurn(); }
    public bool NeedToAskToUseAbilityDuringTurn() { return _superstar.CheckIfNeedToAskToUseAbilityDuringTurn(); }
    public bool MustUseAbilityDuringDrawSegment() { return _superstar.CheckIfHasToUseAbilityDuringDrawSegment(); }
    public bool MustUseAbilityWhileReceivingDamage() { return _superstar.CheckIfHasToUseAbilityWhileReceivingDamage(); }
    public bool CheckIfHasHigherFortitudeThanGiven(int fortitude) { return _fortitude >= fortitude; }
    public Card GetCardOnTopOfRingside() { return _ringSide.GetCardOnTop(); }
    public Card GetCardOnTopOfArsenal() { return _arsenal.GetCardOnTop(); }
}