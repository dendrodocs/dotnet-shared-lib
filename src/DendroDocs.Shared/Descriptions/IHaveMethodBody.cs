namespace DendroDocs;

public interface IHaveAMethodBody : IHaveModifiers
{
    DocumentationCommentsDescription? DocumentationComments { get; set; }

    string Name { get; }

    List<ParameterDescription> Parameters { get; }

    [JsonProperty(ItemTypeNameHandling = TypeNameHandling.Objects)]
    List<Statement> Statements { get; }

    List<AttributeDescription> Attributes { get; }
}
