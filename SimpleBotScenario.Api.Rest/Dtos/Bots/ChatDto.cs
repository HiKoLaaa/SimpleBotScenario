namespace SimpleBotScenario.Api.Rest.Dtos.Bots;

using System.Text.Json.Serialization;

public class ChatDto
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
}