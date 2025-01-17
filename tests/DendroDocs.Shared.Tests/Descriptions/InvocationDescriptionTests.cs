namespace DendroDocs.Descriptions.Tests;

[TestClass]
public class InvocationDescriptionTests
{
    [TestMethod]
    public void ConstructorShouldSetTypesCorrectly()
    {
        // Act
        var description = new InvocationDescription("Type", "Name");

        // Assert
        description.ContainingType.ShouldBe("Type");
        description.Name.ShouldBe("Name");
        description.Arguments.ShouldBeEmpty();
    }

    [DataRow(null, "Name", "containingType", DisplayName = "Constuctor should throw when `containingType` is `null`")]
    [DataRow("Type", null, "name", DisplayName = "Constuctor should throw when `name` is `null`")]
    [TestMethod]
    public void ConstructorShouldGuardAgainstNullParamters(string containingType, string name, string parameterName)
    {
        // Act
        Action act = () => new InvocationDescription(containingType, name);

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(parameterName);
    }
}
