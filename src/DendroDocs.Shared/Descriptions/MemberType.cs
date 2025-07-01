namespace DendroDocs;

/// <summary>
/// Specifies the type of member in a .NET type.
/// </summary>
public enum MemberType
{
    /// <summary>
    /// Represents a field member.
    /// </summary>
    Field = 0,
    /// <summary>
    /// Represents a method member.
    /// </summary>
    Method = 1,
    /// <summary>
    /// Represents a property member.
    /// </summary>
    Property = 2,
    /// <summary>
    /// Represents a constructor member.
    /// </summary>
    Constructor = 3,
    /// <summary>
    /// Represents an enumeration member.
    /// </summary>
    EnumMember = 4,
    /// <summary>
    /// Represents an event member.
    /// </summary>
    Event = 5
}
