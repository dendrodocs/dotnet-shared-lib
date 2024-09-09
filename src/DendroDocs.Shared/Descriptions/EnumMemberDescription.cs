namespace DendroDocs;

[DebuggerDisplay("EnumMember {Name,nq}")]
public class EnumMemberDescription(string name, string? value) : MemberDescription(name)
{
    public string? Value { get; } = value;

    [JsonIgnore]
    public override MemberType MemberType => MemberType.EnumMember;
}
