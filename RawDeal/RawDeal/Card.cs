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
    public bool CheckIfCanReverseThisPlay(Play playThatOpponentIsTryingToMake)
    {
        if (HasMatchingSubTypeReverseEffect(playThatOpponentIsTryingToMake))
            return true;

        if (HasActionReverseEffect(playThatOpponentIsTryingToMake))
            return true;

        return false;
    }

    private bool HasMatchingSubTypeReverseEffect(Play playOpponentIsTryingToMake)
    {
        Card cardToBePlayed = playOpponentIsTryingToMake.Card;
        foreach (string subtype in cardToBePlayed.Subtypes)
        {
            string reverseEffect = "Reverse any " + subtype;
            if (_cardEffect.Contains(reverseEffect) && playOpponentIsTryingToMake.PlayedAs == "MANEUVER")
                return true;
        }

        return false;
    }

    private bool HasActionReverseEffect(Play play)
    {
        return play.PlayedAs == "ACTION" && _cardEffect.Contains("Reverse any ACTION");
    }
    public override string ToString()
    {
        return Formatter.CardToString(this);
    }

    public bool CheckIfCanReverseThisManeuver(Card cardPlayed)
    {
        Console.WriteLine("Card Played by Opponent is ");
        Console.WriteLine(cardPlayed.Title);
        Console.WriteLine("Card That was turned over");
        Console.WriteLine(_title);
        Console.WriteLine(_cardEffect);
        foreach (string subtype in cardPlayed.Subtypes)
        {
            string reverseEffect = "Reverse any " + subtype;
            if (_cardEffect.Contains(reverseEffect))
                return true;
        }

        return false;
    }

}