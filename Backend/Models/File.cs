using System.Text.Json.Serialization;

namespace Anonymous_Chat.Models
{
    public class File
    {
        [JsonPropertyName("preview")]
        public string Preview { get; set; } = string.Empty;
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
    }
}
