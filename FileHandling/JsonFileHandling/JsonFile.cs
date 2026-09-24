using System.Text.Json;

namespace JsonFileHandling
{
    public class JsonFile
    {
        private readonly JsonSerializerOptions _options;

        public JsonFile()
        {
            _options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
        }

        public string Serialize<T>(T data)
        {
            return JsonSerializer.Serialize(data, _options);
        }

        public T Deserialize<T>(string jsonText) where T : new()
        {
            if (string.IsNullOrWhiteSpace(jsonText))
            {
                return new T();
            }
            return JsonSerializer.Deserialize<T>(jsonText, _options) ?? new T();
        }
    }
}
