namespace DendroDocs.Tests;

[TestClass]
public class DocumentationCommentsDescriptionTests
{
    [TestMethod]
    public void DefaultDocumentationSummary_Should_NotBeNull()
    {
        // Assign
        var documentation = new DocumentationCommentsDescription();

        // Assert
        documentation.Summary.Should().NotBeNull();
    }

    [TestMethod]
    public void DefaultDocumentationReturns_Should_NotBeNull()
    {
        // Assign
        var documentation = new DocumentationCommentsDescription();

        // Assert
        documentation.Returns.Should().NotBeNull();
    }

    [TestMethod]
    public void DefaultDocumentationRemarks_Should_NotBeNull()
    {
        // Assign
        var documentation = new DocumentationCommentsDescription();

        // Assert
        documentation.Remarks.Should().NotBeNull();
    }

    [TestMethod]
    public void DefaultDocumentationValue_Should_NotBeNull()
    {
        // Assign
        var documentation = new DocumentationCommentsDescription();

        // Assert
        documentation.Value.Should().NotBeNull();
    }

    [TestMethod]
    public void DefaultDocumentationExample_Should_NotBeNull()
    {
        // Assign
        var documentation = new DocumentationCommentsDescription();

        // Assert
        documentation.Example.Should().NotBeNull();
    }

    [TestMethod]
    [DataRow(null, DisplayName = "Parse should return `null` when `input` is `null`")]
    [DataRow("", DisplayName = "Parse should return `null` when `input` is an empty string")]
    [DataRow("   ", DisplayName = "Parse should return `null` when `input` is a whitespace string")]
    [DataRow("<!-- This is a comment -->", DisplayName = "Parse should return `null` for an XML comment")]
    public void ParseShouldReturnNullForEmptyOrInvalidXml(string? invalidXml)
    {
        // Act
        var result = DocumentationCommentsDescription.Parse(invalidXml);

        // Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void ParseShouldPopulateFieldsForValidXml()
    {
        // Arrange
        var validXml =
            """
            <doc>
                <summary>Summary text</summary>
                <remarks>Remarks text</remarks>
                <example>Example text</example>
                <returns>Returns text</returns>
                <value>Value text</value>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be("Summary text");
        result.Remarks.Should().Be("Remarks text");
        result.Example.Should().Be("Example text");
        result.Returns.Should().Be("Returns text");
        result.Value.Should().Be("Value text");
    }

    [TestMethod]
    public void ParseShouldHandlePlainTextInSummary()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>This is plain text.</summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be("This is plain text.");
    }

    [TestMethod]
    public void ParseShouldHandleInlineCodeElementInSummary()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>This is <c>inline code</c> in the text.</summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be("This is inline code in the text.");
    }

    [TestMethod]
    public void ParseShouldHandleMultipleExceptions()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <exception cref="System.ArgumentNullException">ArgumentNullException description</exception>
                <exception cref="System.ArgumentException">ArgumentException description</exception>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Exceptions.Should().HaveCount(2);
        result.Exceptions.Should().Contain("System.ArgumentNullException", "ArgumentNullException description");
        result.Exceptions.Should().Contain("System.ArgumentException", "ArgumentException description");
    }

    [TestMethod]
    public void ParseShouldHandleParamsAndTypeParams()
    {
        // Arrange
        var validXml =
            """
            <doc>
                <param name="param1">Parameter 1 description</param>
                <typeparam name="T">Type parameter T description</typeparam>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Params.Should().Contain("param1", "Parameter 1 description");
        result.TypeParams.Should().Contain("T", "Type parameter T description");
    }

    [TestMethod]
    public void ParseShouldHandleSeeAlsoTagsCorrectly()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <seealso cref="System.Int32">Custom see also text</seealso>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.SeeAlsos.Should().Contain("System.Int32", "Custom see also text");
    }

