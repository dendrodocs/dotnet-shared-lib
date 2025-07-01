namespace DendroDocs;

/// <summary>
/// Represents a field with its type, name, and optional initializer.
/// </summary>
[DebuggerDisplay("Field {Type,nq} {Name,nq}")]
public class FieldDescription(string type, string name) : MemberDescription(name)
{
    /// <summary>
    /// Gets the type of the field.
    /// </summary>
    public string Type { get; } = type ?? throw new ArgumentNullException(nameof(type));

    /// <summary>
    /// Gets or sets the initializer expression for the field, if any.
    /// </summary>
    public string? Initializer { get; set; }

    /// <summary>
    /// Gets a value indicating whether the field has an initializer expression.
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    [JsonIgnore]
    public bool HasInitializer => this.Initializer is not null;

    /// <summary>
    /// Gets the member type, which is always <see cref="MemberType.Field"/> for field descriptions.
    /// </summary>
    [JsonIgnore]
    public override MemberType MemberType => MemberType.Field;
}
