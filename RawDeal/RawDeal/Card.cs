namespace RawDeal;
using RawDealView;

public class Card
{
    private string _title;
    private List<string> _types;
    private List<string> _subTypes;
    private int _fortitude;
    private int _damage;
    private int _stunValue;
    private string _cardEffect;

    public List<string> SubTypes
    {
        get { return _subTypes; }
    }

    public int Damage
    { get { return  _damage;} } 
    
    public string Title{
        get {return _title ;}
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
        if (damage == "#") damage = "0";
        _damage = Convert.ToInt16(damage);
        _stunValue = Convert.ToInt16(stunValue);
        _cardEffect = cardEffect;
    }

    public override string ToString()
    {
        return Formatter.CardToString(_title, _fortitude.ToString(), _damage.ToString(), _stunValue.ToString(), _types, _subTypes, _cardEffect);
    }  

}