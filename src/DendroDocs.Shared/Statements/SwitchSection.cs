namespace DendroDocs;

/// <summary>
/// Represents a section of a switch statement with its case labels and statements.
/// </summary>
[DebuggerDisplay("Switch Section {Labels}")]
public class SwitchSection : Statement
{
    /// <summary>
    /// Gets the collection of case labels for this switch section.
    /// </summary>
    public List<string> Labels { get; } = [];
}
