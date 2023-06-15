using RawDeal.Bonus;
using RawDealView;
using RawDealView.Options;

namespace RawDeal;

public class Player
{
    private Deck _arsenal = new(new List<Card>());
    private readonly Dictionary<CardSet, Deck> _cardSetToDeck;
    private readonly Deck _ringArea = new(new List<Card>());
    private readonly Deck _ringSide = new(new List<Card>());

    public Player()
    {
        _cardSetToDeck = new Dictionary<CardSet, Deck>
        {
            { CardSet.Hand, Hand },
            { CardSet.RingArea, _ringArea },
            { CardSet.RingsidePile, _ringSide },
            { CardSet.Arsenal, _arsenal }
        };
    }

    public Superstar Superstar { get; private set; }

    public Deck Hand { get; } = new(new List<Card>());

    public int Fortitude { get; private set; }

    public void AssignSuperstar(Superstar superstar)
    {
        Superstar = superstar;
    }

    public void AssignArsenal(Deck arsenal)
    {
        _arsenal = arsenal;
    }

    public string GetSuperstarName()
    {
        return Superstar.Name;
    }

    public List<Card> GetReversalCardsThatPlayerCanPlay(BonusManager bonusManager,
        Card cardOpponentIsTryingToPlay)
    {
        var reversalCards = Hand.GetReversalCards();
        var reversalCardsThatCanBePlayed =
            reversalCards.Where(card =>
                    Fortitude >= bonusManager.GetPlayFortitude(cardOpponentIsTryingToPlay, card))
                .ToList();
        if (cardOpponentIsTryingToPlay.Title is "Tree of Woe" or "Austin Elbow Smash"
            or "Leaping Knee to the Face") reversalCardsThatCanBePlayed.Clear();
        return reversalCardsThatCanBePlayed;
    }

    public void UpdateFortitude()
    {
        Fortitude = 0;
        foreach (var card in _ringArea.Cards)
        {
            var cardDamage = card.Damage;
            if (cardDamage == "#") cardDamage = "0";
            Fortitude += Convert.ToInt16(cardDamage);
        }
    }


    public void DrawCardsFromArsenalToHandAtStart()
    {
        var removableCards = new List<Card>();
        var arsenalCards = _arsenal.Cards;
        for (var i = arsenalCards.Count; i > arsenalCards.Count - Superstar.HandSize; i--)
        {
            var cardToPass = _arsenal.Cards[i - 1];
            removableCards.Add(cardToPass);
        }

        MoveManyCardsFromArsenalToHand(removableCards);
    }

    private void MoveManyCardsFromArsenalToHand(List<Card> removableCards)
    {
        foreach (var card in removableCards)
        {
            Hand.AddCard(card);
            _arsenal.RemoveCard(card);
        }
    }

    public void DrawSingleCard()
    {
        var cardToDraw = _arsenal.Cards.Last();
        Hand.AddCard(cardToDraw);
        _arsenal.RemoveCard(cardToDraw);
    }

    public PlayerInfo GetInfo()
    {
        var info = new PlayerInfo(Superstar.Name, Fortitude,
            Hand.Cards.Count,
            _arsenal.Cards.Count);
        return info;
    }

    public List<Card> GetCardsToShow(CardSet cardSetChosenForShowing)
    {
        var cardsToShow = new List<Card>();
        if (cardSetChosenForShowing == CardSet.Hand)
            cardsToShow = Hand.Cards;
        else if (cardSetChosenForShowing == CardSet.RingArea ||
                 cardSetChosenForShowing == CardSet.OpponentsRingArea)
            cardsToShow = _ringArea.Cards;
        else if (cardSetChosenForShowing == CardSet.RingsidePile ||
                 cardSetChosenForShowing == CardSet.OpponentsRingsidePile)
            cardsToShow = _ringSide.Cards;
        return cardsToShow;
    }

    public void MoveArsenalTopCardToRingside()
    {
        var cardToRemove = _arsenal.GetCardOnTop();
        _arsenal.RemoveCard(cardToRemove);
        _ringSide.AddCard(cardToRemove);
    }

    public bool HasCards(CardSet deckToCheck)
    {
        var cardsToCheck = _cardSetToDeck[deckToCheck];
        return cardsToCheck.CheckIfHasCards();
    }

    public bool HasCardsInArsenal()
    {
        return _arsenal.CheckIfHasCards();
    }

    public void MoveCardFromHandToRingArea(Card cardToMove)
    {
        Hand.RemoveCard(cardToMove);
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
        Hand.RemoveCard(cardToMove);
        _ringSide.AddCard(cardToMove);
    }

    public void MoveCardFromRingsideToHand(Card cardToMove)
    {
        _ringSide.RemoveCard(cardToMove);
        Hand.AddCard(cardToMove);
    }

    public void MoveCardFromHandToArsenal(Card cardToMove)
    {
        Hand.RemoveCard(cardToMove);
        _arsenal.AddCardAtBottom(cardToMove);
    }

    public bool CheckIfPlayerHasMoreThanOneCard(CardSet deckToCheck)
    {
        var cardsToCheck = _cardSetToDeck[deckToCheck];
        return cardsToCheck.CheckIfHasMoreThanOneCard();
    }

    public List<Play> GetAvailablePlays()
    {
        return PlayerUtils.GetAvailablePlays(this);
    }

    public bool CheckIfPlayerHasMoreThanOneCardInArsenal()
    {
        return _arsenal.CheckIfHasMoreThanOneCard();
    }

    public bool CheckIfNeededToAskToUseAbilityAtBeginningOfTurn()
    {
        return Superstar.CheckIfNeedToAskToUseAbilityAtBeginningOfTurn();
    }

    public bool MustUseAbilityAtStartOfTurn()
    {
        return Superstar.CheckIfHasToUseAbilityAtStartOfTurn();
    }

    public bool NeedToAskToUseAbilityDuringTurn()
    {
        return Superstar.CheckIfNeedToAskToUseAbilityDuringTurn();
    }

    public bool MustUseAbilityDuringDrawSegment()
    {
        return Superstar.CheckIfHasToUseAbilityDuringDrawSegment();
    }

    public bool MustUseAbilityWhileReceivingDamage()
    {
        return Superstar.CheckIfHasToUseAbilityWhileReceivingDamage();
    }

    public bool CheckIfHasHigherFortitudeThanGiven(int fortitude)
    {
        return Fortitude >= fortitude;
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