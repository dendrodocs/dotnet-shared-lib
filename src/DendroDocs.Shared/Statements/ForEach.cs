namespace DendroDocs;

/// <summary>
/// Represents a foreach loop statement.
/// </summary>
[DebuggerDisplay("ForEach")]
public class ForEach : Statement
{
    /// <summary>
    /// Gets or sets the expression that defines the foreach iteration.
    /// </summary>
    public string? Expression { get; set; }
}
