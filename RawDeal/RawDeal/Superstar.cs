namespace RawDeal;

public class Superstar
{
    private string _name;
    private int _handSize;
    private string _logo;
    private int _superstarValue;
    private string _superstarAbility;

    public int SuperstarValue{ get { return _superstarValue; } }
    
    public string SuperstarAbility{ get { return _superstarAbility; } }

    public int HandSize{ get { return _handSize; } }

    public string Logo { get { return _logo; } }
    public string Name {get { return _name; } }
    
    public Superstar(string name, string logo, int handSize, int superstarValue, string superstarAbility)
    {
        _name = name;
        _logo = logo;
        _handSize = handSize;
        _superstarValue = superstarValue;
        _superstarAbility = superstarAbility;
    }

    public bool AskToUseAbilityAtBeginningOfTurn()
    {
        return _superstarAbility.Contains("At the start of your turn") && _superstarAbility.Contains("may");
    }

    public bool UseAbilityAtStartOfTurn()
    {
        return _superstarAbility.Contains("At the start of your turn") && _superstarAbility.Contains("must");
    }
    
    public bool AskToUseAbilityDuringTurn()
    {
        return _superstarAbility.Contains("Once during your turn") && _superstarAbility.Contains("may");
    }

    public bool UseAbilityDuringDrawSegment()
    {
        return _superstarAbility.Contains("during your draw segment") && _superstarAbility.Contains("must");
    }
    
    public bool UseAbilityWhileReceivingDamage()
    {
        Console.WriteLine("LA SUPERSTAR ABILITY ESS");
        Console.WriteLine(_superstarAbility);
        Console.WriteLine(_superstarAbility.Contains("All damage from opponent"));
        return _superstarAbility.Contains("All damage from opponent");
    }
    
    
}