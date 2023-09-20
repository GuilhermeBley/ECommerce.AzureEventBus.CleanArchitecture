namespace Ecommerce.Identity.Core.Entities.Company;

public class Company : Entity
{
    public const string VALID_CHARS_NAME = StringUtils.CHAR_AZ_NUMBER_SPACE_ACCENT;
    public const int MAX_LENGTH_NAME = 100;
    public const int MIN_LENGTH_NAME = 3;

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateTime? UpdateAt { get; private set; }
    public DateTime CreateAt { get; private set; }

    private Company(Guid id, string name, DateTime? updateAt, DateTime createAt)
    {
        Id = id;
        Name = name;
        UpdateAt = updateAt;
        CreateAt = createAt;
    }

    public override bool Equals(object? obj)
    {
        return obj is Company company &&
               IdEntity.Equals(company.IdEntity) &&
               Id.Equals(company.Id) &&
               Name == company.Name &&
               UpdateAt == company.UpdateAt &&
               CreateAt == company.CreateAt;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(IdEntity, Id, Name, UpdateAt, CreateAt);
    }

    public static Result<Company> Create(Guid id, string name, DateTime updateAt, DateTime createAt)
    {
        ResultBuilder<Company> resultBuilder = new();

        resultBuilder.AddIf(
            name is null ||
            name.Length < MIN_LENGTH_NAME ||
            name.Length > MAX_LENGTH_NAME ||
            name.Any(c => !VALID_CHARS_NAME.Contains(c)),
            ErrorEnum.InvalidCompanyName
        );

        resultBuilder.AddIf(
            updateAt.Kind != DateTimeKind.Utc,
            ErrorEnum.InvalidCompanyUpdateAt
        );

        resultBuilder.AddIf(
            createAt.Kind != DateTimeKind.Utc,
            ErrorEnum.InvalidCompanyCreatedAt
        );

        if (resultBuilder.TryFailed(out Result<Company>? result))
            return result;

        return resultBuilder.Success(
            new Company(id, name!, updateAt, createAt)    
        );
    }
}
