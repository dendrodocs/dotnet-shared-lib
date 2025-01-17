namespace DendroDocs.Extensions;

public static class StringExtensions
{
    private const char Dot = '.';

    public static string ClassName(this string fullName)
    {
        if (fullName is null)
        {
            return string.Empty;
        }

        return fullName[Math.Min(fullName.LastIndexOf(Dot) + 1, fullName.Length)..];
    }

    public static string Namespace(this string fullName)
    {
        if (fullName is null)
        {
            return string.Empty;
        }

        return fullName[..Math.Max(fullName.LastIndexOf(Dot), 0)].Trim(Dot);
    }

    public static IReadOnlyList<string> NamespaceParts(this string fullName)
    {
        if (fullName is null)
        {
            return [];
        }

        return fullName.Split(Dot, StringSplitOptions.RemoveEmptyEntries);
    }
}
