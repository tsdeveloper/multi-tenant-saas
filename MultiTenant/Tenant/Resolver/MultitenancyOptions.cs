using System.Collections.Generic;
using System.Collections.ObjectModel;
using MultiTenant.Models;

namespace MultiTenant.Tenant.Resolver
{
    public class MultitenancyOptions
    {
        public Collection<AppTenant> Tenants { get; set; }
    }
}