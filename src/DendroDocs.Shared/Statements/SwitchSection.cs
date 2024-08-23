namespace DendroDocs;

[DebuggerDisplay("Switch Section {Labels}")]
public class SwitchSection : Statement
{
    public List<string> Labels { get; } = [];
}
