namespace SimpleBotScenario.Persistence;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class SimpleBotScenarioDbContext : DbContext
{
    public SimpleBotScenarioDbContext(DbContextOptions<SimpleBotScenarioDbContext> options) : base(options)
    {
    }

    public DbSet<Bot> Bots => Set<Bot>();

    public DbSet<Chat> Chats => Set<Chat>();

    public DbSet<Message> Messages => Set<Message>();

    public DbSet<ChatParticipant> ChatParticipants => Set<ChatParticipant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SimpleBotScenarioDbContext).Assembly);
    }
}