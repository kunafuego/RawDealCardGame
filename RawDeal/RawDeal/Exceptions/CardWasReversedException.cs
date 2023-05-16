namespace RawDeal;

public class CardWasReversedException : Exception
{
    public CardWasReversedException(string message) : base(message)
    {
    }
}