namespace DendroDocs.Descriptions.Tests;

[TestClass]
public class MethodDescriptionTests
{
    [TestMethod]
    public void ConstructorShouldSetTypesCorrectly()
    {
        // Act
        var description = new MethodDescription("Type", "Name");

        // Assert
        description.MemberType.ShouldBe(MemberType.Method);
        description.IsInherited.ShouldBeFalse();
        description.ReturnType.ShouldBe("Type");
        description.Name.ShouldBe("Name");
        description.Parameters.ShouldBeEmpty();
        description.Statements.ShouldBeEmpty();
    }

    [TestMethod]
    public void ReturnTypeShouldBeVoidWhenNull()
    {
        // Act
        var description = new MethodDescription(null, "Name");

        // Assert
        description.ReturnType.ShouldBe("void");
    }
}
