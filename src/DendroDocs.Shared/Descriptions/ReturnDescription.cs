namespace DendroDocs;

/// <summary>
/// Represents a return statement with its expression.
/// </summary>
[DebuggerDisplay("Return {Expression}")]
public class ReturnDescription(string expression) : Statement
{
    /// <summary>
    /// Gets the expression returned by the return statement.
    /// </summary>
    public string Expression { get; } = expression;
}
