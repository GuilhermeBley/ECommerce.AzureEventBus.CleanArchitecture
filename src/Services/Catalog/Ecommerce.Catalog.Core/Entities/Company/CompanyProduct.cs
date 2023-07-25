using Ecommerce.Catalog.Core.Primitive.Result;

namespace Ecommerce.Catalog.Core.Entities.Company;

public class CompanyProduct : Entity
{
    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public Guid ProductId { get; private set; }
    public DateTime CreateAt { get; private set; }

    private CompanyProduct(Guid id, Guid companyId, Guid productId, DateTime createAt)
    {
        Id = id;
        CompanyId = companyId;
        ProductId = productId;
        CreateAt = createAt;
    }

    public override bool Equals(object? obj)
    {
        return obj is CompanyProduct product &&
               IdEntity.Equals(product.IdEntity) &&
               Id.Equals(product.Id) &&
               CompanyId.Equals(product.CompanyId) &&
               ProductId.Equals(product.ProductId) &&
               CreateAt == product.CreateAt;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(IdEntity, Id, CompanyId, ProductId, CreateAt);
    }

    public static Result<CompanyProduct> CreateDefault(Guid companyId, Guid productId)
        => Create(Guid.NewGuid(), companyId: companyId, productId: productId, createAt: DateTime.UtcNow);

    public static Result<CompanyProduct> Create(Guid id, Guid companyId, Guid productId, DateTime createAt)
    {
        ResultBuilder<CompanyProduct> builder = new();

        builder.AddIf(createAt.Kind == DateTimeKind.Utc, ErrorEnum.InvalidCreateAtCompanyProduct);

        if (builder.TryFailed(out Result<CompanyProduct>? resultFailed))
            return resultFailed;

        return builder.Success(
            new CompanyProduct(id, companyId, productId, createAt)    
        );
    }
}
