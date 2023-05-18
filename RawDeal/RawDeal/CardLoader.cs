using System.Text.Json;
namespace RawDeal;

public class CardLoader
{
    public List<DeserializedCards> LoadCards()
    {
        string cardsPath = Path.Combine("data", "cards.json");
        string cardsInfo = File.ReadAllText(cardsPath);
        var cardsSerializer = JsonSerializer.Deserialize<List<DeserializedCards>>(cardsInfo);
        return cardsSerializer;
    }
}