using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.ProductSuppliers.Infrastructure
{
    public class PluginViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            // You can add values here to use later in ExpandViewLocations if needed
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            // Only add plugin view paths if we are in the Admin area
            if (context.AreaName?.Equals("Admin", System.StringComparison.OrdinalIgnoreCase) == true)
            {
                var pluginLocations = new[]
                {
                    // Add your custom plugin locations here
                    "/Plugins/Misc.ProductSuppliers/Areas/Admin/Views/{1}/{0}.cshtml",
                    "/Plugins/Misc.ProductSuppliers/Areas/Admin/Views/Shared/{0}.cshtml"
                };

                return pluginLocations.Concat(viewLocations);
            }

            // Return default locations otherwise
            return viewLocations;
        }
    }
}
