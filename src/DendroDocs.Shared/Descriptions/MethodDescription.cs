using DendroDocs.Json;

namespace DendroDocs;

/// <summary>
/// Represents a method with its return type, parameters, and method body statements.
/// </summary>
[DebuggerDisplay("Method {ReturnType,nq} {Name,nq}")]
public class MethodDescription(string? returnType, string name) : MemberDescription(name), IHaveAMethodBody, IJsonOnDeserialized
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MethodDescription"/> class with method statements.
    /// </summary>
    /// <param name="returnType">The return type of the method.</param>
    /// <param name="name">The name of the method.</param>
    /// <param name="statements">The collection of statements in the method body.</param>
    public MethodDescription(string? returnType, string name, List<Statement> statements)
        : this(returnType, name)
    {
        this.Statements.AddRange(statements ?? []);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MethodDescription"/> class with method parameters and statements.
    /// </summary>
    /// <param name="returnType">The return type of the method.</param>
    /// <param name="name">The name of the method.</param>
    /// <param name="parameters">The collection of parameter descriptions for the method.</param>
    /// <param name="statements">The collection of statements in the method body.</param>
    [Newtonsoft.Json.JsonConstructor]
    [JsonConstructor]
    public MethodDescription(string? returnType, string name, List<ParameterDescription>? parameters, List<Statement>? statements)
        : this(returnType, name)
    {
        if (parameters is not null) this.Parameters.AddRange(parameters);
        if (statements is not null) this.Statements.AddRange(statements);
    }

    /// <summary>
    /// Gets the return type of the method. Defaults to "void" if not specified.
    /// </summary>
    [JsonPropertyOrder(10)]
    [DefaultValue("void")]
    public string ReturnType { get; } = returnType ?? "void";

    /// <summary>
    /// Gets the collection of parameter descriptions for the method.
    /// </summary>
    [Newtonsoft.Json.JsonProperty(ItemTypeNameHandling = Newtonsoft.Json.TypeNameHandling.None)]
    [Newtonsoft.Json.JsonConverter(typeof(ConcreteTypeConverter<List<ParameterDescription>>))]
    public List<ParameterDescription> Parameters { get; } = [];

    /// <summary>
    /// Gets the collection of statements that make up the method body.
    /// </summary>
    [JsonPropertyOrder(10)]
    public List<Statement> Statements { get; } = [];

    /// <summary>
    /// Gets the member type, which is always <see cref="MemberType.Method"/> for method descriptions.
    /// </summary>
    [JsonIgnore]
    public override MemberType MemberType => MemberType.Method;

    /// <summary>
    /// Called after deserialization to restore parent-child relationships between statements.
    /// </summary>
    /// <param name="context">The streaming context for deserialization.</param>
    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context)
    {
        this.OnDeserialized();
    }

    /// <summary>
    /// Restores parent-child relationships between this method and its statements after deserialization.
    /// </summary>
    public void OnDeserialized()
    {
        foreach (var statement in this.Statements)
        {
            statement.Parent = this;
        }
    }
}
