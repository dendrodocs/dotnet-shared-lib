using DendroDocs.Extensions;
using DendroDocs.Json;

namespace DendroDocs;

[DebuggerDisplay("{Type} {Name,nq} ({Namespace,nq})")]
public class TypeDescription(TypeType type, string? fullName) : IHaveModifiers
{
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
        List<AttributeDescription> attributes
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

    public TypeType Type { get; } = type;

    public string FullName { get; } = fullName ?? string.Empty;

    public DocumentationCommentsDescription? DocumentationComments { get; set; }

    public List<string> BaseTypes { get; } = [];

    [DefaultValue(Modifier.Internal)]
    [Newtonsoft.Json.JsonProperty(DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.IgnoreAndPopulate)]
    public Modifier Modifiers { get; set; }

    [Newtonsoft.Json.JsonProperty(ItemTypeNameHandling = Newtonsoft.Json.TypeNameHandling.None)]
    [Newtonsoft.Json.JsonConverter(typeof(ConcreteTypeConverter<List<AttributeDescription>>))]
    public List<AttributeDescription> Attributes { get; } = [];

    [Newtonsoft.Json.JsonIgnore]
    public string Name => this.FullName.ClassName();

    [Newtonsoft.Json.JsonIgnore]
    public string Namespace => this.FullName.Namespace();

    [Newtonsoft.Json.JsonIgnore]
    public IReadOnlyList<ConstructorDescription> Constructors => this.constructors;

    [Newtonsoft.Json.JsonIgnore]
    public IReadOnlyList<PropertyDescription> Properties => this.properties;

    [Newtonsoft.Json.JsonIgnore]
    public IReadOnlyList<MethodDescription> Methods => this.methods;

    [Newtonsoft.Json.JsonIgnore]
    public IReadOnlyList<EventDescription> Events => this.events;

    [Newtonsoft.Json.JsonIgnore]
    public IReadOnlyList<FieldDescription> Fields => this.fields;

    [Newtonsoft.Json.JsonIgnore]
    public IReadOnlyList<EnumMemberDescription> EnumMembers => this.enumMembers;

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
