namespace SimpleBotScenario.Application.Features.BotCreate.Commands;

using Domain.Entities;
using Interfaces;
using JetBrains.Annotations;
using MediatR;

[UsedImplicitly]
internal class BotCreateCommandHandler : IRequestHandler<BotCreateCommand, Bot>
{
    private readonly IUnitOfWork _unitOfWork;

    public BotCreateCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Bot> Handle(BotCreateCommand request, CancellationToken cancellationToken)
    {
        var newBot = new Bot(request.Token);

        await _unitOfWork.BotRepository.CreateBotAsync(newBot);
        await _unitOfWork.SaveAsync();

        return newBot;
    }
}
