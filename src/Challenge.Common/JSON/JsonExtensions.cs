using Newtonsoft.Json;

namespace Challenge.Common.JSON;

public static class JsonExtensions
{
    private static JsonSerializerSettings _settings = new JsonSerializerSettings
    {
        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
    };

    public static string ToJson(this object obj)
    {
        return JsonConvert.SerializeObject(obj, _settings);
    }

    public static T FromJson<T>(this string json)
    {
        return JsonConvert.DeserializeObject<T>(json, _settings) ?? throw new Exception($"Could not deserialize '{typeof(T).Name}' from '{json}'");
    }

    public static async Task<T> FromJson<T>(this Task<string> jsonTask)
    {
        return FromJson<T>(await jsonTask);
    }
}
