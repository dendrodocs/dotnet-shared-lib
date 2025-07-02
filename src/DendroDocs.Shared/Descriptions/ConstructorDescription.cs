using DendroDocs.Json;

namespace DendroDocs;

/// <summary>
/// Represents a constructor with its parameters and method body statements.
/// </summary>
[DebuggerDisplay("Constructor {Name}")]
public class ConstructorDescription(string name) : MemberDescription(name), IHaveAMethodBody, IJsonOnDeserialized
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorDescription"/> class with constructor statements.
    /// </summary>
    /// <param name="name">The name of the constructor.</param>
    /// <param name="statements">The collection of statements in the constructor body.</param>
    public ConstructorDescription(string name, List<Statement> statements)
        : this(name)
    {
        this.Statements.AddRange(statements ?? []);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorDescription"/> class with constructor parameters and statements.
    /// </summary>
    /// <param name="name">The name of the constructor.</param>
    /// <param name="parameters">The collection of parameter descriptions for the constructor.</param>
    /// <param name="statements">The collection of statements in the constructor body.</param>
    [Newtonsoft.Json.JsonConstructor]
    [JsonConstructor]
    public ConstructorDescription(string name, List<ParameterDescription>? parameters, List<Statement>? statements)
        : this(name)
    {
        if (parameters is not null) this.Parameters.AddRange(parameters);
        if (statements is not null) this.Statements.AddRange(statements);
    }

    /// <summary>
    /// Gets the collection of parameter descriptions for the constructor.
    /// </summary>
    [Newtonsoft.Json.JsonProperty(ItemTypeNameHandling = Newtonsoft.Json.TypeNameHandling.None)]
    [Newtonsoft.Json.JsonConverter(typeof(ConcreteTypeConverter<List<ParameterDescription>>))]
    public List<ParameterDescription> Parameters { get; } = [];

    /// <summary>
    /// Gets the collection of statements that make up the constructor body.
    /// </summary>
    public List<Statement> Statements { get; } = [];

    /// <summary>
    /// Gets the member type, which is always <see cref="MemberType.Constructor"/> for constructor descriptions.
    /// </summary>
    [JsonIgnore]
    public override MemberType MemberType => MemberType.Constructor;

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
    /// Restores parent-child relationships between this constructor and its statements after deserialization.
    /// </summary>
    public void OnDeserialized()
    {
        foreach (var statement in this.Statements)
        {
            statement.Parent = this;
        }
    }
}
