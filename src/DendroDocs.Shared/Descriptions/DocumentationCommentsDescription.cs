using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using DendroDocs.DocumentationComments;
using Attribute = DendroDocs.DocumentationComments.Attribute;

namespace DendroDocs;

public partial class DocumentationCommentsDescription
{
    [GeneratedRegex("(\\s{2,})", RegexOptions.ECMAScript)]
    private static partial Regex InlineWhitespace();

    [GeneratedRegex("^[NTFPME\\!]:", RegexOptions.ECMAScript)]
    private static partial Regex MemberIdPrefix();
    
    [GeneratedRegex("\\n\\s|\\s\\n", RegexOptions.Multiline)]
    private static partial Regex TrimmedNewlineSpace();

    [DefaultValue("")]
    public string Example { get; set; } = string.Empty;

    [DefaultValue("")]
    public string Remarks { get; set; } = string.Empty;

    [DefaultValue("")]
    public string Returns { get; set; } = string.Empty;

    [DefaultValue("")]
    public string Summary { get; set; } = string.Empty;

    [DefaultValue("")]
    public string Value { get; set; } = string.Empty;

    public Dictionary<string, string> Exceptions { get; set; } = [];

    public Dictionary<string, string> Permissions { get; set; } = [];

    public Dictionary<string, string> Params { get; set; } = [];

    public Dictionary<string, string> SeeAlsos { get; set; } = [];

    public Dictionary<string, string> TypeParams { get; set; } = [];

    public static DocumentationCommentsDescription? Parse(string? documentationCommentXml)
    {
        if (string.IsNullOrWhiteSpace(documentationCommentXml) || documentationCommentXml!.StartsWith("<!--", StringComparison.Ordinal))
        {
            // No documenation or unparseable documentation
            return null;
        }

        var element = XElement.Parse(documentationCommentXml);

        var documentation = new DocumentationCommentsDescription();

        documentation.ParseSections(element);

        return documentation;
    }

    private void ParseSections(XElement element)
    {
        this.Example = this.ParseSection(element.Element(Section.Example));
        this.Remarks = this.ParseSection(element.Element(Section.Remarks));
        this.Returns = this.ParseSection(element.Element(Section.Returns));
        this.Summary = this.ParseSection(element.Element(Section.Summary), true);
        this.Value = this.ParseSection(element.Element(Section.Value));

        this.ParseMultipleSections(element.Elements(Section.Exception), this.Exceptions, Attribute.CRef);
        this.ParseMultipleSections(element.Elements(Section.Param), this.Params, Attribute.Name);
        this.ParseMultipleSections(element.Elements(Section.Permission), this.Permissions, Attribute.CRef);
        this.ParseMultipleSections(element.Elements(Section.TypeParam), this.TypeParams, Attribute.Name);
        this.ParseSeeAlsos(element.Elements(Section.SeeAlso));
    }

    private void ParseMultipleSections(IEnumerable<XElement> sections, Dictionary<string, string> dictionary, string attributeName)
    {
        foreach (var section in sections)
        {
            var key = section.Attribute(attributeName)?.Value;
            if (!string.IsNullOrWhiteSpace(key))
            {
                dictionary[StripIDPrefix(key)] = this.ParseSection(section);
            }
        }
    }

    private void ParseSeeAlsos(IEnumerable<XElement> sections)
    {
        foreach (var section in sections)
        {
            this.ProcessSeeAlsoTag(section, false);
        }
    }

    private string ParseSection(XElement? section, bool removeNewLines = false)
    {
        if (section == null || section.IsEmpty)
        {
            return string.Empty;
        }

        var contents = new StringBuilder();

        foreach (var node in section.Nodes())
        {
            this.ProcessNode(node, contents, removeNewLines);
        }

        return TrimmedNewlineSpace()
            .Replace(contents.ToString(), "\n")
            .Trim();
    }

    private void ProcessNode(XNode node, StringBuilder contents, bool removeNewLines)
    {
        switch (node)
        {
            case XText text:
                ProcessInlineContent(contents, text.Value, removeNewLines);
                break;
            case XElement element:
                this.ProcessElementNode(element, contents, removeNewLines);
                break;
            default:
                // Ignore other node types
                break;
        }
    }

