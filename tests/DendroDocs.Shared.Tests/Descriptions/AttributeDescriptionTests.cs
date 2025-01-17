namespace DendroDocs.Descriptions.Tests;

[TestClass]
public class AttributeDescriptionTests
{
    [TestMethod]
    public void ConstructorShouldSetTypesCorrectly()
    {
        // Act
        var description = new AttributeDescription("Type", "Name");

        // Assert
        description.Type.ShouldBe("Type");
        description.Name.ShouldBe("Name");
        description.Arguments.ShouldBeEmpty();
    }

    [DataRow(null, "Text", "type", DisplayName = "Constuctor should throw when `type` is `null`")]
    [DataRow("Type", null, "name", DisplayName = "Constuctor should throw when `name` is `null`")]
    [TestMethod]
    public void ConstructorShouldGuardAgainstNullParamters(string type, string name, string parameterName)
    {
        // Act
        Action act = () => new AttributeDescription(type, name);

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(parameterName);
    }
}
