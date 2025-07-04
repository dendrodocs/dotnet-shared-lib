namespace DendroDocs;

/// <summary>
/// Represents a base class for all statement types that can appear in method bodies.
/// </summary>
[JsonDerivedType(typeof(ForEach), typeDiscriminator: "DendroDocs.ForEach, DendroDocs.Shared")]
[JsonDerivedType(typeof(If), typeDiscriminator: "DendroDocs.If, DendroDocs.Shared")]
[JsonDerivedType(typeof(IfElseSection), typeDiscriminator: "DendroDocs.IfElseSection, DendroDocs.Shared")]
[JsonDerivedType(typeof(Switch), typeDiscriminator: "DendroDocs.Switch, DendroDocs.Shared")]
[JsonDerivedType(typeof(SwitchSection), typeDiscriminator: "DendroDocs.SwitchSection, DendroDocs.Shared")]
[JsonDerivedType(typeof(InvocationDescription), typeDiscriminator: "DendroDocs.InvocationDescription, DendroDocs.Shared")]
[JsonDerivedType(typeof(ReturnDescription), typeDiscriminator: "DendroDocs.ReturnDescription, DendroDocs.Shared")]
[JsonDerivedType(typeof(AssignmentDescription), typeDiscriminator: "DendroDocs.AssignmentDescription, DendroDocs.Shared")]
public abstract class Statement : IJsonOnDeserialized
{
    /// <summary>
    /// Gets the collection of nested statements contained within this statement.
    /// </summary>
    [Newtonsoft.Json.JsonProperty(ItemTypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects)]
    public virtual List<Statement> Statements { get; } = [];

    /// <summary>
    /// Gets or sets the parent object that contains this statement.
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    [JsonIgnore]
    public object? Parent
    {
        get; internal set;
    }

    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context)
    {
        this.OnDeserialized();
    }

    /// <summary>
    /// Restores parent-child relationships after deserialization.
    /// </summary>
    public void OnDeserialized()
    {
        foreach (var statement in this.Statements)
        {
            statement.Parent ??= this;
        }
    }
}
