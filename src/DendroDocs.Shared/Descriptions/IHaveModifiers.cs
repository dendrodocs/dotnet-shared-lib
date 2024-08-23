namespace DendroDocs;

public interface IHaveModifiers
{
    [JsonProperty(Order = -2)]
    Modifier Modifiers { get; set; }
}
