namespace SimpleBotScenario.Api.Rest.BotEndpoints;

using Application.Features.BotEventProcess.Commands;
using Application.Features.BotStart;
using Dtos.Bots;
using Mapster;

internal static class BotRequestMappingRegister
{
    public static void CreateMappings()
    {
        TypeAdapterConfig<int, BotStartCommand>.NewConfig().Map(d => d.BotId, s => s);
        TypeAdapterConfig<MessageDto, MessageEventProcess>
            .NewConfig()
            .Map(d => d.ExternalChatId, s => s.Chat.Id)
            .Map(d => d.ExternalChatParticipantId, s => s.ChatParticipant.Id);

        TypeAdapterConfig<ChannelPostDto, ChannelPostEventProcess>
            .NewConfig()
            .Map(d => d.ExternalChatId, s => s.Chat.Id);
    }
}
