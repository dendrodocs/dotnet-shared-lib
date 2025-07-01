namespace DendroDocs;

/// <summary>
/// Defines the contract for objects that have modifiers applied to them.
/// </summary>
public interface IHaveModifiers
{
    /// <summary>
    /// Gets or sets the modifiers applied to this object.
    /// </summary>
    [Newtonsoft.Json.JsonProperty(Order = -2)]
    [JsonPropertyOrder(-2)]
    Modifier Modifiers { get; set; }
}
