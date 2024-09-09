using Newtonsoft.Json;

namespace DendroDocs.Json;

internal class ConcreteTypeConverter<TConcrete> : Newtonsoft.Json.JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return true;
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, Newtonsoft.Json.JsonSerializer serializer)
    {
        return serializer.Deserialize<TConcrete>(reader);
    }

    public override void WriteJson(JsonWriter writer, object? value, Newtonsoft.Json.JsonSerializer serializer)
    {
        serializer.TypeNameHandling = TypeNameHandling.None;

        serializer.Serialize(writer, value);
    }
}
