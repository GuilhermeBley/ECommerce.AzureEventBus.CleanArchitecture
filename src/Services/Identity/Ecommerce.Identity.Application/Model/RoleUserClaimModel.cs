﻿namespace Ecommerce.Identity.Application.Model;

public class RoleUserClaimModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
