namespace DendroDocs;

/// <summary>
/// Represents access modifiers and other modifiers that can be applied to types and members.
/// </summary>
[Flags]
public enum Modifier
{
    /// <summary>
    /// Internal access modifier.
    /// </summary>
    Internal = 1 << 0,  //      1
    /// <summary>
    /// Public access modifier.
    /// </summary>
    Public = 1 << 1,    //      2
    /// <summary>
    /// Private access modifier.
    /// </summary>
    Private = 1 << 2,   //      4
    /// <summary>
    /// Protected access modifier.
    /// </summary>
    Protected = 1 << 3, //      8
    /// <summary>
    /// Static modifier.
    /// </summary>
    Static = 1 << 4,    //     16
    /// <summary>
    /// Abstract modifier.
    /// </summary>
    Abstract = 1 << 5,  //     32
    /// <summary>
    /// Override modifier.
    /// </summary>
    Override = 1 << 6,  //     64
    /// <summary>
    /// Readonly modifier.
    /// </summary>
    Readonly = 1 << 7,  //    128
    /// <summary>
    /// Async modifier.
    /// </summary>
    Async = 1 << 8,     //    256
    /// <summary>
    /// Const modifier.
    /// </summary>
    Const = 1 << 9,     //    512
    /// <summary>
    /// Sealed modifier.
    /// </summary>
    Sealed = 1 << 10,   //  1_024
    /// <summary>
    /// Virtual modifier.
    /// </summary>
    Virtual = 1 << 11,  //  2_048
    /// <summary>
    /// Extern modifier.
    /// </summary>
    Extern = 1 << 12,   //  4_096
    /// <summary>
    /// New modifier.
    /// </summary>
    New = 1 << 13,      //  8_192
    /// <summary>
    /// Unsafe modifier.
    /// </summary>
    Unsafe = 1 << 14,   // 16_384
    /// <summary>
    /// Partial modifier.
    /// </summary>
    Partial = 1 << 15,  // 32_768
}
