using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.SpecialOffers.Components
{
    public class SpecialOffersWidgetViewComponent : NopViewComponent
    {
        //private readonly IProductService _productService;
        //private readonly IUrlRecordService _urlRecordService;

        //public SpecialOffersWidgetViewComponent(IProductService productService, IUrlRecordService urlRecordService)
        //{
        //    _productService = productService;
        //    _urlRecordService = urlRecordService;
        //}

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            return View("~/Plugins/Widgets.SpecialOffers/Views/PublicInfo.cshtml");
        }
    }
}
