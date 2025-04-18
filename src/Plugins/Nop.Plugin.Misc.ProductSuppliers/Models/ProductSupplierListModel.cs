using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.ProductSuppliers.Models;

/// <summary>
/// Represents a vendor list model
/// </summary>
public partial record ProductSupplierListModel : BasePagedListModel<ProductSupplierModel>
{
}