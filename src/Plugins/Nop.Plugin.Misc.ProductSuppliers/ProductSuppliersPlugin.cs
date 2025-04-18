using Humanizer.Localisation;
using Nop.Plugin.Misc.ProductSuppliers.Controllers;
using Nop.Services.Cms;
using Nop.Services.Events;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Web.Framework.Events;
using Nop.Web.Framework.Menu;

using Nop.Services.Localization;

namespace Nop.Plugin.Misc.ProductSuppliers;

public class ProductSuppliersPlugin : BasePlugin
{
    private readonly ILocalizationService _localizationService;

    private readonly IPermissionService _permissionService;

    public ProductSuppliersPlugin(ILocalizationService localizationService,
        IPermissionService permissionService)
    {
        _localizationService = localizationService;
        _permissionService = permissionService;
    }

    //public bool HideInWidgetList => false;

    public override async Task InstallAsync()
    {
        var resources = new Dictionary<string, string>
        {
            ["Admin.ProductSupplier.List.SearchName"] = "Search by name",
            ["Admin.ProductSupplier.List.SearchEmail"] = "Search by email",
            ["Admin.ProductSupplier.List.SearchName.Hint"] = "Search by name",
            ["Admin.ProductSupplier.List.SearchEmail.Hint"] = "Search by email",
            ["Admin.ProductSupplies.Fields.Name"] = "Name",
            ["Admin.ProductSupplies.Fields.Name.Hint"] = "Enter the name of the supplier.",

            ["Admin.ProductSupplies.Fields.Email"] = "Email",
            ["Admin.ProductSupplies.Fields.Email.Hint"] = "Enter the supplier's email address.",

            ["Admin.ProductSupplies.Fields.Description"] = "Description",
            ["Admin.ProductSupplies.Fields.Description.Hint"] = "Enter a description for this supplier.",

            ["Admin.ProductSupplies.Fields.AdminComment"] = "Admin comment",
            ["Admin.ProductSupplies.Fields.AdminComment.Hint"] = "Internal notes for administrators.",

            ["Admin.ProductSupplies.Fields.Phone"] = "Phone",
            ["Admin.ProductSupplies.Fields.Phone.Hint"] = "Enter the supplier's contact number.",

            ["Admin.ProductSupplies.Fields.Address"] = "Address",
            ["Admin.ProductSupplies.Fields.Address.Hint"] = "Enter the supplier's full address.",

            ["Admin.ProductSupplies.Fields.Active"] = "Active",
            ["Admin.ProductSupplies.Fields.Active.Hint"] = "Uncheck to disable this supplier.",

            ["Admin.ProductSupplies.Fields.DisplayOrder"] = "Display order",
            ["Admin.ProductSupplies.Fields.DisplayOrder.Hint"] = "The order in which this supplier is displayed.",

            ["Admin.ProductSupplies.Fields.MetaDescription"] = "Meta description",
            ["Admin.ProductSupplies.Fields.MetaDescription.Hint"] = "Meta description for SEO purposes.",

            ["Admin.ProductSupplies.Fields.MetaTitle"] = "Meta title",
            ["Admin.ProductSupplies.Fields.MetaTitle.Hint"] = "Meta title for SEO purposes.",

            ["Admin.ProductSupplies.Fields.SeName"] = "Search engine friendly name",
            ["Admin.ProductSupplies.Fields.SeName.Hint"] = "The URL-friendly name for this supplier.",

            ["Admin.ProductSupplies.Fields.PageSize"] = "Page size",
            ["Admin.ProductSupplies.Fields.PageSize.Hint"] = "Number of products per page.",

            ["Admin.ProductSupplies.Fields.AllowCustomersToSelectPageSize"] = "Allow customers to select page size",
            ["Admin.ProductSupplies.Fields.AllowCustomersToSelectPageSize.Hint"] = "Check to let users choose how many items they see per page.",

            ["Admin.ProductSupplies.Fields.PageSizeOptions"] = "Page size options",
            ["Admin.ProductSupplies.Fields.PageSizeOptions.Hint"] = "Comma-separated list of page size options (e.g., 10,20,50).",
             ["Admin.ProductSuppliers.EditSupplierDetails"] = "Edit Supplier Details",
            ["Admin.ProductSuppliers.BackToList"] = "Back to List",
            ["Admin.ProductSuppliers.AddNew"] = "Add New Supplier",
            ["Admin.ProductSuppliers.ProductSupplies"] = "Product Suppliers"
        };


        await _localizationService.AddOrUpdateLocaleResourceAsync(resources);
        await base.InstallAsync();
    }

    /// <summary>
    /// Uninstall the plugin
    /// </summary>
    /// <returns>A task that represents the asynchronous operation</returns>
    public override async Task UninstallAsync()
    {
       

        await base.UninstallAsync();
    }

}

public class EventConsumer : IConsumer<AdminMenuCreatedEvent>
{
    private readonly IPermissionService _permissionService;

    public EventConsumer(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    public async Task HandleEventAsync(AdminMenuCreatedEvent eventMessage)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_PLUGINS))
            return;

        eventMessage.RootMenuItem.InsertBefore("Local plugins",
            new AdminMenuItem
            {
                SystemName = "Misc.ProductSupliers",
                Title = "Product Supliers",
                Url = eventMessage.GetMenuItemUrl("ProductSuppliers", "List"),
                IconClass = "far fa-dot-circle",
                Visible = true,
            });

    }
}
