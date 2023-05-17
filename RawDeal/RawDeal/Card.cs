namespace RawDeal;
using RawDealView.Formatters;
public class Card : IViewableCardInfo
{
    private readonly string _title;
    private readonly List<string> _types;
    private readonly List<string> _subTypes;
    private readonly int _fortitude;
    private readonly string _damage;
    private int _reversalDamage;
    private readonly int _stunValue;
    private readonly string _cardEffect;

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

    public int ReversalDamage
    {
        get { return _reversalDamage; }
        set { _reversalDamage = value; }
    }
    public Card(string title, List<string> types, List<string> subtypes, string fortitude, string damage, string stunValue, string cardEffect)
    {
        _title = title;
        _types = types;
        _subTypes = subtypes;
        _fortitude = Convert.ToInt16(fortitude);
        _damage = damage;
        _stunValue = Convert.ToInt16(stunValue);
        _cardEffect = cardEffect;
        _reversalDamage = 0;
    }
    
    public int GetDamage()
    {
        if (_damage == "#")
        {
            if (_reversalDamage == 0)
            {
                return  0;
            }
            return _reversalDamage;
        }
        return Convert.ToInt16(_damage);
    } 

    public bool HasReversalType()
    {
        return _types.Contains("Reversal");
    }

    public bool CheckIfSubtypesContain(string subtype)
    {
        return _subTypes.Contains(subtype);
    }

    public override string ToString()
    {
        return Formatter.CardToString(this);
    }

}