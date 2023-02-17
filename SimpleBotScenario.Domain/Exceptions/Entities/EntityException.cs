namespace SimpleBotScenario.Domain.Exceptions.Entities;

public class EntityException : Exception
{
    public string EntityName { get; }

    public EntityException(string entityName, string message) : base(message)
    {
        EntityName = entityName;
    }
}
