namespace SimpleBotScenario.Domain.Exceptions.Entities;

public class EntityNotFoundException : EntityException
{
    public EntityNotFoundException(string entityName) : base(entityName, "Сущность не найдена.")
    {
    }
}
