using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Seo;

namespace Nop.Plugin.Misc.ProductSuppliers.Domains;
public class ProductSupplierEntity : BaseEntity, ILocalizedEntity, ISlugSupported
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string AdminComment { get; set; }
    public bool Active { get; set; }
    public int DisplayOrder { get; set; }
    public string MetaDescription { get; set; }
    public string MetaTitle { get; set; }
    public string SeName { get; set; }
    public int PageSize { get; set; }
    public bool AllowCustomersToSelectPageSize { get; set; }
    public string PageSizeOptions { get; set; }
}