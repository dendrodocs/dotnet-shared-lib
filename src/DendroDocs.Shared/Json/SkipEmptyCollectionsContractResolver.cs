using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Reflection;

namespace DendroDocs.Json;

/// <remarks>
/// Code based on example by Discord [http://stackoverflow.com/a/18486790]
/// </remarks>>
public class SkipEmptyCollectionsContractResolver : DefaultContractResolver
{
    protected override Newtonsoft.Json.Serialization.JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);

        var isDefaultValueIgnored = ((property.DefaultValueHandling ?? DefaultValueHandling.Ignore) & DefaultValueHandling.Ignore) != 0;
        if (!isDefaultValueIgnored || typeof(string).IsAssignableFrom(property.PropertyType) || !typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
        {
            // The property should not be ignored, or is of the type String or is not of the type IEnumerable
            // Just return the property
            return property;
        }

        // This check return true if the collection contains items
        bool newShouldSerialize(object obj)
        {
            return property.ValueProvider?.GetValue(obj) is not ICollection collection || collection.Count > 0;
        }

        var originalShouldSerialize = property.ShouldSerialize;

        // The property should serialize if the original check (if any) and the new check both incicate the value should be serialized
        property.ShouldSerialize = originalShouldSerialize != null ? o => originalShouldSerialize(o) && newShouldSerialize(o) : newShouldSerialize;

        return property;
    }
}
