namespace DendroDocs;

public interface IHaveModifiers
{
    [Newtonsoft.Json.JsonProperty(Order = -2)]
    [JsonPropertyOrder(-2)]
    Modifier Modifiers { get; set; }
}
