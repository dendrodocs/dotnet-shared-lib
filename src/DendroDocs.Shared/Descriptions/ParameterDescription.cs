using DendroDocs.Json;

namespace DendroDocs;

/// <summary>
/// Represents a method or constructor parameter with its type, name, and attributes.
/// </summary>
[DebuggerDisplay("Parameter {Type} {Name}")]
public class ParameterDescription(string type, string name)
{
    /// <summary>
    /// Gets the type of the parameter.
    /// </summary>
    public string Type { get; } = type ?? throw new ArgumentNullException(nameof(type));

    /// <summary>
    /// Gets the name of the parameter.
    /// </summary>
    public string Name { get; } = name ?? throw new ArgumentNullException(nameof(name));

    /// <summary>
    /// Gets or sets a value indicating whether the parameter has a default value.
    /// </summary>
    public bool HasDefaultValue { get; set; }

    /// <summary>
    /// Gets the collection of attributes applied to this parameter.
    /// </summary>
    [Newtonsoft.Json.JsonProperty(ItemTypeNameHandling = Newtonsoft.Json.TypeNameHandling.None)]
    [Newtonsoft.Json.JsonConverter(typeof(ConcreteTypeConverter<List<AttributeDescription>>))]
    public List<AttributeDescription> Attributes { get; init; } = [];
}
