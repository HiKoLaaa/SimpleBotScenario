namespace SimpleBotScenario.Api.Rest.Dtos.Bots;

using System.Text.Json.Serialization;

public class ChatParticipantDto
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
}