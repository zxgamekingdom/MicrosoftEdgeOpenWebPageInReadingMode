using System;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace MicrosoftEdge以阅读模式打开网页
{
    public static class ObjectExtensions
    {
        public static void WriteLine(this object o)
        {
            Console.WriteLine(o);
        }

        public static string JsonSerialize(
            this object o,
            JsonSerializerOptions jsonSerializerOptions = null)
        {
            return JsonSerializer.Serialize(o,
                jsonSerializerOptions
                ?? new JsonSerializerOptions()
                {
                    Encoder = JavaScriptEncoder
                        .UnsafeRelaxedJsonEscaping
                });
        }

        public static T JsonDeserialize<T>(
            this string o,
            JsonSerializerOptions jsonSerializerOptions = null)
        {
            return jsonSerializerOptions is null
                ? JsonSerializer.Deserialize<T>(o)
                : JsonSerializer.Deserialize<T>(o,
                    jsonSerializerOptions);
        }
    }
}