using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.ProductSuppliers.Services;
using Nop.Plugin.Misc.ProductSuppliers.Domains;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Framework.Controllers;
using Nop.Plugin.Misc.ProductSuppliers.Models;

using NUglify;
using System.Collections.Generic;
using Nop.Plugin.Misc.ProductSupplier.Factories;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Nop.Core.Domain.Vendors;
using Nop.Services.Vendors;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Core.Domain.Forums;

namespace Nop.Plugin.Misc.ProductSuppliers.Controllers;

[AuthorizeAdmin]
[Area("Admin")]
public class ProductSuppliersController : BasePluginController
{
    private readonly IProductSupplierService _productSuplierService;
    private readonly IProductSupplierModelFactory _productSupplierModelFactory;
    protected readonly IGenericAttributeService _genericAttributeService;
    protected readonly ILocalizationService _localizationService;
    protected readonly ILocalizedEntityService _localizedEntityService;
    protected readonly INotificationService _notificationService;




    public ProductSuppliersController(IProductSupplierService productSuplierService, ILocalizationService localizationService,
        ILocalizedEntityService localizedEntityService,
        INotificationService notificationService, IProductSupplierModelFactory productSupplierModelFactory)
    {
        _productSuplierService = productSuplierService;
        _productSupplierModelFactory = productSupplierModelFactory;

    }



    public async Task<IActionResult> Index()
    {
        await Task.CompletedTask; // Ensures the method is truly asynchronous
        var model = await _productSupplierModelFactory.PrepareProductSupplierListModelAsync(new ProductSupplierSearchModel());
        return View("~/Plugins/Misc.ProductSuppliers/Views/Suppliers/Index.cshtml", model);
    }
    public async Task<IActionResult> List()
    {
        await Task.CompletedTask;
        var model = await _productSupplierModelFactory.PrepareSupplierSearchModelAsync(new ProductSupplierSearchModel());
        //console
       // var model = await _productSupplierModelFactory.PrepareProductSupplierListModelAsync(new ProductSupplierSearchModel());
        return View("~/Plugins/Misc.ProductSuppliers/Areas/Admin/Views/List.cshtml", model);
    }
    [HttpPost]
    public async Task<IActionResult> List(ProductSupplierSearchModel searchModel)
    {
        var model = await _productSupplierModelFactory.PrepareProductSupplierListModelAsync(searchModel);
        return Json(model);
    }



    //[CheckPermission(StandardPermission.Customers.VENDORS_CREATE_EDIT_DELETE)]
    public virtual async Task<IActionResult> Create()
    {
        //prepare model
        var model = await _productSupplierModelFactory.PrepareProductSupplierModelAsync(new ProductSupplierModel(), null);

        return View("~/Plugins/Misc.ProductSuppliers/Areas/Admin/Views/Create.cshtml",model);
    }

