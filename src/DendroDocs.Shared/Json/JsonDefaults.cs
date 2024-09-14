using System.Collections;
using System.Reflection;
using System.Text.Json.Serialization.Metadata;
using Newtonsoft.Json;

namespace DendroDocs.Json;

public static class JsonDefaults
{
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
            // Only serialize read-only collections if they have items
            if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(IReadOnlyList<>))
            {
                property.ShouldSerialize = (_, value) => value is IReadOnlyCollection<object> collection && collection.Count > 0;
            }

            // Only serialize collections if they have items
            else if (typeof(ICollection).IsAssignableFrom(property.PropertyType))
            {
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

        // Set default values if necessary
        typeInfo.OnDeserialized = SetDefaultValues;

        void SetDefaultValues(object obj)
        {
            foreach (var property in typeInfo.Properties)
            {
                var reflectedProperty = typeInfo.Type.GetProperty(property.Name)!;
                var defaultValueAttribute = reflectedProperty.GetCustomAttribute<DefaultValueAttribute>();

                if (defaultValueAttribute is not null && Equals(reflectedProperty.GetValue(obj), GetDefault(property.PropertyType)))
                {
                    reflectedProperty.SetValue(obj, defaultValueAttribute.Value);
                }
            }
        }

        static IEnumerable<MethodInfo> OnDeserializedMethods(JsonTypeInfo typeInfo) =>
            typeInfo.Type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Where(m => m.GetCustomAttribute<OnDeserializedAttribute>() is not null &&
                            m.GetParameters().Length == 1 &&
                            m.GetParameters()[0].ParameterType == typeof(StreamingContext));
    }

    // Get the default value for a type at runtime
    private static object? GetDefault(Type type)
    {
        var genericDefault = GetDefault<object>;

        return genericDefault.Method.GetGenericMethodDefinition().MakeGenericMethod(type).Invoke(default, default);
    }

    private static T? GetDefault<T>() => default;
}
