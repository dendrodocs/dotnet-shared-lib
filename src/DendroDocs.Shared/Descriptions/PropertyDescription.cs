namespace DendroDocs;

/// <summary>
/// Represents a property with its type, name, and optional initializer.
/// </summary>
[DebuggerDisplay("Property {Type,nq} {Name,nq}")]
public class PropertyDescription(string type, string name) : MemberDescription(name)
{
    /// <summary>
    /// Gets the type of the property.
    /// </summary>
    public string Type { get; } = type ?? throw new ArgumentNullException(nameof(type));

    /// <summary>
    /// Gets or sets the initializer expression for the property, if any.
    /// </summary>
    public string? Initializer { get; set; }
        
    /// <summary>
    /// Gets a value indicating whether the property has an initializer expression.
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    [JsonIgnore]
    public bool HasInitializer => !string.IsNullOrWhiteSpace(this.Initializer);

    /// <summary>
    /// Gets the member type, which is always <see cref="MemberType.Property"/> for property descriptions.
    /// </summary>
    [JsonIgnore]
    public override MemberType MemberType => MemberType.Property;
}
