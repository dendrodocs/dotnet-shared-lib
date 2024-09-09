using DendroDocs.Json;

namespace DendroDocs;

[DebuggerDisplay("Constructor {Name}")]
public class ConstructorDescription(string name) : MemberDescription(name), IHaveAMethodBody
{
    [Newtonsoft.Json.JsonConstructor]
    [JsonConstructor]
    public ConstructorDescription(string name, List<Statement> statements)
        : this(name)
    {
        this.Statements = statements ?? [];
    }

    [Newtonsoft.Json.JsonProperty(ItemTypeNameHandling = Newtonsoft.Json.TypeNameHandling.None)]
    [Newtonsoft.Json.JsonConverter(typeof(ConcreteTypeConverter<List<ParameterDescription>>))]
    public List<ParameterDescription> Parameters { get; } = [];

    public List<Statement> Statements { get; } = [];

    [JsonIgnore]
    public override MemberType MemberType => MemberType.Constructor;

    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context)
    {
        foreach (var statement in this.Statements)
        {
            statement.Parent = this;
        }
    }
}
