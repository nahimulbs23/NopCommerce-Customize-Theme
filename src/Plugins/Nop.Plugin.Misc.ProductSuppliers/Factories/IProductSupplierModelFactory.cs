using System.Threading.Tasks;
using Nop.Core.Domain.Vendors;
using Nop.Plugin.Misc.ProductSuppliers.Domains;
using Nop.Plugin.Misc.ProductSuppliers.Models;

namespace Nop.Plugin.Misc.ProductSupplier.Factories;

    public interface IProductSupplierModelFactory
    {
        Task<ProductSupplierListModel> PrepareProductSupplierListModelAsync(ProductSupplierSearchModel searchModel);
       Task<ProductSupplierSearchModel> PrepareSupplierSearchModelAsync(ProductSupplierSearchModel searchModel);
    Task<ProductSupplierModel> PrepareProductSupplierModelAsync(ProductSupplierModel model, ProductSupplierEntity productSupplier, bool excludeProperties = false);
}
