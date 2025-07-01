namespace DendroDocs;

/// <summary>
/// Represents an argument passed to a method or constructor with its type and text representation.
/// </summary>
[DebuggerDisplay("Argument {Text} ({Type,nq})")]
public class ArgumentDescription(string type, string text)
{
    /// <summary>
    /// Gets the type of the argument.
    /// </summary>
    public string Type { get; } = type ?? throw new ArgumentNullException(nameof(type));

    /// <summary>
    /// Gets the text representation of the argument value.
    /// </summary>
    public string Text { get; } = text ?? throw new ArgumentNullException(nameof(text));
}
