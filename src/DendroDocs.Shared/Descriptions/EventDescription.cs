namespace DendroDocs;

[DebuggerDisplay("Event {Type,nq} {Name,nq}")]
public class EventDescription(string type, string name) : MemberDescription(name)
{
    public string Type { get; } = type ?? throw new ArgumentNullException(nameof(type));

    public string? Initializer { get; set; }

    [Newtonsoft.Json.JsonIgnore]
    [JsonIgnore]
    public bool HasInitializer => this.Initializer is not null;

    [JsonIgnore]
    public override MemberType MemberType => MemberType.Event;
}
