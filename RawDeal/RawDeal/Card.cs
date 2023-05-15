namespace RawDeal;
using RawDealView.Options;
using RawDealView.Formatters;
using RawDealView;

public class Card : IViewableCardInfo
{
    private string _title;
    private List<string> _types;
    private List<string> _subTypes;
    private int _fortitude;
    private string _damage;
    private int _stunValue;
    private string _cardEffect;

    public List<string> SubTypes
    {
        get { return _subTypes; }
    }

    public string Damage
    {
        get { return _damage; }
    }
    public string Title{
        get {return _title ;}
    }
    public List<string> Subtypes {
        get { return _subTypes; }
    }
    public string CardEffect
    {
        get { return _cardEffect; }
    }
    public int Fortitude
    { get { return  _fortitude;} } 
    public int StunValue
    { get { return  _stunValue;} } 
    public List<string> Types
    { get { return  _types;} }
    public string Effect
    { get { return  _cardEffect;} } 
    public Card(string title, List<string> types, List<string> subtypes, string fortitude, string damage, string stunValue, string cardEffect)
    {
        _title = title;
        _types = types;
        _subTypes = subtypes;
        _fortitude = Convert.ToInt16(fortitude);
        _damage = damage;
        _stunValue = Convert.ToInt16(stunValue);
        _cardEffect = cardEffect;
    }
    
    public int GetDamage()
    {
        if(_damage == "#") return  0;
        return Convert.ToInt16(_damage);
} 

    public bool HasReversalType()
    {
        return _types.Contains("Reversal");
    }

    public override string ToString()
    {
        return Formatter.CardToString(this);
    }

    public bool CheckIfCanReverseThisManeuver(Card cardPlayed)
    {
        foreach (string subtype in cardPlayed.Subtypes)
        {
            string reverseEffect = "Reverse any " + subtype;
            if (_cardEffect.Contains(reverseEffect))
                return true;
        }

        return false;
    }

}