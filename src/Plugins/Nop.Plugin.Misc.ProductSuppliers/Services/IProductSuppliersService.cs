using Nop.Plugin.Misc.ProductSuppliers.Domains;
using Nop.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Nop.Plugin.Misc.ProductSuppliers.Services;
    public interface IProductSupplierService
    {
        Task InsertAsync(ProductSupplierEntity supplier);
        Task UpdateAsync(ProductSupplierEntity supplier);
        Task DeleteAsync(ProductSupplierEntity supplier);
        Task<ProductSupplierEntity> GetByIdAsync(int id);
        Task<IList<ProductSupplierEntity>> GetAllAsync();
        Task<IList<ProductSupplierEntity>> GetAllSuppliers(string name, string email,int pageIndex,int pageSize);
        Task<IPagedList<ProductSupplierEntity>> GetAllProductSupplierAsync(string name = "", string email = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
}
