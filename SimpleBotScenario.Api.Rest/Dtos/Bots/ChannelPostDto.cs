namespace SimpleBotScenario.Api.Rest.Dtos.Bots;

using System.Text.Json.Serialization;
using Converters;

public class ChannelPostDto
{
    [JsonPropertyName("message_id")]
    public int Id { get; set; }

    [JsonPropertyName("chat")]
    public ChatDto Chat { get; set; } = null!;

    [JsonConverter(typeof(UnixEpochDateTimeConverter))]
    [JsonPropertyName("date")]
    public DateTime DateTimeUtc { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }
}
