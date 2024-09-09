namespace DendroDocs;

public interface IHaveAMethodBody : IHaveModifiers
{
    DocumentationCommentsDescription? DocumentationComments { get; set; }

    string Name { get; }

    List<ParameterDescription> Parameters { get; }

    [Newtonsoft.Json.JsonProperty(ItemTypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects)]
    List<Statement> Statements { get; }

    List<AttributeDescription> Attributes { get; }
}
