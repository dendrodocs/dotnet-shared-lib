namespace DendroDocs;

/// <summary>
/// Represents an if statement with multiple if/else sections.
/// </summary>
[DebuggerDisplay("If")]
public class If : Statement, IJsonOnDeserialized
{
    /// <summary>
    /// Gets the collection of if/else sections that make up this if statement.
    /// </summary>
    public List<IfElseSection> Sections { get; } = [];

    /// <summary>
    /// Gets all statements from all sections in this if statement.
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
