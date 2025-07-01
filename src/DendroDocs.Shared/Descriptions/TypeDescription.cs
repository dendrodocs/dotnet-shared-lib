using DendroDocs.Extensions;
using DendroDocs.Json;

namespace DendroDocs;

/// <summary>
/// Represents a .NET type (class, interface, struct, enum, or delegate) with its members, modifiers, and metadata.
/// </summary>
[DebuggerDisplay("{Type} {Name,nq} ({Namespace,nq})")]
public class TypeDescription(TypeType type, string? fullName) : IHaveModifiers
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TypeDescription"/> class with full type information.
    /// </summary>
    /// <param name="type">The type of the .NET type (class, interface, struct, enum, or delegate).</param>
    /// <param name="fullName">The fully qualified name of the type.</param>
    /// <param name="fields">The collection of field descriptions.</param>
    /// <param name="constructors">The collection of constructor descriptions.</param>
    /// <param name="properties">The collection of property descriptions.</param>
    /// <param name="methods">The collection of method descriptions.</param>
    /// <param name="enumMembers">The collection of enum member descriptions.</param>
    /// <param name="events">The collection of event descriptions.</param>
    /// <param name="attributes">The collection of attribute descriptions.</param>
    /// <param name="baseTypes">The collection of base type names.</param>
    [Newtonsoft.Json.JsonConstructor]
    [JsonConstructor]
    public TypeDescription(
        TypeType type,  string? fullName, 
        IReadOnlyList<FieldDescription> fields,
        IReadOnlyList<ConstructorDescription> constructors, 
        IReadOnlyList<PropertyDescription> properties,
        IReadOnlyList<MethodDescription> methods,
        IReadOnlyList<EnumMemberDescription> enumMembers,
        IReadOnlyList<EventDescription> events,
        List<AttributeDescription> attributes,
        List<string> baseTypes
    )
        : this(type, fullName)
    {
        if (fields is not null) this.fields = [.. fields];
        if (constructors is not null) this.constructors = [.. constructors];
        if (properties is not null) this.properties = [.. properties];
        if (methods is not null) this.methods = [.. methods];
        if (enumMembers is not null) this.enumMembers = [.. enumMembers];
        if (events is not null) this.events = [.. events];
        if (attributes is not null) this.Attributes.AddRange(attributes);
        if (baseTypes is not null) this.BaseTypes.AddRange(baseTypes);
    }

    [Newtonsoft.Json.JsonProperty(Order = 1, PropertyName = nameof(Fields))]
    private readonly List<FieldDescription> fields = [];

    [Newtonsoft.Json.JsonProperty(Order = 2, PropertyName = nameof(Constructors))]
    private readonly List<ConstructorDescription> constructors = [];

    [Newtonsoft.Json.JsonProperty(Order = 3, PropertyName = nameof(Properties))]
    private readonly List<PropertyDescription> properties = [];

    [Newtonsoft.Json.JsonProperty(Order = 4, PropertyName = nameof(Methods))]
    private readonly List<MethodDescription> methods = [];

    [Newtonsoft.Json.JsonProperty(Order = 5, PropertyName = nameof(EnumMembers))]
    private readonly List<EnumMemberDescription> enumMembers = [];

    [Newtonsoft.Json.JsonProperty(Order = 6, PropertyName = nameof(Events))]
    private readonly List<EventDescription> events = [];

    /// <summary>
    /// Gets the type category of this type (class, interface, struct, enum, or delegate).
    /// </summary>
    public TypeType Type { get; } = type;

    /// <summary>
    /// Gets the fully qualified name of the type, including namespace.
    /// </summary>
    public string FullName { get; } = fullName ?? string.Empty;

    /// <summary>
    /// Gets or sets the parsed XML documentation comments for this type.
    /// </summary>
    public DocumentationCommentsDescription? DocumentationComments { get; set; }

    /// <summary>
    /// Gets the collection of base type names that this type inherits from or implements.
    /// </summary>
    public List<string> BaseTypes { get; } = [];

    /// <summary>
    /// Gets or sets the access modifiers and other modifiers applied to this type.
    /// </summary>
    [DefaultValue(Modifier.Internal)]
    [Newtonsoft.Json.JsonProperty(DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.IgnoreAndPopulate)]
    public Modifier Modifiers { get; set; }

    /// <summary>
    /// Gets the collection of attributes applied to this type.
    /// </summary>
    [Newtonsoft.Json.JsonProperty(ItemTypeNameHandling = Newtonsoft.Json.TypeNameHandling.None)]
    [Newtonsoft.Json.JsonConverter(typeof(ConcreteTypeConverter<List<AttributeDescription>>))]
    public List<AttributeDescription> Attributes { get; } = [];

    /// <summary>
    /// Gets the simple name of the type without namespace qualification.
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    [JsonIgnore]
    public string Name => this.FullName.ClassName();

    /// <summary>
    /// Gets the namespace containing this type.
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    [JsonIgnore]
    public string Namespace => this.FullName.Namespace();

    /// <summary>
    /// Gets the read-only collection of field descriptions for this type.
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    public IReadOnlyList<FieldDescription> Fields => this.fields;

    /// <summary>
    /// Gets the read-only collection of constructor descriptions for this type.
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    public IReadOnlyList<ConstructorDescription> Constructors => this.constructors;

    /// <summary>
    /// Gets the read-only collection of property descriptions for this type.
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    public IReadOnlyList<PropertyDescription> Properties => this.properties;

    /// <summary>
    /// Gets the read-only collection of method descriptions for this type.
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    public IReadOnlyList<MethodDescription> Methods => this.methods;

    /// <summary>
    /// Gets the read-only collection of enum member descriptions for this type (applicable only to enum types).
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    public IReadOnlyList<EnumMemberDescription> EnumMembers => this.enumMembers;

    /// <summary>
    /// Gets the read-only collection of event descriptions for this type.
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    public IReadOnlyList<EventDescription> Events => this.events;

    /// <summary>
    /// Adds a member description to the appropriate collection based on its type.
    /// </summary>
    /// <param name="member">The member description to add.</param>
    /// <exception cref="NotSupportedException">Thrown when the member type is not supported.</exception>
    public void AddMember(MemberDescription member)
    {
        switch (member)
        {
            case ConstructorDescription c:
                this.constructors.Add(c);
                break;

            case FieldDescription f:
                this.fields.Add(f);
                break;

            case PropertyDescription p:
                this.properties.Add(p);
                break;

            case MethodDescription m:
                this.methods.Add(m);
                break;

            case EnumMemberDescription em:
                this.enumMembers.Add(em);
                break;

            case EventDescription e:
                this.events.Add(e);
                break;

            default:
                throw new NotSupportedException($"Unable to add {member.GetType()} as member");
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is not TypeDescription other)
        {
            return false;
        }

        return string.Equals(this.FullName, other.FullName);
    }

    public override int GetHashCode() => this.FullName.GetHashCode();

    public IEnumerable<IHaveAMethodBody> MethodBodies() => this.Constructors.Cast<IHaveAMethodBody>().Concat(this.Methods);

    public bool ImplementsType(string fullName) => this.BaseTypes.Contains(fullName);

    public bool ImplementsTypeStartsWith(string partialName) => this.BaseTypes.Any(bt => bt.StartsWith(partialName, StringComparison.Ordinal));

    public bool IsClass() => this.Type == TypeType.Class;

    public bool IsEnum() => this.Type == TypeType.Enum;

    public bool IsInterface() => this.Type == TypeType.Interface;

    public bool IsStruct() => this.Type == TypeType.Struct;

    public bool HasProperty(string name) => this.properties.Any(m => string.Equals(m.Name, name, StringComparison.Ordinal));

    public bool HasMethod(string name) => this.methods.Any(m => string.Equals(m.Name, name, StringComparison.Ordinal));

    public bool HasEvent(string name) => this.events.Any(m => string.Equals(m.Name, name, StringComparison.Ordinal));

    public bool HasField(string name) => this.fields.Any(m => string.Equals(m.Name, name, StringComparison.Ordinal));

    public bool HasEnumMember(string name) => this.enumMembers.Any(m => string.Equals(m.Name, name, StringComparison.Ordinal));
}
