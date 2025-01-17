namespace DendroDocs.Descriptions.Tests;

[TestClass]
public class ParameterDescriptionTests
{
    [TestMethod]
    public void ConstructorShouldSetTypesCorrectly()
    {
        // Act
        var description = new ParameterDescription("Type", "Name");

        // Assert
        description.Type.ShouldBe("Type");
        description.Name.ShouldBe("Name");
        description.HasDefaultValue.ShouldBeFalse();
        description.Attributes.ShouldBeEmpty();
    }

    [DataRow(null, "Name", "type", DisplayName = "Constuctor should throw when `type` is `null`")]
    [DataRow("Type", null, "name", DisplayName = "Constuctor should throw when `name` is `null`")]
    [TestMethod]
    public void ConstructorShouldGuardAgainstNullParamters(string type, string name, string parameterName)
    {
        // Act
        Action act = () => new ParameterDescription(type, name);

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(parameterName);
    }
}
