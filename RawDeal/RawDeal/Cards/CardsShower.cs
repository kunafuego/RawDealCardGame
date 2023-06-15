using RawDealView;

namespace RawDeal;

public class CardsShower
{
    private readonly Player _playerNotPlayingRound;
    private readonly Player _playerPlayingRound;
    private readonly View _view;

    public CardsShower(View view, Player playerPlayingRound, Player playerNotPlayingRound)
    {
        _view = view;
        _playerPlayingRound = playerPlayingRound;
        _playerNotPlayingRound = playerNotPlayingRound;
    }

    public void ManageShowingCards()
    {
        var cardSetChosenForShowing = _view.AskUserWhatSetOfCardsHeWantsToSee();
        var cardsObjectsToShow = new List<Card>();
        if (cardSetChosenForShowing.ToString().Contains("Opponents"))
            cardsObjectsToShow = _playerNotPlayingRound.GetCardsToShow(cardSetChosenForShowing);
        else
            cardsObjectsToShow = _playerPlayingRound.GetCardsToShow(cardSetChosenForShowing);
        var cardsStringsToShow = GetCardsAsStringForShowing(cardsObjectsToShow);
        _view.ShowCards(cardsStringsToShow);
    }

    private List<string> GetCardsAsStringForShowing(List<Card> cardsObjectsToShow)
    {
        var cardsStringsToShow = new List<string>();
        foreach (var card in cardsObjectsToShow) cardsStringsToShow.Add(card.ToString());
        return cardsStringsToShow;
    }
}