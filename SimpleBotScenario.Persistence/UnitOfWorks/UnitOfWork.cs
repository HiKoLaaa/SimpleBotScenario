namespace SimpleBotScenario.Persistence.UnitOfWorks;

using Application.Interfaces;

internal class UnitOfWork : IUnitOfWork
{
    private readonly SimpleBotScenarioDbContext _dbContext;

    public UnitOfWork(
        SimpleBotScenarioDbContext dbContext,
        IBotRepository botRepository,
        IChatRepository chatRepository,
        IChatParticipantRepository chatParticipantRepository)
    {
        _dbContext = dbContext;
        BotRepository = botRepository;
        ChatRepository = chatRepository;
        ChatParticipantRepository = chatParticipantRepository;
    }

    public IBotRepository BotRepository { get; }

    public IChatRepository ChatRepository { get; }

    public IChatParticipantRepository ChatParticipantRepository { get; }

    public Task SaveAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}
