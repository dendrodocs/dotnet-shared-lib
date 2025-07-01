using DendroDocs.Json;

namespace DendroDocs;

/// <summary>
/// Represents a base class for all type members (methods, properties, fields, events, etc.) with common properties and functionality.
/// </summary>
public abstract class MemberDescription(string name) : IMemberable
{
    /// <summary>
    /// Gets the type of member this description represents.
    /// </summary>
    public abstract MemberType MemberType { get; }

    /// <summary>
    /// Gets the name of the member.
    /// </summary>
    public string Name { get; } = name ?? throw new ArgumentNullException("name");

    /// <summary>
    /// Gets or sets the access modifiers and other modifiers applied to this member.
    /// </summary>
    [DefaultValue(Modifier.Private)]
    [Newtonsoft.Json.JsonProperty(DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.IgnoreAndPopulate)]
    public Modifier Modifiers { get; set; }

    /// <summary>
    /// Gets a value indicating whether this member is inherited from a base type.
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    [JsonIgnore]
    public bool IsInherited { get; internal set; } = false;

    /// <summary>
    /// Gets or sets the parsed XML documentation comments for this member.
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(ConcreteTypeConverter<DocumentationCommentsDescription>))]
    public DocumentationCommentsDescription? DocumentationComments { get; set; }

    /// <summary>
    /// Gets the collection of attributes applied to this member.
    /// </summary>
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
