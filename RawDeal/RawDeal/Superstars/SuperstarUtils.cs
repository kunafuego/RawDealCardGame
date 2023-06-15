using System.Text.Json;

namespace RawDeal;

public class SuperstarUtils
{
    public List<string> GetSuperstarLogos()
    {
        var superstarPath = Path.Combine("data", "superstar.json");
        var superstarInfo = File.ReadAllText(superstarPath);
        var superstarSerializer =
            JsonSerializer.Deserialize<List<DeserializedSuperstars>>(superstarInfo);
        var superstarLogos = new List<string>();
        foreach (var superstarSerialized in superstarSerializer)
            superstarLogos.Add(superstarSerialized.Logo);

        return superstarLogos;
    }
}