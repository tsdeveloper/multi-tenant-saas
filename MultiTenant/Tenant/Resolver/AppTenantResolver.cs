using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MultiTenant.Models;
using SaasKit.Multitenancy;

namespace MultiTenant.Tenant.Resolver
{
    public class AppTenantResolver : MemoryCacheTenantResolver<AppTenant>
    {
        public IEnumerable<AppTenant> Tenants { get; set; }

        public AppTenantResolver(IMemoryCache cache, ILoggerFactory loggerFactory, IOptions<MultitenancyOptions> options) : base(cache, loggerFactory)
        {
            Tenants = options.Value.Tenants;
        }



        public AppTenantResolver(IMemoryCache cache, ILoggerFactory loggerFactory, MemoryCacheTenantResolverOptions options) : base(cache, loggerFactory, options)
        {

        }

        protected override string GetContextIdentifier(HttpContext context)
        {
            return context.Request.Host.Value.ToLower();
        }

        protected override IEnumerable<string> GetTenantIdentifiers(TenantContext<AppTenant> context)
        {
            return context.Tenant.Hostnames;
        }

        protected override Task<TenantContext<AppTenant>> ResolveAsync(HttpContext context)
        {
            TenantContext<AppTenant> tenantContext = null;
            var tenant =
                Tenants.FirstOrDefault(t => t.Hostnames.Any(h => h.Equals(context.Request.Host.Value.ToLower())));
            if (tenant != null)
                tenantContext = new TenantContext<AppTenant>(tenant);

            return Task.FromResult(tenantContext);
        }
    }
}