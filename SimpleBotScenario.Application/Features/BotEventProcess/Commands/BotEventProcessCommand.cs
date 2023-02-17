namespace SimpleBotScenario.Application.Features.BotEventProcess.Commands;

using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

public class BotEventProcessCommand : IRequest<Result<Unit>>
{
    public MessageEventProcess? Message { get; set; }

    public ChannelPostEventProcess? ChannelPost { get; set; }

    public string BotToken { get; set; } = null!;
}
