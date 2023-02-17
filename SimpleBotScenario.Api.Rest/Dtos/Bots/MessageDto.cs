namespace SimpleBotScenario.Api.Rest.Dtos.Bots;

using System.Text.Json.Serialization;
using Converters;

public class MessageDto
{
    [JsonPropertyName("message_id")]
    public int Id { get; set; }

    [JsonConverter(typeof(UnixEpochDateTimeConverter))]
    [JsonPropertyName("date")]
    public DateTime DateTimeUtc { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("chat")]
    public ChatDto Chat { get; set; } = null!;

    [JsonPropertyName("from")]
    public ChatParticipantDto ChatParticipant { get; set; } = null!;
}
