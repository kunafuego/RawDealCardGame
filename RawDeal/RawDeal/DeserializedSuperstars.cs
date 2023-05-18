using System.Text.Json.Serialization;

namespace RawDeal;

public class DeserializedSuperstars
{
    public string Name { get; set; }
    public string Logo { get; set; }
    
    [JsonPropertyName("Hand Size")]
    public int HandSize { get; set; }
    
    [JsonPropertyName("Superstar Value")]
    public int SuperstarValue { get; set; }

    [JsonPropertyName("Superstar Ability")]
    public string SuperstarAbility { get; set; }
}