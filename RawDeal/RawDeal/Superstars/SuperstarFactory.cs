using System.Text.Json;
namespace RawDeal;

public class SuperstarFactory
{
    public Superstar CreateSuperstar(string superstarName)
    {
        string superstarInfo = ReadSuperstarInfo();
        DeserializedSuperstars serializedSuperstar = FindSerializedSuperstar(superstarName, superstarInfo);
        Superstar superstarObject = CreateSuperstarObject(serializedSuperstar);
        return superstarObject;
    }
    
    private string ReadSuperstarInfo()
    {
        string superstarPath = Path.Combine("data", "superstar.json");
        string superstarInfo = File.ReadAllText(superstarPath);
        return superstarInfo;
    }
    
    private DeserializedSuperstars FindSerializedSuperstar(string superstarName, string superstarInfo)
    {
        var superstarSerializer = JsonSerializer.Deserialize<List<DeserializedSuperstars>>(superstarInfo);
        var serializedSuperstar = superstarSerializer.Find(x => superstarName.Contains(x.Name));
        return serializedSuperstar;
    }
    
    private Superstar CreateSuperstarObject(DeserializedSuperstars serializedSuperstar)
    {
        Superstar superstarObject = new Superstar(serializedSuperstar.Name, serializedSuperstar.Logo, serializedSuperstar.HandSize, 
            serializedSuperstar.SuperstarValue,
            serializedSuperstar.SuperstarAbility);
        return superstarObject;
    }
}