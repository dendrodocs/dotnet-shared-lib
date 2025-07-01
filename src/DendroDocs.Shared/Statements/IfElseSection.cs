namespace DendroDocs;

/// <summary>
/// Represents a section of an if/else statement with its condition and statements.
/// </summary>
[DebuggerDisplay("IfElse {Condition}")]
public class IfElseSection : Statement
{
    /// <summary>
    /// Gets or sets the condition for this if/else section.
    /// </summary>
    public string? Condition { get; set; }
}
