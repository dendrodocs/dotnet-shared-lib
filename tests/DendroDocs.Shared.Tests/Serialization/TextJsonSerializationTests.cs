using DendroDocs.Json;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace DendroDocs.Shared.Tests;

[TestClass]
public class TextJsonSerializationTests
{
    [TestMethod]
    public void NoTypes_Should_GiveEmptyArray()
    {
        // Assign
        List<TypeDescription> types = [];
        
        // Act
        var result = JsonSerializer.Serialize(types, JsonDefaults.SerializerOptions());

        // Assert
        result.Should().Be("[]");
    }

    [TestMethod]
    public void InternalClass_Should_GiveOnlyFullName()
    {
        // Assign
        List<TypeDescription> types = [
            new TypeDescription(TypeType.Class, "Test")
            {
                Modifiers = Modifier.Internal
            }
        ];

        // Act
        var result = JsonSerializer.Serialize(types, JsonDefaults.SerializerOptions());

        // Assert
        result.Should().Be(@"[{""FullName"":""Test""}]");
    }

    [TestMethod]
    public void PublicClass_Should_GiveOnlyNonDefaultModifier()
    {
        // Assign
        List<TypeDescription> types = [
            new TypeDescription(TypeType.Class, "Test")
            {
                Modifiers = Modifier.Public
            }
        ];

        // Act
        var result = JsonSerializer.Serialize(types, JsonDefaults.SerializerOptions());

        // Assert
        result.Should().Be(@"[{""FullName"":""Test"",""Modifiers"":2}]");
    }

    [TestMethod]
    public void PrivateVoidMethod_Should_GiveOnlyName()
    {
        // Assign
        List<TypeDescription> types = [
            new TypeDescription(TypeType.Class, "Test")
            {
                Modifiers = Modifier.Internal
            }
        ];

        MemberDescription method = new MethodDescription("void", "Method")
        {
            Modifiers = Modifier.Private
        };
        types[0].AddMember(method);

        // Act
        var result = JsonSerializer.Serialize(types, JsonDefaults.SerializerOptions());

        // Assert
        result.Should().Be(@"[{""FullName"":""Test"",""Methods"":[{""Name"":""Method""}]}]");
    }

    [TestMethod]
    public void PrivateNonVoidMethod_Should_GiveNameAndReturnType()
    {
        // Assign
        List<TypeDescription> types = [
            new TypeDescription(TypeType.Class, "Test")
            {
                Modifiers = Modifier.Internal
            }
        ];

        MethodDescription method = new MethodDescription("int", "Method")
        {
            Modifiers = Modifier.Private
        };
        method.Statements.Add(new ReturnDescription("return 0"));
        types[0].AddMember(method);

        // Act
        var result = JsonSerializer.Serialize(types, JsonDefaults.SerializerOptions());

        // Assert
        result.Should().Match(@"[{""FullName"":""Test"",""Methods"":[{""Name"":""Method"",""ReturnType"":""int"",*}]}]");
    }

    [TestMethod]
    public void Attributes_Should_GiveNameAndType()
    {
        // Assign
        List<TypeDescription> types = [
            new TypeDescription(TypeType.Class, "Test")
            {
                Modifiers = Modifier.Internal
            }
        ];

        types[0].Attributes.Add(new AttributeDescription("System.ObsoleteAttribute", "System.Obsolete"));

        // Act
        var result = JsonSerializer.Serialize(types, JsonDefaults.SerializerOptions());

        // Assert
        result.Should().Match(@"[{""FullName"":""Test"",""Attributes"":[{""Type"":""System.ObsoleteAttribute"",""Name"":""System.Obsolete""}]}]");
    }

    [TestMethod]
    public void AttributeArguments_Should_GiveName_TypeAndValue()
    {
        // Assign
        List<TypeDescription> types = [
           new TypeDescription(TypeType.Class, "Test")
           {
                Modifiers = Modifier.Internal
            }
       ];

        var attribute = new AttributeDescription("System.ObsoleteAttribute", "System.Obsolete");
        attribute.Arguments.Add(new AttributeArgumentDescription("message", "string", "Reason"));
        types[0].Attributes.Add(attribute);

        // Act
        var result = JsonSerializer.Serialize(types, JsonDefaults.SerializerOptions());

        // Assert
        result.Should().Match(@"[{""FullName"":""Test"",""Attributes"":[{""Type"":""System.ObsoleteAttribute"",""Name"":""System.Obsolete"",""Arguments"":[{""Name"":""message"",""Type"":""string"",""Value"":""Reason""}]}]}]");
    }
}
