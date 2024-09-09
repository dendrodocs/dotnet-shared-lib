namespace DendroDocs;

[Flags]
public enum Modifier
{
    Internal = 1 << 0,  //      1
    Public = 1 << 1,    //      2
    Private = 1 << 2,   //      4
    Protected = 1 << 3, //      8
    Static = 1 << 4,    //     16
    Abstract = 1 << 5,  //     32
    Override = 1 << 6,  //     64
    Readonly = 1 << 7,  //    128
    Async = 1 << 8,     //    256
    Const = 1 << 9,     //    512
    Sealed = 1 << 10,   //  1_024
    Virtual = 1 << 11,  //  2_048
    Extern = 1 << 12,   //  4_096
    New = 1 << 13,      //  8_192
    Unsafe = 1 << 14,   // 16_384
    Partial = 1 << 15,  // 32_768
}
