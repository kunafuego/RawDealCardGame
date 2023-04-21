namespace RawDeal;

public class CardInfo
{
    private string _title;
    private int _fortitude;
    private int _damage;
    private int _stunValue;
    private string _cardEffect;
    private List<string> _types;
    private List<string> _subTypes;

    public CardInfo(string title, int fortitude, int damage, int stunValue, string cardEffect, List<string> types, List<string> subTypes)
    {
    _title = title;
    _fortitude = fortitude;
    _damage = damage;
    _stunValue = stunValue;
    _cardEffect = cardEffect;
    _types = types;
    _subTypes = subTypes;
    }
}