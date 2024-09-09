using DendroDocs.Json;

namespace DendroDocs;

public abstract class MemberDescription(string name) : IMemberable
{
    public abstract MemberType MemberType { get; }

    public string Name { get; } = name ?? throw new ArgumentNullException("name");

    [DefaultValue(Modifier.Private)]
    [Newtonsoft.Json.JsonProperty(DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.IgnoreAndPopulate)]
    public Modifier Modifiers { get; set; }

    [Newtonsoft.Json.JsonIgnore]
    public bool IsInherited { get; internal set; } = false;

    [Newtonsoft.Json.JsonConverter(typeof(ConcreteTypeConverter<DocumentationCommentsDescription>))]
    public DocumentationCommentsDescription? DocumentationComments { get; set; }

    [Newtonsoft.Json.JsonProperty(ItemTypeNameHandling = Newtonsoft.Json.TypeNameHandling.None)]
    [Newtonsoft.Json.JsonConverter(typeof(ConcreteTypeConverter<List<AttributeDescription>>))]
    public List<AttributeDescription> Attributes { get; init; } = [];

    public override bool Equals(object? obj)
    {
        if (obj is not MemberDescription other)
        {
            return false;
        }

        return Equals(this.MemberType, other.MemberType) && string.Equals(this.Name, other.Name);
    }

    public override int GetHashCode()
    {
        return (this.MemberType, this.Name).GetHashCode();
    }
}
