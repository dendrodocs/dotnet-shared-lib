namespace DendroDocs;

/// <summary>
/// Represents an assignment statement with left-hand side, operator, and right-hand side expressions.
/// </summary>
[DebuggerDisplay("Assignment \"{Left,nq} {Operator,nq} {Right,nq}\"")]
public class AssignmentDescription(string left, string @operator, string right) : Statement
{
    /// <summary>
    /// Gets the left-hand side expression of the assignment.
    /// </summary>
    public string Left { get; } = left ?? throw new ArgumentNullException(nameof(left));

    /// <summary>
    /// Gets the assignment operator (e.g., "=", "+=", "-=").
    /// </summary>
    public string Operator { get; } = @operator ?? throw new ArgumentNullException(nameof(@operator));

    /// <summary>
    /// Gets the right-hand side expression of the assignment.
    /// </summary>
    public string Right { get; } = right ?? throw new ArgumentNullException(nameof(right));
}
