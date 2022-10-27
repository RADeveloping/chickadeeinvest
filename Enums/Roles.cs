using System;
using System.ComponentModel.DataAnnotations;

namespace chickadee.Enums
{
    public enum Roles
    {
        SuperAdmin,
        Admin,
        PropertyManager,
        Tenant
    }

    public enum PublicRoles
    {
        [Display(Name = "Property Manager")]
        PropertyManager = Roles.PropertyManager,
        Tenant = Roles.Tenant
    }
}

