namespace DendroDocs;

public interface IHaveModifiers
{
    [Newtonsoft.Json.JsonProperty(Order = -2)]
    Modifier Modifiers { get; set; }
}
