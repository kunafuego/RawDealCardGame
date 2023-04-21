using RawDealView;

namespace RawDeal;

public class Play
{
    private Card _card;
    private string _type;

    public Card Card
    {
        get { return _card; }
    }

    public string Type{
        get {return _type ;}
    }
    
    public Play(Card card, string type)
    {
        _card = card;
        _type = type.ToUpper();
    }

    public string GetCardString()
    {
        string cardAsString = Formatter.CardToString(_card.Title, _card.Fortitude.ToString(), _card.Damage.ToString(), _card.StunValue.ToString(),
            _card.Types, _card.SubTypes, _card.Effect);
        return cardAsString;
    }
    
    public override string ToString()
    {
        return Formatter.PlayToString(_card.ToString(), _type);
    }  
}