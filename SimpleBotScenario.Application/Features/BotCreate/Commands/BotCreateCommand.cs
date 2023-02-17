namespace SimpleBotScenario.Application.Features.BotCreate.Commands;

using Domain.Entities;
using MediatR;

public class BotCreateCommand : IRequest<Bot>
{
    public string Token { get; set; } = null!;
}