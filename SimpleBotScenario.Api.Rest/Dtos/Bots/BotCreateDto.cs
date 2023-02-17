namespace SimpleBotScenario.Api.Rest.Dtos.Bots;

using System.Text.Json.Serialization;

public class BotCreateDto
{
    [JsonPropertyName("token")]
    [JsonRequired]
    public string Token { get; set; } = null!;
}