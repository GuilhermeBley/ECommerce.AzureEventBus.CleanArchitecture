﻿namespace Ecommerce.Catalog.Core.Exceptions;

public enum ErrorEnum
{
    Unauthorized = 401,
    Forbbiden = 403,
    InvalidDescription = 400,
    InvalidName = 400,
    InvalidValue = 400,
    InvalidEmail = 400,
    InvalidNickName = 400,
    InvalidPassword = 400,
    InvalidPhoneNumber = 400,
    InvalidClaimType = 400,
    InvalidClaimValue = 400,
    InvalidIdClaim = 400,
    InvalidIdRole = 400,
    InvalidCreateAtCompanyProduct = 400,
    ProductNotFound = 404
}
