namespace RawDealView.Formatters;

public interface IViewableCardInfo
{
    string Title { get; }
    int Fortitude { get; }
    string Damage { get; }
    int StunValue { get; }
    List<string> Types { get; }
    List<string> Subtypes { get; }
    string CardEffect { get; }
}