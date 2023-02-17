namespace SimpleBotScenario.Persistence;

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

[UsedImplicitly]
public class SimpleBotScenarioDbContextFactory : IDesignTimeDbContextFactory<SimpleBotScenarioDbContext>
{
    public SimpleBotScenarioDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SimpleBotScenarioDbContext>();
        optionsBuilder.UseNpgsql(args[0]);

        return new SimpleBotScenarioDbContext(optionsBuilder.Options);
    }
}