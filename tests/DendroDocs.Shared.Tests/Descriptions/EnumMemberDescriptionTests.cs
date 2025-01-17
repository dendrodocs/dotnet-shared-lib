namespace DendroDocs.Descriptions.Tests;

[TestClass]
public class EnumMemberDescriptionTests
{
    [TestMethod]
    public void ConstructorShouldSetTypesCorrectly()
    {
        // Act
        var description = new EnumMemberDescription("Name", "Value");

        // Assert
        description.MemberType.ShouldBe(MemberType.EnumMember);
        description.IsInherited.ShouldBeFalse();
        description.Name.ShouldBe("Name");
        description.Value.ShouldBe("Value");
    }

    [DataRow(null, null, "name", DisplayName = "Constuctor should throw when `name` is `null`")]
    [TestMethod]
    public void ConstructorShouldGuardAgainstNullParamters(string name, string value, string parameterName)
    {
        // Act
        Action act = () => new EnumMemberDescription(name, value);

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(parameterName);
    }
}
