namespace DendroDocs;

[DebuggerDisplay("Property {Type,nq} {Name,nq}")]
public class PropertyDescription(string type, string name) : MemberDescription(name)
{
    public string Type { get; } = type ?? throw new ArgumentNullException(nameof(type));

    public string? Initializer { get; set; }
        
    [Newtonsoft.Json.JsonIgnore]
    public bool HasInitializer => !string.IsNullOrWhiteSpace(this.Initializer);

    [JsonIgnore]
    public override MemberType MemberType => MemberType.Property;
}
