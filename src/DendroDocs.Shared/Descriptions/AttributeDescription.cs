using DendroDocs.Json;

namespace DendroDocs;

/// <summary>
/// Represents an attribute applied to a type or member with its type, name, and arguments.
/// </summary>
[DebuggerDisplay("Attribute {Type} {Name}")]
public class AttributeDescription(string? type, string? name)
{
    /// <summary>
    /// Gets the type name of the attribute.
    /// </summary>
    public string Type { get; } = type ?? throw new ArgumentNullException(nameof(type));

    /// <summary>
    /// Gets the name of the attribute.
    /// </summary>
    public string Name { get; } = name ?? throw new ArgumentNullException(nameof(name));

    /// <summary>
    /// Gets the collection of arguments passed to the attribute.
    /// </summary>
    [Newtonsoft.Json.JsonProperty(ItemTypeNameHandling = Newtonsoft.Json.TypeNameHandling.None)]
    [Newtonsoft.Json.JsonConverter(typeof(ConcreteTypeConverter<List<AttributeArgumentDescription>>))]
    public List<AttributeArgumentDescription> Arguments { get; init; } = [];
}
