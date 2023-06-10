using RawDeal.Effects;
using RawDeal.Preconditions;

namespace RawDeal;

public class CardFactory
{
    private LastPlay _lastPlayInstance;
    public CardFactory(LastPlay lastPlayInstance)
    {
        PreconditionCatalog.SetPreconditionCatalog(lastPlayInstance);
        EffectCatalog.SetEffectCatalog(lastPlayInstance);
    }
    
    public Card CreateCard(DeserializedCards deserializedCard)
    {
        return new Card(deserializedCard,
            PreconditionCatalog.GetPrecondition(deserializedCard.Title),
            EffectCatalog.GetEffect(deserializedCard.Title));
    }
}