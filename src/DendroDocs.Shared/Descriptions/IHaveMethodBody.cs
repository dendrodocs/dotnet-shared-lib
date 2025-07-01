namespace DendroDocs;

/// <summary>
/// Defines the contract for objects that have a method body with parameters and statements.
/// </summary>
public interface IHaveAMethodBody : IHaveModifiers
{
    /// <summary>
    /// Gets or sets the parsed XML documentation comments for this method body.
    /// </summary>
    DocumentationCommentsDescription? DocumentationComments { get; set; }

    /// <summary>
    /// Gets the name of the method.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the collection of parameter descriptions for the method.
    /// </summary>
    List<ParameterDescription> Parameters { get; }

    /// <summary>
    /// Gets the collection of statements that make up the method body.
    /// </summary>
    [Newtonsoft.Json.JsonProperty(ItemTypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects)]
    List<Statement> Statements { get; }

    /// <summary>
    /// Gets the collection of attributes applied to this method.
    /// </summary>
    List<AttributeDescription> Attributes { get; }
}
