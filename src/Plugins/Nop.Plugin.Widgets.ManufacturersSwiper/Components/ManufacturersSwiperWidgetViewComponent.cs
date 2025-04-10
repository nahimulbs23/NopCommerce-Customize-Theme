using Microsoft.AspNetCore.Mvc;
using Nop.Services.Catalog;
using Nop.Services.Media;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.ManufacturersSwiper.Components;
public class ManufacturersSwiperWidgetViewComponent : NopViewComponent
{
     private readonly IManufacturerService _manufacturerService;
    private readonly IPictureService _pictureService;

    public ManufacturersSwiperWidgetViewComponent(IManufacturerService manufacturerService, IPictureService pictureService)
    {
        _manufacturerService = manufacturerService;
        _pictureService = pictureService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var manufacturers = await _manufacturerService.GetAllManufacturersAsync();
        var pictureUrls = new List<string>();
        foreach (var manufacturer in manufacturers)
        {
            var pictureUrl = await _pictureService.GetPictureUrlAsync(manufacturer.PictureId,200);
                if(!string.IsNullOrEmpty(pictureUrl))
                    pictureUrls.Add(pictureUrl);
            
        }
        // return Content("Manufacturers SwiperWidget View Component");
        return View("~/Plugins/Widgets.ManufacturersSwiper/Views/ManufacturersSlider.cshtml", pictureUrls);
    }
}
