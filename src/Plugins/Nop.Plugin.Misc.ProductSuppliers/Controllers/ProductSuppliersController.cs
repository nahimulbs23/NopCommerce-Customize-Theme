using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.ProductSuppliers.Services;
using Nop.Plugin.Misc.ProductSuppliers.Domains;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Framework.Controllers;
using Nop.Plugin.Misc.ProductSuppliers.Models;
using NUglify;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.ProductSuppliers.Controllers;

[AuthorizeAdmin]
[Area("Admin")]
public class ProductSuppliersController : BasePluginController
{
    private readonly IProductSupplierService _productSuplierService;

    public ProductSuppliersController(IProductSupplierService productSuplierService)
    {
        _productSuplierService = productSuplierService;
    }

    public async Task<IActionResult> Index()
    {
        var suppliers = await _productSuplierService.GetAllAsync();
        if (suppliers == null)
        {
            return NotFound();
        } 
        var model = new ProductSupplierSearchModel();
        model.Suppliers = suppliers;
        return View("~/Plugins/Misc.ProductSuppliers/Views/Suppliers/Index.cshtml", model);
        
        //    return View("~/Plugins/Misc.ProductSuppliers/Views/Suppliers/Index.cshtml", suppliers);
    }

    [HttpGet]
    public async Task<IActionResult> Index(string name, string email)
    {
        if(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(email))
        {
            Index();

        }
        var suppliers = await _productSuplierService.GetAllSuppliers(name, email);
        if (suppliers == null)
        {
            return NotFound();
        }
        var model = new ProductSupplierSearchModel
        {
            Name = name,
            Email = email,
            Suppliers = suppliers
        };
        return View("~/Plugins/Misc.ProductSuppliers/Views/Suppliers/Index.cshtml", model);
    }
    public async Task<IActionResult> Create()
    {
        var model = new ProductSupplier();
        return View("~/Plugins/Misc.ProductSuppliers/Views/Suppliers/Create.cshtml",model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductSupplier supplier)
    {
        Console.WriteLine("Supplier: ", supplier);
        if (ModelState.IsValid)
        {
            var suppliers = new ProductSupplier
            {
                Name = supplier.Name,
                Email = supplier.Email,
                Description = supplier.Description,
                Address = string.IsNullOrWhiteSpace(supplier.Address) ? null : supplier.Address,

                Phone = supplier.Phone,
                Website = supplier?.Website
            };
            await _productSuplierService.InsertAsync(suppliers);
            return RedirectToAction("Index");
        }
        return View("~/Plugins/Misc.ProductSuppliers/Views/Suppliers/Create.cshtml", supplier);
    }
    public async Task<IActionResult> Edit(int Id)
    {
        var supplier = await _productSuplierService.GetByIdAsync(Id);
        if(supplier == null)
        {
            return NotFound();
        }
        return View("~/Plugins/Misc.ProductSuppliers/Views/Suppliers/Edit.cshtml", supplier);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id, ProductSupplier supplier)
    {
        var suppliers = await _productSuplierService.GetByIdAsync(id);
        if (suppliers== null)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            await _productSuplierService.UpdateAsync(supplier);
            return RedirectToAction("Index");
        }
        return View("~/Plugins/Misc.ProductSuppliers/Views/Suppliers/Edit.cshtml", supplier);
    }

    [HttpPost]

    public async Task<IActionResult> Delete(int Id)
    {
        var supplier = await _productSuplierService.GetByIdAsync(Id);
        if (supplier != null)     await _productSuplierService.DeleteAsync(supplier);
        return RedirectToAction("Index");
    }


}