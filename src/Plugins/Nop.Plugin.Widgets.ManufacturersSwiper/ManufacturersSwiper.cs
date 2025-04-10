using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Widgets.ManufacturersSwiper.Components;
using Nop.Services.Cms;
using Nop.Services.Plugins;

namespace Nop.Plugin.Widgets.ManufacturersSwiper;
public class ManufacturersSwiper : BasePlugin, IWidgetPlugin
{
    // Gets a value indicating whether to hide this plugin on the widget list page in the admin area
    public bool HideInWidgetList => false; // Implementation for IWidgetPlugin.HideInWidgetList

    // Gets a type of a view component for displaying widget
    // <param name="widgetZone">Name of the widget zone</param>
    // <returns>View component type</returns>
    public Type GetWidgetViewComponent(string widgetZone)
    {
        return typeof(ManufacturersSwiperWidgetViewComponent);
    }

    // Gets widget zones where this widget should be rendered
    // A task that represents the asynchronous operation
    // The task result contains the widget zones
    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string> { "home_page_before_news" });
    }
    public override async Task InstallAsync()
    {
        // Add your installation logic here
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        // Add your uninstallation logic here
        await base.UninstallAsync();
    }
}
