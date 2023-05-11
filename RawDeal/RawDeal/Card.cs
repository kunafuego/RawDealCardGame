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
        if (damage == "#") damage = "0";
        _damage = Convert.ToInt16(damage);
        _stunValue = Convert.ToInt16(stunValue);
        _cardEffect = cardEffect;
    }

    public bool HasReversalType()
    {
        return _types.Contains("Reversal");
    }

    // public bool CheckIfCanReverseThisPlay(Play play)
    // {
    //     Card cardToBePlayed = play.Card;
    //     foreach (string subtype in cardToBePlayed._subTypes)
    //     {
    //         if(_cardEffect.Contains("Reverse any" + subtype))
    //         {
    //             return true;
    //         }
    //         List<string> types = cardToBePlayed.Types;
    //         else if (types.Contains("Action") && _cardEffect.Contains("Reverse any ACTION"))
    //         {
    //             return true;
    //         }
    //     }  
    // }
    public bool CheckIfCanReverseThisPlay(Play play)
    {
        Card cardToBePlayed = play.Card;

        if (HasMatchingSubTypeReverseEffect(cardToBePlayed))
            return true;

        if (HasActionReverseEffect(cardToBePlayed))
            return true;

        return false;
    }

    private bool HasMatchingSubTypeReverseEffect(Card card)
    {
        foreach (string subtype in card.Subtypes)
        {
            string reverseEffect = "Reverse any " + subtype;
            if (_cardEffect.Contains(reverseEffect))
                return true;
        }

        return false;
    }

    private bool HasActionReverseEffect(Card card)
    {
        List<string> types = card.Types;
        return types.Contains("Action") && _cardEffect.Contains("Reverse any ACTION");
    }
    public override string ToString()
    {
        return Formatter.CardToString(this);
    }  

}