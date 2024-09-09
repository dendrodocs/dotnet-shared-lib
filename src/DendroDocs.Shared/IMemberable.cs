namespace DendroDocs;

public interface IMemberable : IHaveModifiers
{
    [JsonIgnore]
    MemberType MemberType { get; }

    [JsonProperty(Order = -3)]
    string Name { get; }

    DocumentationCommentsDescription? DocumentationComments { get; internal set; }
}
