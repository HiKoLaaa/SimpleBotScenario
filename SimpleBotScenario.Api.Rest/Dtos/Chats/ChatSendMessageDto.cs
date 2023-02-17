namespace SimpleBotScenario.Api.Rest.Dtos.Chats;

using System.Text.Json.Serialization;

public class ChatSendMessageDto
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = null!;
}
