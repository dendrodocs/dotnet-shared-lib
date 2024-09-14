namespace DendroDocs;

[DebuggerDisplay("Switch {Expression}")]
public class Switch : Statement, IJsonOnDeserialized
{
    public List<SwitchSection> Sections { get; } = [];

    public string? Expression { get; set; }

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
