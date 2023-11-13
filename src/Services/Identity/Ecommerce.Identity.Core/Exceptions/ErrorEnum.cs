﻿namespace Ecommerce.Identity.Core.Exceptions;

public enum ErrorEnum
{
    NoContent = 201,
    Unauthorized = 401,
    Forbbiden = 403,
    InvalidCompanyName = 400,
    InvalidCompanyUpdateAt = 400,
    InvalidCompanyCreatedAt = 400,
    InvalidClaimType = 400,
    InvalidClaimValue = 400,
    InvalidRoleName = 400,
    InvalidName = 400,
    InvalidValue = 400,
    InvalidEmail = 400,
    InvalidNickName = 400,
    InvalidPassword = 400,
    InvalidPhoneNumber = 400,
    UserNotFound = 404,
    CompanyNotFound = 404,
    ConflicUser = 409,
    CompanyQuantityOverflow = 400
}
