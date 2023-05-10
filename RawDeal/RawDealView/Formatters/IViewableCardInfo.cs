namespace RawDealView.Formatters;

public interface IViewableCardInfo
{
    string Title { get; }
    int Fortitude { get; }
    int Damage { get; }
    int StunValue { get; }
    List<string> Types { get; }
    List<string> Subtypes { get; }
    string CardEffect { get; }
}