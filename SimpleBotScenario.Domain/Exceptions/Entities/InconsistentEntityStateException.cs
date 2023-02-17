namespace SimpleBotScenario.Domain.Exceptions.Entities;

public class InconsistentEntityStateException : EntityException
{
    public InconsistentEntityStateException(string entityName, string details) : base(entityName, $"Невалидное состояние сущности {entityName}: {details}.")
    {
    }
}
