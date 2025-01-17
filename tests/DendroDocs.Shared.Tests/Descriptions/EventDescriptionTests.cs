namespace DendroDocs.Descriptions.Tests;

[TestClass]
public class EventDescriptionTests
{
    [TestMethod]
    public void ConstructorShouldSetTypesCorrectly()
    {
        // Act
        var description = new EventDescription("Type", "Name");

        // Assert
        description.MemberType.ShouldBe(MemberType.Event);
        description.IsInherited.ShouldBeFalse();
        description.Type.ShouldBe("Type");
        description.Name.ShouldBe("Name");
        description.HasInitializer.ShouldBeFalse();
    }

    [DataRow(null, "Name", "type", DisplayName = "Constuctor should throw when `type` is `null`")]
    [DataRow("Type", null, "name", DisplayName = "Constuctor should throw when `name` is `null`")]
    [TestMethod]
    public void ConstructorShouldGuardAgainstNullParamters(string type, string name, string parameterName)
    {
        // Act
        Action act = () => new EventDescription(type, name);

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(parameterName);
    }

    [TestMethod]
    public void InitializerShouldBeSetCorrectly()
    {
        // Act
        var description = new EventDescription("Type", "Name")
        {
            Initializer = "1"
        };

        // Assert
        description.Initializer.ShouldBe("1");
        description.HasInitializer.ShouldBeTrue();
    }
}
