namespace DendroDocs;

[DebuggerDisplay("If")]
public class If : Statement, IJsonOnDeserialized
{
    public List<IfElseSection> Sections { get; } = [];

    [Newtonsoft.Json.JsonIgnore]
    [JsonIgnore]
    public override List<Statement> Statements => this.Sections.SelectMany(s => s.Statements).ToList();

    [OnDeserialized]
    internal new void OnDeserializedMethod(StreamingContext context)
    {
         this.OnDeserialized();
    }

    public new void OnDeserialized()
    {
        foreach (var section in this.Sections)
        {
            section.Parent ??= this;
        }
    }
}
