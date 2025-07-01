namespace DendroDocs.Extensions;

/// <summary>
/// Provides extension methods for string manipulation, particularly for working with fully qualified type names.
/// </summary>
public static class StringExtensions
{
    private const char Dot = '.';

    /// <summary>
    /// Extracts the class name from a fully qualified type name.
    /// </summary>
    /// <param name="fullName">The fully qualified type name.</param>
    /// <returns>The class name without namespace qualification, or an empty string if the input is <c>null</c>.</returns>
    public static string ClassName(this string fullName)
    {
        if (fullName is null)
        {
            return string.Empty;
        }

        return fullName[Math.Min(fullName.LastIndexOf(Dot) + 1, fullName.Length)..];
    }

    /// <summary>
    /// Extracts the namespace from a fully qualified type name.
    /// </summary>
    /// <param name="fullName">The fully qualified type name.</param>
    /// <returns>The namespace portion, or an empty string if the input is <c>null</c> or has no namespace.</returns>
    public static string Namespace(this string fullName)
    {
        if (fullName is null)
        {
            return string.Empty;
        }

        return fullName[..Math.Max(fullName.LastIndexOf(Dot), 0)].Trim(Dot);
    }

    /// <summary>
    /// Splits a fully qualified type name into its namespace components.
    /// </summary>
    /// <param name="fullName">The fully qualified type name.</param>
    /// <returns>A read-only list of namespace parts, or an empty list if the input is <c>null</c>.</returns>
    public static IReadOnlyList<string> NamespaceParts(this string fullName)
    {
        if (fullName is null)
        {
            return [];
        }

        return fullName.Split(Dot, StringSplitOptions.RemoveEmptyEntries);
    }
}
