using System.Collections;
using System.Reflection;
using System.Text.Json.Serialization.Metadata;
using Newtonsoft.Json;

namespace DendroDocs.Json;

/// <summary>
/// Provides default JSON serialization settings and options optimized for DendroDocs data structures.
/// </summary>
public static class JsonDefaults
{
    /// <summary>
    /// Creates JSON serializer settings optimized for DendroDocs data structures using Newtonsoft.Json.
    /// </summary>
    /// <returns>Configured JSON serializer settings with appropriate defaults.</returns>
    public static JsonSerializerSettings SerializerSettings()
    {
        var serializerSettings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new SkipEmptyCollectionsContractResolver(),
            TypeNameHandling = TypeNameHandling.Auto
        };

        return serializerSettings;
    }

    /// <summary>
    /// Creates JSON serializer options optimized for DendroDocs data structures using System.Text.Json.
    /// </summary>
    /// <returns>Configured JSON serializer options with appropriate defaults.</returns>
    public static JsonSerializerOptions SerializerOptions()
    {
        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            PropertyNamingPolicy = null,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers =
                {
                    CustomDendroDocsSerialization
                }
            },
        };

        return options;
    }

    /// <summary>
    /// Creates JSON deserializer settings optimized for DendroDocs data structures using Newtonsoft.Json.
    /// </summary>
    /// <returns>Configured JSON deserializer settings with appropriate defaults.</returns>
    public static JsonSerializerSettings DeserializerSettings()
    {
        var serializerSettings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            ContractResolver = new SkipEmptyCollectionsContractResolver(),
            TypeNameHandling = TypeNameHandling.Auto
        };

        return serializerSettings;
    }

    /// <summary>
    /// Creates JSON deserializer options optimized for DendroDocs data structures using System.Text.Json.
    /// </summary>
    /// <returns>Configured JSON deserializer options with appropriate defaults.</returns>
    public static JsonSerializerOptions? DeserializerOptions()
    {
        var options = new JsonSerializerOptions
        {
            PreferredObjectCreationHandling = JsonObjectCreationHandling.Populate,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers =
                {
                    CustomDendroDocsDeserialization
                }
            },
        };

        return options;
    }

    private static void CustomDendroDocsSerialization(JsonTypeInfo typeInfo)
    {
        foreach (var property in typeInfo.Properties)
        {
            SetShouldSerializeBasedOnCollection(property);
            SetShouldSerializeBasedOnDefaultValue(typeInfo, property);
        }

        static void SetShouldSerializeBasedOnCollection(JsonPropertyInfo property)
        {
            if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(IReadOnlyList<>))
            {
                // Only serialize read-only collections if they have items
                property.ShouldSerialize = (_, value) => value is IReadOnlyCollection<object> collection && collection.Count > 0;
            }
            else if (typeof(ICollection).IsAssignableFrom(property.PropertyType))
            {
                // Only serialize collections if they have items
                property.ShouldSerialize = (_, value) => value is ICollection collection && collection.Count > 0;
            }
        }

        static void SetShouldSerializeBasedOnDefaultValue(JsonTypeInfo typeInfo, JsonPropertyInfo property)
        {
            // Serialize only if the value differs from the default value
            var propertyInfo = typeInfo.Type.GetProperty(property.Name)?.GetCustomAttribute<DefaultValueAttribute>();
            if (propertyInfo is not null)
            {
                property.ShouldSerialize = (_, value) => !Equals(propertyInfo.Value, value);
            }
        }
    }

    private static void CustomDendroDocsDeserialization(JsonTypeInfo typeInfo)
    {
        // Process only if typeInfo represents an object
        if (typeInfo.Kind != JsonTypeInfoKind.Object) return;

        // Preserve original OnDeserializing method
        var originalOnDeserializing = typeInfo.OnDeserializing;
        typeInfo.OnDeserializing = (obj) =>
        {
            originalOnDeserializing?.Invoke(obj);

            // Set default values for properties with DefaultValueAttribute
            SetDefaultValues(obj);
        };

        void SetDefaultValues(object obj)
        {
            foreach (var property in typeInfo.Properties)
            {
                var reflectedProperty = typeInfo.Type.GetProperty(property.Name)!;
                var defaultValueAttribute = reflectedProperty.GetCustomAttribute<DefaultValueAttribute>();
                if (defaultValueAttribute is not null)
                {
                    if (!reflectedProperty.CanWrite)
                    {
                        continue;
                    }
                    
                    // Check if not already set, e.g. by a property initializer
                    if (!Equals(reflectedProperty.GetValue(obj), defaultValueAttribute.Value))
                    {
                        reflectedProperty.SetValue(obj, defaultValueAttribute.Value);
                    }
                }
            }
        }
    }
}
