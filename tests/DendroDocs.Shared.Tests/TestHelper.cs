using System.Text.RegularExpressions;

namespace DendroDocs.Tests;

internal static class TestHelper
{
    public static string UseUnixNewLine(this string value) => value.UseSpecificNewLine("\n");
    public static string UseWindowsNewLine(this string value) => value.UseSpecificNewLine("\r\n");
    public static string UseEnvironmentNewLine(this string value) => value.UseSpecificNewLine(Environment.NewLine);
    public static string UseSpecificNewLine(this string value, string specificNewline) => Regex.Replace(value, @"(\r\n|\r|\n)", specificNewline);
}
