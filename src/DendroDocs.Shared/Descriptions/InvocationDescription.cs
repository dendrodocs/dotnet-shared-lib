using DendroDocs.Json;

namespace DendroDocs;

/// <summary>
/// Represents a method or constructor invocation with its containing type, name, and arguments.
/// </summary>
[DebuggerDisplay("Invocation \"{ContainingType,nq}.{Name,nq}\"")]
public class InvocationDescription(string containingType, string name) : Statement
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvocationDescription"/> class with arguments.
    /// </summary>
    /// <param name="containingType">The type that contains the invoked method or constructor.</param>
    /// <param name="name">The name of the invoked method or constructor.</param>
    /// <param name="arguments">The collection of arguments passed to the invocation.</param>
    [Newtonsoft.Json.JsonConstructor]
    [JsonConstructor]
    public InvocationDescription(string containingType, string name, List<ArgumentDescription>? arguments)
        : this(containingType, name)
    {
        if (arguments is not null) this.Arguments.AddRange(arguments);
    }

    /// <summary>
    /// Gets the type that contains the invoked method or constructor.
    /// </summary>
    public string ContainingType { get; } = containingType ?? throw new ArgumentNullException(nameof(containingType));

    /// <summary>
    /// Gets the name of the invoked method or constructor.
    /// </summary>
    public string Name { get; } = name ?? throw new ArgumentNullException(nameof(name));

    /// <summary>
    /// Gets the collection of arguments passed to the invocation.
    /// </summary>
    [Newtonsoft.Json.JsonProperty(ItemTypeNameHandling = Newtonsoft.Json.TypeNameHandling.None)]
    [Newtonsoft.Json.JsonConverter(typeof(ConcreteTypeConverter<List<ArgumentDescription>>))]
    public List<ArgumentDescription> Arguments { get; } = [];
}
