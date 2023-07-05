using ECommerce.Catalog.Core.Primitive.Result;

namespace Ecommerce.Catalog.Core.Entities.Product;

public class Product : Entity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public double Value { get; private set; }
    public DateTime UpdateAt { get; private set; }
    public DateTime InsertAt { get; private set; }

    private Product(Guid id, string name, string description, double value, DateTime updateAt, DateTime insertAt)
    {
        Id = id;
        Name = name;
        Description = description;
        Value = value;
        UpdateAt = updateAt;
        InsertAt = insertAt;
    }

    public override bool Equals(object? obj)
    {
        return obj is Product product &&
               IdEntity.Equals(product.IdEntity) &&
               Id.Equals(product.Id) &&
               Name == product.Name &&
               Description == product.Description &&
               Value == product.Value &&
               UpdateAt == product.UpdateAt &&
               InsertAt == product.InsertAt;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(IdEntity, Id, Name, Description, Value, UpdateAt, InsertAt);
    }

    public static Result<Product> Create(Guid id, string name, string description, double value, DateTime updateAt, DateTime insertAt)
    {
        ResultBuilder<Product> resultBuilder = new();

        resultBuilder.AddIfIsInRange(name, ErrorEnum.InvalidName, minValue: 2, maxValue: 100);

        resultBuilder.AddIfIsInRange(description, ErrorEnum.InvalidDescription, minValue: 0, maxValue: 1000);

        resultBuilder.AddIf(value < 0, ErrorEnum.InvalidValue);

        if (resultBuilder.TryFailed(out Result<Product>? result))
            return result;

        return resultBuilder.Success(
            new Product(id, name, description, value, updateAt, insertAt)    
        );
    }
}
