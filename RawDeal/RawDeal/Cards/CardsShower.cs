using RawDealView;
using RawDealView.Options;
namespace RawDeal;

public class CardsShower
{
    private readonly View _view;
    private readonly Player _playerPlayingRound;
    private readonly Player _playerNotPlayingRound;

    public CardsShower(View view, Player playerPlayingRound, Player playerNotPlayingRound)
    {
        _view = view;
        _playerPlayingRound = playerPlayingRound;
        _playerNotPlayingRound = playerNotPlayingRound;
    }
    
    public void ManageShowingCards()
    {
        CardSet cardSetChosenForShowing = _view.AskUserWhatSetOfCardsHeWantsToSee();
        List<Card> cardsObjectsToShow = new List<Card>();
        if (cardSetChosenForShowing.ToString().Contains("Opponents"))
        {
            cardsObjectsToShow = _playerNotPlayingRound.GetCardsToShow(cardSetChosenForShowing);
        }
        else
        {
            cardsObjectsToShow = _playerPlayingRound.GetCardsToShow(cardSetChosenForShowing);
        }
        List<string> cardsStringsToShow = GetCardsAsStringForShowing(cardsObjectsToShow);
        _view.ShowCards(cardsStringsToShow);
    }
    
    private List<string> GetCardsAsStringForShowing(List<Card> cardsObjectsToShow)
    {
        List<string> cardsStringsToShow = new List<string>();
        foreach (Card card in cardsObjectsToShow)
        {
            cardsStringsToShow.Add(card.ToString());
        }
        return cardsStringsToShow;
    }
}