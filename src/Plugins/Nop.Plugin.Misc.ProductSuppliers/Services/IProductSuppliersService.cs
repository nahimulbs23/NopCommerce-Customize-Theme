using Nop.Plugin.Misc.ProductSuppliers.Domains;

namespace Nop.Plugin.Misc.ProductSuppliers.Services;
    public interface IProductSupplierService
    {
        Task InsertAsync(ProductSupplier supplier);
        Task UpdateAsync(ProductSupplier supplier);
        Task DeleteAsync(ProductSupplier supplier);
        Task<ProductSupplier> GetByIdAsync(int id);
        Task<IList<ProductSupplier>> GetAllAsync();
        Task<IList<ProductSupplier>> GetAllSuppliers(string name, string email);
}
