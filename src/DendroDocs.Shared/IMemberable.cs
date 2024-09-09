namespace DendroDocs;

public interface IMemberable : IHaveModifiers
{
    [Newtonsoft.Json.JsonIgnore]
    MemberType MemberType { get; }

    [Newtonsoft.Json.JsonProperty(Order = -3)]
    [JsonPropertyOrder(-3)]
    string Name { get; }

    DocumentationCommentsDescription? DocumentationComments { get; internal set; }
}
