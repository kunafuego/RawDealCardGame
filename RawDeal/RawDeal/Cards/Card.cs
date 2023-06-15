using RawDeal.Effects;
using RawDeal.Preconditions;
using RawDealView.Formatters;

namespace RawDeal;

public class Card : IViewableCardInfo
{
    public Card(DeserializedCards deserializedCard, Precondition preconditionObject,
        List<Effect> effectsList)
    {
        Title = deserializedCard.Title;
        Types = deserializedCard.Types;
        SubTypes = deserializedCard.Subtypes;
        Fortitude = Convert.ToInt16(deserializedCard.Fortitude);
        Damage = deserializedCard.Damage;
        StunValue = Convert.ToInt16(deserializedCard.StunValue);
        CardEffect = deserializedCard.CardEffect;
        ReversalDamage = 0;
        Precondition = preconditionObject;
        EffectObject = effectsList;
    }

    public List<string> SubTypes { get; }

    public List<Effect> EffectObject { get; }

    public Precondition Precondition { get; }

    public int ReversalDamage { get; set; }

    public string Damage { get; }

    public string Title { get; }

    public List<string> Subtypes => SubTypes;

    public string CardEffect { get; }

    public int Fortitude { get; }

    public int StunValue { get; }

    public List<string> Types { get; }

    public int GetDamage()
    {
        if (Damage == "#")
        {
            if (ReversalDamage == 0) return 0;
            return ReversalDamage;
        }

        return Convert.ToInt16(Damage);
    }

    public bool HasReversalType()
    {
        return Types.Contains("Reversal");
    }

    public bool CheckIfSubtypesContain(string subtype)
    {
        return SubTypes.Contains(subtype);
    }

    public override string ToString()
    {
        return Formatter.CardToString(this);
    }
}