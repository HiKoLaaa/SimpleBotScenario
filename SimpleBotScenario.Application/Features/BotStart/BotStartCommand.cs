namespace SimpleBotScenario.Application.Features.BotStart;

using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

public class BotStartCommand : IRequest<Result<Unit>>
{
    public int BotId { get; set; }
}
