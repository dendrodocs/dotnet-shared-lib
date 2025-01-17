namespace DendroDocs.Descriptions.Tests;

[TestClass]
public class ArgumentDescriptionTests
{
    [TestMethod]
    public void ConstructorShouldSetTypesCorrectly()
    {
        // Act
        var description = new ArgumentDescription("Type", "Text");

        // Assert
        description.Type.ShouldBe("Type");
        description.Text.ShouldBe("Text");
    }

    [DataRow(null, "Text", "type", DisplayName = "Constuctor should throw when `type` is `null`")]
    [DataRow("Type", null, "text", DisplayName = "Constuctor should throw when `text` is `null`")]
    [TestMethod]
    public void ConstructorShouldGuardAgainstNullParamters(string type, string text, string parameterName)
    {
        // Act
        Action act = () => new ArgumentDescription(type, text);

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(parameterName);
    }
}
