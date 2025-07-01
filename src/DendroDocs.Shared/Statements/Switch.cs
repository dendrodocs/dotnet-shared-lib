namespace DendroDocs;

/// <summary>
/// Represents a switch statement with multiple switch sections.
/// </summary>
[DebuggerDisplay("Switch {Expression}")]
public class Switch : Statement, IJsonOnDeserialized
{
    /// <summary>
    /// Gets the collection of switch sections that make up this switch statement.
    /// </summary>
    public List<SwitchSection> Sections { get; } = [];

    /// <summary>
    /// Gets or sets the expression being switched on.
    /// </summary>
    public string? Expression { get; set; }

    /// <summary>
    /// Gets all statements from all sections in this switch statement.
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    [JsonIgnore]
    public override List<Statement> Statements => this.Sections.SelectMany(s => s.Statements).ToList();

    [OnDeserialized]
    internal new void OnDeserializedMethod(StreamingContext context)
    {
        this.OnDeserialized();
    }

    /// <summary>
    /// Restores parent-child relationships after deserialization.
    /// </summary>
    public new void OnDeserialized()
    {
        foreach (var section in this.Sections)
        {
            section.Parent ??= this;
        }
    }
}
