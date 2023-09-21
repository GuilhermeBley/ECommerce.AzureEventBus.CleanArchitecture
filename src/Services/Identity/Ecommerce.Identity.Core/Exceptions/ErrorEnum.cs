namespace Ecommerce.Identity.Core.Exceptions;

public enum ErrorEnum
{
    Unauthorized = 401,
    Forbbiden = 403,
    InvalidCompanyName = 400,
    InvalidCompanyUpdateAt = 400,
    InvalidCompanyCreatedAt = 400,
    InvalidClaimType = 400,
    InvalidClaimValue = 400,
    InvalidRoleName = 400,
}
