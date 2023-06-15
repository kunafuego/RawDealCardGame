using System.Text.Json;

namespace RawDeal;

public class SuperstarFactory
{
    public Superstar CreateSuperstar(string superstarName)
    {
        var superstarInfo = ReadSuperstarInfo();
        var serializedSuperstar = FindSerializedSuperstar(superstarName, superstarInfo);
        var superstarObject = CreateSuperstarObject(serializedSuperstar);
        return superstarObject;
    }

    private string ReadSuperstarInfo()
    {
        var superstarPath = Path.Combine("data", "superstar.json");
        var superstarInfo = File.ReadAllText(superstarPath);
        return superstarInfo;
    }

    private DeserializedSuperstars FindSerializedSuperstar(string superstarName,
        string superstarInfo)
    {
        var superstarSerializer =
            JsonSerializer.Deserialize<List<DeserializedSuperstars>>(superstarInfo);
        var serializedSuperstar = superstarSerializer.Find(x => superstarName.Contains(x.Name));
        return serializedSuperstar;
    }

    private Superstar CreateSuperstarObject(DeserializedSuperstars serializedSuperstar)
    {
        var superstarObject = new Superstar(serializedSuperstar);
        return superstarObject;
    }
}