namespace DendroDocs;

/// <summary>
/// Defines the contract for objects that represent type members with modifiers and documentation.
/// </summary>
public interface IMemberable : IHaveModifiers
{
    /// <summary>
    /// Gets the type of member this object represents.
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    MemberType MemberType { get; }

    /// <summary>
    /// Gets the name of the member.
    /// </summary>
    [Newtonsoft.Json.JsonProperty(Order = -3)]
    [JsonPropertyOrder(-3)]
    string Name { get; }

    /// <summary>
    /// Gets or sets the parsed XML documentation comments for this member.
    /// </summary>
    DocumentationCommentsDescription? DocumentationComments { get; internal set; }
}