    [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
    [FormValueRequired("save", "save-continue")]
    //[CheckPermission(StandardPermission.Customers.VENDORS_CREATE_EDIT_DELETE)]
    public virtual async Task<IActionResult> Create(ProductSupplierModel model, bool continueEditing, IFormCollection form)
    {
        //parse vendor attributes
        //var vendorAttributesXml = await ParseVendorAttributesAsync(form);
        //var warnings = (await _vendorAttributeParser.GetAttributeWarningsAsync(vendorAttributesXml)).ToList();
        //foreach (var warning in warnings)
        //{
        //    ModelState.AddModelError(string.Empty, warning);
        //}

        if (ModelState.IsValid)
        {
            var suppliers = model.ToEntity<ProductSupplierEntity>();
            await _productSuplierService.InsertAsync(suppliers);

            //activity log
            //await _customerActivityService.InsertActivityAsync("AddNewVendor",
            //    string.Format(await _localizationService.GetResourceAsync("ActivityLog.AddNewVendor"), vendor.Id), vendor);

            ////search engine name
            //model.SeName = await _urlRecordService.ValidateSeNameAsync(vendor, model.SeName, vendor.Name, true);
            //await _urlRecordService.SaveSlugAsync(vendor, model.SeName, 0);

            ////address
            //var address = model.Address.ToEntity<Address>();
            //address.CreatedOnUtc = DateTime.UtcNow;

            ////some validation
            //if (address.CountryId == 0)
            //    address.CountryId = null;
            //if (address.StateProvinceId == 0)
            //    address.StateProvinceId = null;
            //await _addressService.InsertAddressAsync(address);
            //vendor.AddressId = address.Id;
            //await _vendorService.UpdateVendorAsync(vendor);

            ////vendor attributes
            //await _genericAttributeService.SaveAttributeAsync(vendor, NopVendorDefaults.VendorAttributes, vendorAttributesXml);

            ////locales
            //await UpdateLocalesAsync(vendor, model);

            ////update picture seo file name
            //await UpdatePictureSeoNamesAsync(vendor);

           // _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Vendors.Added"));

            if (!continueEditing)
                return RedirectToAction("List");

            return RedirectToAction("Edit", new { id = suppliers.Id });
        }

        //prepare model
        model = await _productSupplierModelFactory.PrepareProductSupplierModelAsync(model, null, true);

        //if we got this far, something failed, redisplay form
        return View(model);
    }


    public virtual async Task<IActionResult> Edit(int id)
    {
        //try to get a vendor with the specified id
        var Supliers = await _productSuplierService.GetByIdAsync(id);
        if (Supliers == null)
            return RedirectToAction("List");

        //prepare model
        var model = await _productSupplierModelFactory.PrepareProductSupplierModelAsync(null, Supliers);

        //if (!_forumSettings.AllowPrivateMessages && model.PmCustomerId > 0)
        //    _notificationService.WarningNotification("Private messages are disabled. Do not forget to enable them.");

        return View("~/Plugins/Misc.ProductSuppliers/Areas/Admin/Views/Edit.cshtml",model);
    }

    [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
    //[CheckPermission(StandardPermission.Customers.VENDORS_CREATE_EDIT_DELETE)]
    public virtual async Task<IActionResult> Edit(ProductSupplierModel model, bool continueEditing, IFormCollection form)
    {
        //try to get a vendor with the specified id
        var supplier = await _productSuplierService.GetByIdAsync(model.Id);
        if (supplier == null)
            return RedirectToAction("List");

        if (ModelState.IsValid)
        {
            //var prevPictureId = vendor.PictureId;
            supplier = model.ToEntity(supplier);
            await _productSuplierService.UpdateAsync(supplier);

     
            if (!continueEditing)
                return RedirectToAction("List");

            return RedirectToAction("Edit", new { id = supplier.Id });
        }

        //prepare model
        model = await _productSupplierModelFactory.PrepareProductSupplierModelAsync(model, supplier, true);

        //if we got this far, something failed, redisplay form
        return View("~/Plugins/Misc.ProductSuppliers/Areas/Admin/Views/Edit.cshtml",model);
    }

    [HttpPost]
    //[CheckPermission(StandardPermission.Customers.VENDORS_CREATE_EDIT_DELETE)]
    public virtual async Task<IActionResult> Delete(int id)
    {
        //try to get a vendor with the specified id
        var vendor = await _productSuplierService.GetByIdAsync(id);
        if (vendor == null)
            return RedirectToAction("List");

       
        //delete a vendor
        await _productSuplierService.DeleteAsync(vendor);

        return RedirectToAction("List");
    }

    //[HttpGet]
    //public async Task<IActionResult> Index(string name, string email)
    //{
    //    if(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(email))
    //    {
    //        Index();

    //    }
    //    var suppliers = await _productSuplierService.GetAllSuppliers(name, email);
    //    if (suppliers == null)
    //    {
    //        return NotFound();
    //    }
    //    var model = new ProductSupplierSearchModel
    //    {
    //        Name = name,
    //        Email = email,
    //        Suppliers = suppliers
    //    };
    //    return View("~/Plugins/Misc.ProductSuppliers/Views/Suppliers/Index.cshtml", model);
    //}
    //public async Task<IActionResult> Create()
    //{
    //    await Task.CompletedTask;
    //    var model = new ProductSupplierEntity();
    //    return View("~/Plugins/Misc.ProductSuppliers/Areas/Admin/Views/Create.cshtml", model);
    //}

    //[HttpPost]
    //public async Task<IActionResult> Create(ProductSupplier supplier)
    //{
    //    Console.WriteLine("Supplier: ", supplier);
    //    if (ModelState.IsValid)
    //    {
    //        var suppliers = new ProductSupplier
    //        {
    //            Name = supplier.Name,
    //            Email = supplier.Email,
    //            Description = supplier.Description,
    //            Address = string.IsNullOrWhiteSpace(supplier.Address) ? null : supplier.Address,

    //            Phone = supplier.Phone,
    //            Website = supplier?.Website
    //        };
    //        await _productSuplierService.InsertAsync(suppliers);
    //        return RedirectToAction("Index");
    //    }
    //    return View("~/Plugins/Misc.ProductSuppliers/Views/Suppliers/Create.cshtml", supplier);
    //}
    //public async Task<IActionResult> Edit(int Id)
    //{
    //    var supplier = await _productSuplierService.GetByIdAsync(Id);
    //    if(supplier == null)
    //    {
    //        return NotFound();
    //    }
    //    return View("~/Plugins/Misc.ProductSuppliers/Views/Suppliers/Edit.cshtml", supplier);
    //}
    //[HttpPost]
    //public async Task<IActionResult> Edit(int id, ProductSupplier supplier)
    //{
    //    var suppliers = await _productSuplierService.GetByIdAsync(id);
    //    if (suppliers== null)
    //    {
    //        return NotFound();
    //    }
    //    if (ModelState.IsValid)
    //    {
    //        await _productSuplierService.UpdateAsync(supplier);
    //        return RedirectToAction("Index");
    //    }
    //    return View("~/Plugins/Misc.ProductSuppliers/Views/Suppliers/Edit.cshtml", supplier);
    //}

    //[HttpPost]

    //public async Task<IActionResult> Delete(int Id)
    //{
    //    var supplier = await _productSuplierService.GetByIdAsync(Id);
    //    if (supplier != null)     await _productSuplierService.DeleteAsync(supplier);
    //    return RedirectToAction("Index");
    //}


}