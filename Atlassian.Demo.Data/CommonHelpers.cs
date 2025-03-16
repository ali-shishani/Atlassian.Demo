using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Atlassian.Demo.Data
{
    public static class JsonHelpers
    {
        private static JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public static string SerializeJson(object model)
        {
            return JsonSerializer.Serialize(model, _jsonSerializerOptions);
        }

        public static T DeserializeJson<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions)!;
        }
    }
}
