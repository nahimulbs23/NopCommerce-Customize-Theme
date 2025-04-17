using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Plugin.Misc.ProductSuppliers.Services;
using Nop.Core.Infrastructure;

namespace Nop.Plugin.Misc.ProductSuppliers.Infrastructure;
public class PluginNopStrartup : INopStartup // Corrected the interface name
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductSupplierService, ProductSuppliersService>();
    }

    /// <summary>
    /// Configure the using of added middleware
    /// </summary>
    /// <param name="application">Builder for configuring an application's request pipeline</param>
    public void Configure(IApplicationBuilder application)
    {
    }

    /// <summary>
    /// Gets order of this startup configuration implementation
    /// </summary>
    public int Order => 3000;
}
