using RawDealView.Formatters;

namespace RawDeal;

public class Play : IViewablePlayInfo
{
    private Card _cardThatWasReversedBy;

    public Play(Card card, string type)
    {
        Card = card;
        PlayedAs = type.ToUpper();
    }

    public Card Card { get; }

    public IViewableCardInfo CardInfo => Card;

    public string PlayedAs { get; set; }

    public override string ToString()
    {
        return Formatter.PlayToString(this);
    }
}