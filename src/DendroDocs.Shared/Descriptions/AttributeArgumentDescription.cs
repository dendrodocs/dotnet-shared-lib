namespace DendroDocs;

/// <summary>
/// Represents an argument passed to an attribute with its name, type, and value.
/// </summary>
[DebuggerDisplay("AttributeArgument {Name} {Type} {Value}")]
public class AttributeArgumentDescription(string? name, string? type, string? value)
{
    /// <summary>
    /// Gets the name of the attribute argument.
    /// </summary>
    public string Name { get; } = name ?? throw new ArgumentNullException("name");

    /// <summary>
    /// Gets the type of the attribute argument.
    /// </summary>
    public string Type { get; } = type ?? throw new ArgumentNullException("type");

    /// <summary>
    /// Gets the value of the attribute argument.
    /// </summary>
    public string Value { get; } = value ?? throw new ArgumentNullException("value");
}
