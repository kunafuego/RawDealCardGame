using RawDeal.Effects;
using RawDeal.Preconditions;

namespace RawDeal;

public class CardFactory
{
    public Card CreateCard(DeserializedCards deserializedCard)
    {
        return new Card(
            deserializedCard.Title, 
            deserializedCard.Types, 
            deserializedCard.Subtypes, 
            deserializedCard.Fortitude, 
            deserializedCard.Damage, 
            deserializedCard.StunValue, 
            deserializedCard.CardEffect,
            PreconditionCatalog.GetPrecondition(deserializedCard.Title),
            EffectCatalog.GetEffect(deserializedCard.Title));
    }
}