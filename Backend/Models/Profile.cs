using System.Text.Json.Serialization;

namespace Anonymous_Chat.Models
{
    public class Profile
    {
        [JsonPropertyName("pseudonym")]
        public string Pseudonym { get; set; } = string.Empty;

        [JsonPropertyName("gender")]
        public int Gender { get; set; }
    }
}