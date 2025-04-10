using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.SpecialOffers.Controllers;
public class WidgetSpecialOffersController : BasePluginController
{
    public async Task<IActionResult> Configure(bool showtour = false)
    {
        return View("~/Plugins/Widgets.SpecialOffers/Views/Configure.cshtml");
    }

}
