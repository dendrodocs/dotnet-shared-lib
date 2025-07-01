namespace DendroDocs.Extensions;

/// <summary>
/// Provides extension methods for checking specific modifiers on objects that implement <see cref="IHaveModifiers"/>.
/// </summary>
public static class IHaveModifiersExtensions
{
    /// <summary>
    /// Determines whether the object has the static modifier.
    /// </summary>
    /// <param name="iHaveModifiers">The object to check.</param>
    /// <returns><c>true</c> if the object has the static modifier; otherwise, <c>false</c>.</returns>
    public static bool IsStatic(this IHaveModifiers iHaveModifiers)
    {
        return (iHaveModifiers.Modifiers & Modifier.Static) == Modifier.Static;
    }

    /// <summary>
    /// Determines whether the object has the public access modifier.
    /// </summary>
    /// <param name="iHaveModifiers">The object to check.</param>
    /// <returns><c>true</c> if the object has the public access modifier; otherwise, <c>false</c>.</returns>
    public static bool IsPublic(this IHaveModifiers iHaveModifiers)
    {
        return (iHaveModifiers.Modifiers & Modifier.Public) == Modifier.Public;
    }

    /// <summary>
    /// Determines whether the object has the internal access modifier.
    /// </summary>
    /// <param name="iHaveModifiers">The object to check.</param>
    /// <returns><c>true</c> if the object has the internal access modifier; otherwise, <c>false</c>.</returns>
    public static bool IsInternal(this IHaveModifiers iHaveModifiers)
    {
        return (iHaveModifiers.Modifiers & Modifier.Internal) == Modifier.Internal;
    }

    /// <summary>
    /// Determines whether the object has the protected access modifier.
    /// </summary>
    /// <param name="iHaveModifiers">The object to check.</param>
    /// <returns><c>true</c> if the object has the protected access modifier; otherwise, <c>false</c>.</returns>
    public static bool IsProtected(this IHaveModifiers iHaveModifiers)
    {
        return (iHaveModifiers.Modifiers & Modifier.Protected) == Modifier.Protected;
    }

    /// <summary>
    /// Determines whether the object has the abstract modifier.
    /// </summary>
    /// <param name="iHaveModifiers">The object to check.</param>
    /// <returns><c>true</c> if the object has the abstract modifier; otherwise, <c>false</c>.</returns>
    public static bool IsAbstract(this IHaveModifiers iHaveModifiers)
    {
        return (iHaveModifiers.Modifiers & Modifier.Abstract) == Modifier.Abstract;
    }

    /// <summary>
    /// Determines whether the object has the private access modifier.
    /// </summary>
    /// <param name="iHaveModifiers">The object to check.</param>
    /// <returns><c>true</c> if the object has the private access modifier; otherwise, <c>false</c>.</returns>
    public static bool IsPrivate(this IHaveModifiers iHaveModifiers)
    {
        return (iHaveModifiers.Modifiers & Modifier.Private) == Modifier.Private;
    }

    /// <summary>
    /// Determines whether the object has the async modifier.
    /// </summary>
    /// <param name="iHaveModifiers">The object to check.</param>
    /// <returns><c>true</c> if the object has the async modifier; otherwise, <c>false</c>.</returns>
    public static bool IsAsync(this IHaveModifiers iHaveModifiers)
    {
        return (iHaveModifiers.Modifiers & Modifier.Async) == Modifier.Async;
    }

    /// <summary>
    /// Determines whether the object has the override modifier.
    /// </summary>
    /// <param name="iHaveModifiers">The object to check.</param>
    /// <returns><c>true</c> if the object has the override modifier; otherwise, <c>false</c>.</returns>
    public static bool IsOverride(this IHaveModifiers iHaveModifiers)
    {
        return (iHaveModifiers.Modifiers & Modifier.Override) == Modifier.Override;
    }

    /// <summary>
    /// Determines whether the object has the readonly modifier.
    /// </summary>
    /// <param name="iHaveModifiers">The object to check.</param>
    /// <returns><c>true</c> if the object has the readonly modifier; otherwise, <c>false</c>.</returns>
    public static bool IsReadonly(this IHaveModifiers iHaveModifiers)
    {
        return (iHaveModifiers.Modifiers & Modifier.Readonly) == Modifier.Readonly;
    }

    /// <summary>
    /// Determines whether the object has the const modifier.
    /// </summary>
    /// <param name="iHaveModifiers">The object to check.</param>
    /// <returns><c>true</c> if the object has the const modifier; otherwise, <c>false</c>.</returns>
    public static bool IsConst(this IHaveModifiers iHaveModifiers)
    {
        return (iHaveModifiers.Modifiers & Modifier.Const) == Modifier.Const;
    }

    /// <summary>
    /// Determines whether the object has the partial modifier.
    /// </summary>
    /// <param name="iHaveModifiers">The object to check.</param>
    /// <returns><c>true</c> if the object has the partial modifier; otherwise, <c>false</c>.</returns>
    public static bool IsPartial(this IHaveModifiers iHaveModifiers)
    {
        return (iHaveModifiers.Modifiers & Modifier.Partial) == Modifier.Partial;
    }

    /// <summary>
    /// Determines whether the object has the extern modifier.
    /// </summary>
    /// <param name="iHaveModifiers">The object to check.</param>
    /// <returns><c>true</c> if the object has the extern modifier; otherwise, <c>false</c>.</returns>
    public static bool IsExtern(this IHaveModifiers iHaveModifiers)
    {
        return (iHaveModifiers.Modifiers & Modifier.Extern) == Modifier.Extern;
    }

    /// <summary>
    /// Determines whether the object has the new modifier.
    /// </summary>
    /// <param name="iHaveModifiers">The object to check.</param>
    /// <returns><c>true</c> if the object has the new modifier; otherwise, <c>false</c>.</returns>
    public static bool IsNew(this IHaveModifiers iHaveModifiers)
    {
        return (iHaveModifiers.Modifiers & Modifier.New) == Modifier.New;
    }

    /// <summary>
    /// Determines whether the object has the sealed modifier.
    /// </summary>
    /// <param name="iHaveModifiers">The object to check.</param>
    /// <returns><c>true</c> if the object has the sealed modifier; otherwise, <c>false</c>.</returns>
    public static bool IsSealed(this IHaveModifiers iHaveModifiers)
    {
        return (iHaveModifiers.Modifiers & Modifier.Sealed) == Modifier.Sealed;
    }

    /// <summary>
    /// Determines whether the object has the unsafe modifier.
    /// </summary>
    /// <param name="iHaveModifiers">The object to check.</param>
    /// <returns><c>true</c> if the object has the unsafe modifier; otherwise, <c>false</c>.</returns>
    public static bool IsUnsafe(this IHaveModifiers iHaveModifiers)
    {
        return (iHaveModifiers.Modifiers & Modifier.Unsafe) == Modifier.Unsafe;
    }

    /// <summary>
    /// Determines whether the object has the virtual modifier.
    /// </summary>
    /// <param name="iHaveModifiers">The object to check.</param>
    /// <returns><c>true</c> if the object has the virtual modifier; otherwise, <c>false</c>.</returns>
    public static bool IsVirtual(this IHaveModifiers iHaveModifiers)
    {
        return (iHaveModifiers.Modifiers & Modifier.Virtual) == Modifier.Virtual;
    }
}
