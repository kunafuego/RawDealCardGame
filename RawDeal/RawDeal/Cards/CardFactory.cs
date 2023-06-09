using RawDeal.Effects;
using RawDeal.Preconditions;

namespace RawDeal;

public class CardFactory
{
    public Card CreateCard(DeserializedCards deserializedCard)
    {
        return new Card(deserializedCard,
            PreconditionCatalog.GetPrecondition(deserializedCard.Title),
            EffectCatalog.GetEffect(deserializedCard.Title));
    }
}