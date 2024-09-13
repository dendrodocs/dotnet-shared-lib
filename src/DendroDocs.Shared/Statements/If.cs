namespace DendroDocs;

[DebuggerDisplay("If")]
public class If : Statement
{
    public List<IfElseSection> Sections { get; } = [];

    [Newtonsoft.Json.JsonIgnore]
    public override List<Statement> Statements => this.Sections.SelectMany(s => s.Statements).ToList();

    [OnDeserialized]
    internal new void OnDeserializedMethod(StreamingContext context)
    {
        foreach (var section in this.Sections)
        {
            section.Parent ??= this;
        }
    }
}
