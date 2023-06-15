namespace RawDeal;

public class Superstar
{
    public Superstar(DeserializedSuperstars serializedSuperstar)
    {
        Name = serializedSuperstar.Name;
        Logo = serializedSuperstar.Logo;
        HandSize = serializedSuperstar.HandSize;
        SuperstarValue = serializedSuperstar.SuperstarValue;
        SuperstarAbility = serializedSuperstar.SuperstarAbility;
    }

    public int SuperstarValue { get; }

    public string SuperstarAbility { get; }

    public int HandSize { get; }

    public string Logo { get; }

    public string Name { get; }

    public bool CheckIfNeedToAskToUseAbilityAtBeginningOfTurn()
    {
        return SuperstarAbility.Contains("At the start of your turn") &&
               SuperstarAbility.Contains("may");
    }

    public bool CheckIfHasToUseAbilityAtStartOfTurn()
    {
        return SuperstarAbility.Contains("At the start of your turn") &&
               SuperstarAbility.Contains("must");
    }

    public bool CheckIfNeedToAskToUseAbilityDuringTurn()
    {
        return SuperstarAbility.Contains("Once during your turn") &&
               SuperstarAbility.Contains("may");
    }

    public bool CheckIfHasToUseAbilityDuringDrawSegment()
    {
        return SuperstarAbility.Contains("during your draw segment") &&
               SuperstarAbility.Contains("must");
    }

    public bool CheckIfHasToUseAbilityWhileReceivingDamage()
    {
        return SuperstarAbility.Contains("All damage from opponent");
    }
}