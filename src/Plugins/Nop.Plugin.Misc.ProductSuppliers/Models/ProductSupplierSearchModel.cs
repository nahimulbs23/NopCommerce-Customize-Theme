using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.ProductSuppliers.Models;

/// <summary>
/// Represents a vendor search model
/// </summary>
public partial record ProductSupplierSearchModel : BaseSearchModel
{
    #region Properties

    [NopResourceDisplayName("Admin.ProductSupplier.List.SearchName")]
    public string SearchName { get; set; }

    [NopResourceDisplayName("Admin.ProductSupplier.List.SearchEmail")]
    public string SearchEmail { get; set; }

    #endregion
}