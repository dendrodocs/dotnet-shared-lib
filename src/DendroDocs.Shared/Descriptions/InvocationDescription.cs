namespace DendroDocs;

/// <summary>
/// Represents a method or constructor invocation with its containing type, name, and arguments.
/// </summary>
[DebuggerDisplay("Invocation \"{ContainingType,nq}.{Name,nq}\"")]
public class InvocationDescription(string containingType, string name) : Statement
{
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
    public List<ArgumentDescription> Arguments { get; } = [];
}
