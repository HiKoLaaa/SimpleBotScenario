namespace SimpleBotScenario.Api.Rest.Dtos.Bots;

using System.Text.Json.Serialization;

public class BotEvent
{
    [JsonPropertyName("message")]
    public MessageDto? Message { get; set; }

    [JsonPropertyName("channel_post")]
    public ChannelPostDto? ChannelPost { get; set; }
}