    private void ProcessElementNode(XElement element, StringBuilder contents, bool removeNewLines)
    {
        switch (element.Name.LocalName)
        {
            case Block.Code or Block.Para:
                this.ProcessBlockContent(contents, element);
                break;
            case Block.List:
                this.ProcessListContent(contents, element);
                break;
            case Inline.C:
                ProcessInlineContent(contents, element.Value, removeNewLines);
                break;
            case Inline.ParamRef or Inline.TypeParamRef:
                ProcessInlineContent(contents, StripIDPrefix(element.Attribute(Attribute.Name)?.Value), removeNewLines);
                break;
            case Inline.See:
                ProcessInlineContent(contents, element.IsEmpty ? StripIDPrefix(element.Attribute(Attribute.CRef)?.Value) : element.Value, removeNewLines);
                break;
            case Section.SeeAlso:
                contents.Append(this.ProcessSeeAlsoTag(element, removeNewLines));
                break;
            default:
                if (!element.IsEmpty) ProcessInlineContent(contents, element.Value, removeNewLines);
                break;
        }
    }

    private void ProcessListContent(StringBuilder contents, XElement element)
    {
        contents.Append('\n');

        var listType = element.Attribute(Attribute.Type)?.Value ?? ListType.Definition;
        switch (listType)
        {
            case ListType.Bullet:
                this.ProcessBulletList(contents, element);
                break;
            case ListType.Number:
                this.ProcessNumberedList(contents, element);
                break;
            case ListType.Definition:
                this.ProcessDefinitionList(contents, element);
                break;
        }
    }

    private void ProcessBulletList(StringBuilder contents, XElement element)
    {
        foreach (var item in element.Elements(List.Item))
        {
            this.AppendListItem(contents, "* ", item);
        }
    }

    private void ProcessNumberedList(StringBuilder contents, XElement element)
    {
        var startIndex = int.TryParse(element.Attribute(Attribute.Start)?.Value, out var result) ? result : 1;

        foreach (var item in element.Elements(List.Item))
        {
            this.AppendListItem(contents, $"{startIndex++}. ", item);
        }
    }

    private void ProcessDefinitionList(StringBuilder contents, XElement element)
    {
        foreach (var item in element.Elements(List.Item))
        {
            var term = this.ParseSection(item.Element(List.Term));
            var description = this.ParseSection(item.Element(List.Description));

            contents.Append(term);
            contents.Append(" — ");
            contents.Append(description);
            contents.Append('\n');
        }
    }

    private void AppendListItem(StringBuilder contents, string prefix, XElement item)
    {
        var term = this.ParseSection(item.Element(List.Term));
        var description = this.ParseSection(item.Element(List.Description));

        contents.Append(prefix);

        if (!string.IsNullOrEmpty(term))
        {
            contents.Append(term);
            contents.Append(" - ");
        }

        if (!string.IsNullOrEmpty(description))
        {
            contents.Append(description);
        }

        if (string.IsNullOrEmpty(term) && string.IsNullOrEmpty(description))
        {
            contents.Append(item.Value.Trim());
        }

        contents.Append('\n');
    }

    private static void ProcessInlineContent(StringBuilder stringBuilder, string text, bool removeNewLines)
    {
        if (removeNewLines)
        {
            stringBuilder.Append(InlineWhitespace().Replace(text, " "));
        }
        else
        {
            stringBuilder.Append(string.Join("\n", text.Split('\n').Select(t => t.Trim())));
        }
    }

    private void ProcessBlockContent(StringBuilder stringBuilder, XElement element)
    {
        stringBuilder.Append('\n');
        stringBuilder.Append(this.ParseSection(element));
        stringBuilder.Append('\n');
    }

    private string ProcessSeeAlsoTag(XElement element, bool removeNewLines)
    {
        var key = StripIDPrefix(element.Attribute(Attribute.CRef)?.Value);

        var contents = new StringBuilder();

        ProcessInlineContent(contents, element.Value, removeNewLines);

        var value = contents.ToString().Trim();

        this.SeeAlsos[key] = !string.IsNullOrEmpty(value) ? value : key;

        return this.SeeAlsos[key];
    }

    private static string StripIDPrefix(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return MemberIdPrefix().Replace(value, string.Empty);
    }
}
