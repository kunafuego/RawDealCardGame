using System.Text.Json;

namespace RawDeal;

public class CardLoader
{
    public List<DeserializedCards> LoadCards()
    {
        var cardsPath = Path.Combine("data", "cards.json");
        var cardsInfo = File.ReadAllText(cardsPath);
        var cardsSerializer = JsonSerializer.Deserialize<List<DeserializedCards>>(cardsInfo);
        return cardsSerializer;
    }
}