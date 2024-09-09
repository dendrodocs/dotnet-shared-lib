using DendroDocs.Json;

namespace DendroDocs;

[DebuggerDisplay("Method {ReturnType,nq} {Name,nq}")]
public class MethodDescription(string? returnType, string name) : MemberDescription(name), IHaveAMethodBody
{
    [Newtonsoft.Json.JsonConstructor]
    [JsonConstructor]
    public MethodDescription(string? returnType, string name, List<Statement> statements)
        : this(returnType, name)
    {
        this.Statements = statements ?? [];
    }

    [DefaultValue("void")]
    public string ReturnType { get; } = returnType ?? "void";

    [Newtonsoft.Json.JsonProperty(ItemTypeNameHandling = Newtonsoft.Json.TypeNameHandling.None)]
    [Newtonsoft.Json.JsonConverter(typeof(ConcreteTypeConverter<List<ParameterDescription>>))]
    public List<ParameterDescription> Parameters { get; } = [];

    public List<Statement> Statements { get; } = [];

    public override MemberType MemberType => MemberType.Method;

    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context)
    {
        foreach (var statement in this.Statements)
        {
            statement.Parent = this;
        }
    }
}
