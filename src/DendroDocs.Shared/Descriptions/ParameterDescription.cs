using DendroDocs.Json;

namespace DendroDocs;

[DebuggerDisplay("Parameter {Type} {Name}")]
public class ParameterDescription(string type, string name)
{
    public string Type { get; } = type ?? throw new ArgumentNullException(nameof(type));

    public string Name { get; } = name ?? throw new ArgumentNullException(nameof(name));

    public bool HasDefaultValue { get; set; }

    [Newtonsoft.Json.JsonProperty(ItemTypeNameHandling = Newtonsoft.Json.TypeNameHandling.None)]
    [Newtonsoft.Json.JsonConverter(typeof(ConcreteTypeConverter<List<AttributeDescription>>))]
    public List<AttributeDescription> Attributes { get; init; } = [];
}
