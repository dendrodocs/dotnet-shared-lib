namespace DendroDocs.Descriptions.Tests;

[TestClass]
public class ConstructorDescriptionTests
{
    [TestMethod]
    public void ConstructorShouldSetTypesCorrectly()
    {
        // Act
        var description = new ConstructorDescription("Name");

        // Assert
        description.MemberType.ShouldBe(MemberType.Constructor);
        description.IsInherited.ShouldBeFalse();
        description.Name.ShouldBe("Name");
        description.Parameters.ShouldBeEmpty();
        description.Statements.ShouldBeEmpty();
    }

    [DataRow(null, "name", DisplayName = "Constuctor should throw when `name` is `null`")]
    [TestMethod]
    public void ConstructorShouldGuardAgainstNullParamters(string name, string parameterName)
    {
        // Act
        Action act = () => new ConstructorDescription(name);

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(parameterName);
    }
}
