namespace DendroDocs;

/// <summary>
/// Specifies the category of .NET type.
/// </summary>
public enum TypeType
{
    /// <summary>
    /// Represents a class type.
    /// </summary>
    Class = 0,
    /// <summary>
    /// Represents an interface type.
    /// </summary>
    Interface = 1,
    /// <summary>
    /// Represents a struct (value type).
    /// </summary>
    Struct = 2,
    /// <summary>
    /// Represents an enumeration type.
    /// </summary>
    Enum = 3
}
