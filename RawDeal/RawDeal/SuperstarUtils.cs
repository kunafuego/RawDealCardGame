using System.Text.Json;
namespace RawDeal;

public class SuperstarUtils
{
    public List<string> GetSuperstarLogos()
    {
        string superstarPath = Path.Combine("data", "superstar.json");
        string superstarInfo = File.ReadAllText(superstarPath);
        var superstarSerializer = JsonSerializer.Deserialize<List<DeserializedSuperstars>>(superstarInfo);
        List<string> superstarLogos = new List<string>();
        foreach (var superstarSerialized in superstarSerializer)
        {
            superstarLogos.Add(superstarSerialized.Logo);
        }

        return superstarLogos;
    }
}