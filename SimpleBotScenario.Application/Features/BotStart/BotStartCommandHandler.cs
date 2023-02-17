namespace SimpleBotScenario.Application.Features.BotStart;

using Domain.Entities;
using Domain.Exceptions;
using Domain.Exceptions.Entities;
using Interfaces;
using JetBrains.Annotations;
using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

[UsedImplicitly]
internal class BotStartCommandHandler : IRequestHandler<BotStartCommand, Result<Unit>>
{
    private readonly IBotService _botService;
    private readonly IUnitOfWork _unitOfWork;

    public BotStartCommandHandler(IUnitOfWork unitOfWork, IBotService botService)
    {
        _unitOfWork = unitOfWork;
        _botService = botService;
    }

    public async Task<Result<Unit>> Handle(BotStartCommand request, CancellationToken cancellationToken)
    {
        var bot = await _unitOfWork.BotRepository.GetBotAsync(request.BotId);
        if (bot is null)
            return new Result<Unit>(new EntityNotFoundException(nameof(Bot)));

        await _botService.StartAsync(bot.Token, cancellationToken);

        return new Result<Unit>(Unit.Default);
    }
}
