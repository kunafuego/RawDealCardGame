using RawDealView.Options;
using RawDealView.Formatters;
using RawDealView;

namespace RawDeal;

public class Play : IViewablePlayInfo
{
    private Card _card;
    private string _playedAs;
    private Card _cardThatWasReversedBy;

    public Card Card
    {
        get { return _card; }
    }
    public IViewableCardInfo CardInfo
    {
        get { return _card; }
    }

    public string PlayedAs{
        get {return _playedAs ;}
        set { _playedAs = value; }
    }
    
    public Play(Card card, string type)
    {
        _card = card;
        _playedAs = type.ToUpper();
    }

    public override string ToString()
    {
        return Formatter.PlayToString(this);
    }  
}