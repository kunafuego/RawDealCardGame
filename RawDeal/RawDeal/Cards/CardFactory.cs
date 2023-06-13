using RawDeal.Bonus;
using RawDeal.Effects;
using RawDeal.Preconditions;

namespace RawDeal;

public class CardFactory
{
    private LastPlay _lastPlayInstance;
    public CardFactory(LastPlay lastPlayInstance, BonusManager bonusManager)
    {
        PreconditionCatalog.SetPreconditionCatalog(lastPlayInstance);
        EffectCatalog.SetEffectCatalog(lastPlayInstance, bonusManager);
    }
    
    public Card CreateCard(DeserializedCards deserializedCard)
    {
        return new Card(deserializedCard,
            PreconditionCatalog.GetPrecondition(deserializedCard.Title),
            EffectCatalog.GetEffect(deserializedCard.Title));
    }
}