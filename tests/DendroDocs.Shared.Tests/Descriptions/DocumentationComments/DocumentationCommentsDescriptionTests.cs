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
        documentation.Summary.ShouldNotBeNull();
    }

    [TestMethod]
    public void DefaultDocumentationReturns_Should_NotBeNull()
    {
        // Assign
        var documentation = new DocumentationCommentsDescription();

        // Assert
        documentation.Returns.ShouldNotBeNull();
    }

    [TestMethod]
    public void DefaultDocumentationRemarks_Should_NotBeNull()
    {
        // Assign
        var documentation = new DocumentationCommentsDescription();

        // Assert
        documentation.Remarks.ShouldNotBeNull();
    }

    [TestMethod]
    public void DefaultDocumentationValue_Should_NotBeNull()
    {
        // Assign
        var documentation = new DocumentationCommentsDescription();

        // Assert
        documentation.Value.ShouldNotBeNull();
    }

    [TestMethod]
    public void DefaultDocumentationExample_Should_NotBeNull()
    {
        // Assign
        var documentation = new DocumentationCommentsDescription();

        // Assert
        documentation.Example.ShouldNotBeNull();
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
        result.ShouldBeNull();
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
        result.Summary.ShouldBe("Summary text");
        result.Remarks.ShouldBe("Remarks text");
        result.Example.ShouldBe("Example text");
        result.Returns.ShouldBe("Returns text");
        result.Value.ShouldBe("Value text");
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
        result.Summary.ShouldBe("This is plain text.");
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
        result.Summary.ShouldBe("This is inline code in the text.");
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
        result.Exceptions.Count.ShouldBe(2);
        result.Exceptions.ShouldContainKeyAndValue("System.ArgumentNullException", "ArgumentNullException description");
        result.Exceptions.ShouldContainKeyAndValue("System.ArgumentException", "ArgumentException description");
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
        result.Params.ShouldContainKeyAndValue("param1", "Parameter 1 description");
        result.TypeParams.ShouldContainKeyAndValue("T", "Type parameter T description");
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
        result.SeeAlsos.ShouldContainKeyAndValue("System.Int32", "Custom see also text");
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
        result.SeeAlsos.ShouldContainKeyAndValue("System.String", "System.String");
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
        result.Exceptions.ShouldBeEmpty();
        result.Params.ShouldBeEmpty();
        result.Permissions.ShouldBeEmpty();
        result.TypeParams.ShouldBeEmpty();
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
        result.Summary.ShouldBe(
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
        result.Summary.ShouldBe(
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
        result.Summary.ShouldBe(
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
        result.Summary.ShouldBe("""
            Term 1 — Description 1
            Term 2 — Description 2
            """.UseUnixNewLine());
    }

    [TestMethod]
    public void ParseShouldHandleListWithoutListStyleButWithTermsAndDescriptionAsDefinitionList()
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
        result.Summary.ShouldBe("Item 1 — Description 1");
    }

    [TestMethod]
    public void ParseShouldHandleListWithoutListStyleNotItemOrDescriptionAsParagraphs()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                    <list>
                        <item>First item</item>
                        <item>Second item</item>
                    </list>
                </summary>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.ShouldBe(
            """
            First item
            Second item
            """.UseUnixNewLine());
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
        result.Summary.ShouldBe("* Content without term or description");
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
        result.Summary.ShouldBe(
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
        result.SeeAlsos.ShouldContainKeyAndValue("System.String.Format", "See System.String.Format");
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
        result.Summary.ShouldBe(
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
        result.Summary.ShouldBe(
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
        result.Summary.ShouldBe("This method takes a parameter called param1.");
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
        result.Summary.ShouldBe("This method uses the type parameter T.");
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
        result.Summary.ShouldBe("See System.String for more details.");
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
        result.Summary.ShouldBe("See string documentation for more details.");
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
        result.Summary.ShouldBe("This text includes an empty see element: .");
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
        result.Summary.ShouldBe("This method takes a parameter called .");
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
        result.Summary.ShouldBe("This method uses the type parameter .");
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
        result.Summary.ShouldBe("For more information, see System.String.");
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
        result.Summary.ShouldBe("For more information, see .");
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
        result.Summary.ShouldBe("This text includes an unknown element: bold text.");
    }

    [TestMethod]
    public void ParseShouldHandleSummaryWithUnknownSelfClosingElementWithoutContentAsWhitespace()
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
        result.Summary.ShouldBe("This text includes a self-closing element:.");
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
        result.Summary.ShouldBe("Some text.");
    }

    [TestMethod]
    public void ParseShouldKeepNewLinesInExamples()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <example>
                The following example demonstrates the use of this method.

                <code>
                // Get a new random number
                SampleClass sc = new SampleClass(10);

                int random = sc.GetRandomNumber();

                Console.WriteLine("Random value: {0}", random);
                </code>
                </example>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Example.ShouldBe(
            """
            The following example demonstrates the use of this method.

            // Get a new random number
            SampleClass sc = new SampleClass(10);

            int random = sc.GetRandomNumber();

            Console.WriteLine("Random value: {0}", random);
            """.UseUnixNewLine());
    }

    [TestMethod]
    public void ParseShouldKeepNewLinesButTrimSpacesInRemarks()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <remarks>
                  A
                  B
                </remarks>
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Remarks.ShouldBe(
            """
            A
            B
            """.UseUnixNewLine());
    }

    [TestMethod]
    public void ParseShouldHandleNestedInlineElementsCorrectly()
    {
        // Arrange
        string validXml =
            """
            <doc>
                <summary>
                This is a summary with mixed content.
                <para>A <see cref="System.Object">paragraph</see></para>
                <para>Another <paramref name="paragraph"/></para>
                <list type="definition">
                <item>
                <term>Term 1</term>
                <description>First <typeparamref name="item"/></description>
                </item>
                <item>
                <term>Term <c>2</c></term>
                <description><para>Second item</para></description>
                </item>
                </list>
                <code>
                class ACodeSample { }
                </code>
                More text <c>null</c> and more text
                <seealso cref="System.Object"/>
            </summary>
            
            </doc>
            """;

        // Act
        var result = DocumentationCommentsDescription.Parse(validXml)!;

        // Assert
        result.Summary.ShouldBe(
            """
            This is a summary with mixed content.
            A paragraph
            Another paragraph
            Term 1 — First item
            Term 2 — Second item
            class ACodeSample { }
            More text null and more text System.Object
            """.UseUnixNewLine());
    }
}
