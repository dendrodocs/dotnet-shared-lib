namespace DendroDocs;

public abstract class Statement
{
    [Newtonsoft.Json.JsonProperty(ItemTypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects)]
    public virtual List<Statement> Statements { get; } = [];

    [Newtonsoft.Json.JsonIgnore]
    public object? Parent
    {
        get; internal set;
    }

    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context)
    {
        foreach (var statement in this.Statements)
        {
            statement.Parent ??= this;
        }
    }
}
