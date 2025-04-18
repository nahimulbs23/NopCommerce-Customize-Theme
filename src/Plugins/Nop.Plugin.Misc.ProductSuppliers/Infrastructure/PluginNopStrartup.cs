using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Autofac;
using Nop.Plugin.Misc.ProductSuppliers.Services;
using Nop.Plugin.Misc.ProductSuppliers.Factories;
using Nop.Plugin.Misc.ProductSuppliers.Infrastructure.Mapper;
using Nop.Plugin.Misc.ProductSupplier.Factories;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Nop.Plugin.Misc.ProductSuppliers.Infrastructure
{
    public class PluginNopStartup : INopStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Register services
            services.AddScoped<IProductSupplierService, ProductSuppliersService>();
            services.AddScoped<IProductSupplierModelFactory, ProductSupplierModelFactory>();
            // Register AutoMapper
            services.AddAutoMapper(typeof(ProductSupplierAutoMapperProfile).Assembly);
            //ViewLocation Expander
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new PluginViewLocationExpander());
            });

        }
        public void Configure(IApplicationBuilder application)
        {
            // No middleware needed for now
        }

        public int Order => 3000;
    }
}
