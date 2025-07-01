namespace DendroDocs;

/// <summary>
/// Represents an enumeration member with its name and optional value.
/// </summary>
[DebuggerDisplay("EnumMember {Name,nq}")]
public class EnumMemberDescription(string name, string? value) : MemberDescription(name)
{
    /// <summary>
    /// Gets the value of the enumeration member, if explicitly specified.
    /// </summary>
    public string? Value { get; } = value;

    /// <summary>
    /// Gets the member type, which is always <see cref="MemberType.EnumMember"/> for enum member descriptions.
    /// </summary>
    [JsonIgnore]
    public override MemberType MemberType => MemberType.EnumMember;
}
