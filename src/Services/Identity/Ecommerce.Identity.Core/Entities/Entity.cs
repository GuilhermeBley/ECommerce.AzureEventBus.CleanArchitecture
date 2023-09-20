namespace Ecommerce.Identity.Core.Entities;

public class Entity : IEntity
{
    public virtual Guid IdEntity { get; }
}

public interface IEntity
{
    Guid IdEntity { get; }
}