    [TestMethod]
    public void ParseShouldHandleEmptySeeAlsoContent()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <seealso cref="System.String"></seealso>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.SeeAlsos.Should().Contain("System.String", "System.String");
    }

    [TestMethod]
    public void ParseShouldHandleMissingMandatoryAttribute()
    {
        // Arrange
        string validXml =
            $"""
            <doc>
                <exception>Exception description</exception>
                <param>Param description</param>
                <permission>Permission description</permission>
                <typeparam>TypeParam description</typeparam>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Exceptions.Should().BeEmpty();
        result.Params.Should().BeEmpty();
        result.Permissions.Should().BeEmpty();
        result.TypeParams.Should().BeEmpty();
    }

    [TestMethod]
    public void ParseShouldProcessBulletList()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    <list type="bullet">
                        <item>
                            <term>Item 1</term>
                            <description>Description 1</description>
                        </item>
                        <item>
                            <term>Item 2</term>
                            <description>Description 2</description>
                        </item>
                    </list>
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be(
            """
            * Item 1 - Description 1
            * Item 2 - Description 2
            """.UseUnixNewLine());
    }

    [TestMethod]
    public void ParseShouldProcessNumberedList()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    <list type="number">
                        <item>
                            <term>Step 1</term>
                            <description>Description 1</description>
                        </item>
                        <item>
                            <term>Step 2</term>
                            <description>Description 2</description>
                        </item>
                    </list>
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be(
            """
            1. Step 1 - Description 1
            2. Step 2 - Description 2
            """.UseUnixNewLine());
    }

    [TestMethod]
    public void ParseShouldHandleNumberedListWithStartGreaterThanOne()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    <list type="number" start="5">
                        <item>
                            <term>Step 1</term>
                            <description>Description 1</description>
                        </item>
                        <item>
                            <term>Step 2</term>
                            <description>Description 2</description>
                        </item>
                    </list>
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be(
            """
            5. Step 1 - Description 1
            6. Step 2 - Description 2
            """.UseUnixNewLine());
    }

    [TestMethod]
    public void ParseShouldProcessDefinitionList()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    <list type="definition">
                        <item>
                            <term>Term 1</term>
                            <description>Description 1</description>
                        </item>
                        <item>
                            <term>Term 2</term>
                            <description>Description 2</description>
                        </item>
                    </list>
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be("""
            Term 1 — Description 1
            Term 2 — Description 2
            """.UseUnixNewLine());
    }

    [TestMethod]
    public void ParseShouldHandleListWithoutListStyleAsDefinitionList()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    <list>
                        <item>
                            <term>Item 1</term>
                            <description>Description 1</description>
                        </item>
                    </list>
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be("Item 1 — Description 1");
    }

    [TestMethod]
    public void ParseShouldHandleListWithMissingTermAndDescription()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    <list type="bullet">
                        <item>Content without term or description</item>
                    </list>
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be("* Content without term or description");
    }

    [TestMethod]
    public void ParseShouldHandleListWithRegularContentBeforeAndAfter()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    This is regular content.
                    <list type="bullet">
                        <item>
                            <term>Item 1</term>
                            <description>Description 1</description>
                        </item>
                    </list>
                    This is content after the list.
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be(
            """
            This is regular content.
            * Item 1 - Description 1
            This is content after the list.
            """.UseUnixNewLine());
    }

    [TestMethod]
    public void ParseShouldHandleCrefWithXmlDocIDPrefix()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <seealso cref="M:System.String.Format">See System.String.Format</seealso>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.SeeAlsos.Should().Contain("System.String.Format", "See System.String.Format");
    }

    [TestMethod]
    public void ParseShouldHandleSummaryWithTwoParaBlocks()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    <para>This is the first paragraph.</para>
                    <para>This is the second paragraph.</para>
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be(
            """
            This is the first paragraph.
            This is the second paragraph.
            """.UseUnixNewLine());
    }

    [TestMethod]
    public void ParseShouldHandleSummaryWithCodeBlock()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    Here is an example:
                    <code>
                    var x = 10;
                    var y = 20;

                    var sum = x + y;
                    </code>
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be(
            """
            Here is an example:
            var x = 10;
            var y = 20;

            var sum = x + y;
            """.UseUnixNewLine());
    }

    [TestMethod]
    public void ParseShouldHandleSummaryWithParamref()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    This method takes a parameter called <paramref name="param1"/>.
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be("This method takes a parameter called param1.");
    }

    [TestMethod]
    public void ParseShouldHandleSummaryWithTypeparamref()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    This method uses the type parameter <typeparamref name="T"/>.
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be("This method uses the type parameter T.");
    }

    [TestMethod]
    public void ParseShouldHandleSummaryWithSeeTag()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    See <see cref="System.String"/> for more details.
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be("See System.String for more details.");
    }

    [TestMethod]
    public void ParseShouldHandleSummaryWithSeeTagThatHasContent()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    See <see cref="System.String">string documentation</see> for more details.
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be("See string documentation for more details.");
    }

    [TestMethod]
    public void ParseShouldHandleEmptyElementWithNoCrefAttribute()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    This text includes an empty see element: <see/>.
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be("This text includes an empty see element: .");
    }

    [TestMethod]
    public void ParseShouldHandleSummaryWithParamrefWithNoName()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    This method takes a parameter called <paramref />.
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be("This method takes a parameter called .");
    }

    [TestMethod]
    public void ParseShouldHandleSummaryWithTypeparamrefWithNoName()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    This method uses the type parameter <typeparamref />.
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be("This method uses the type parameter .");
    }

    [TestMethod]
    public void ParseShouldHandleSummaryWithSeeAlsoTag()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    For more information, see <seealso cref="System.String"/>.
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be("For more information, see System.String.");
    }

    [TestMethod]
    public void ParseShouldHandleSummaryWithSeeAlsoWithoutCref()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    For more information, see <seealso></seealso>.
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be("For more information, see .");
    }

    [TestMethod]
    public void ParseShouldHandleSummaryWithUnknownElementWithContent()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    This text includes an unknown element: <b>bold text</b>.
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be("This text includes an unknown element: bold text.");
    }

    [TestMethod]
    public void ParseShouldHandleSummaryWithUnknownSelfClosingElementWithoutContent()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    This text includes a self-closing element: <br/>.
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be("This text includes a self-closing element: .");
    }

    [TestMethod]
    public void ParseShouldHandleUnknownNodeTypes()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    Some text. <!-- This is a comment -->
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.Should().Be("Some text.");
    }
}
