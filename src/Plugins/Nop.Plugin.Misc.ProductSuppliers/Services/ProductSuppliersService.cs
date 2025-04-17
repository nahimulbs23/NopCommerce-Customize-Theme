using Nop.Plugin.Misc.ProductSuppliers.Domains;
using Nop.Data;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Nop.Plugin.Misc.ProductSuppliers.Services;
public class ProductSuppliersService : IProductSupplierService
{
    private readonly IRepository<ProductSupplier> _repository;

    public ProductSuppliersService(IRepository<ProductSupplier> repository)
    {
        _repository = repository;
    }

    public async Task InsertAsync(ProductSupplier supplier) {
         await _repository.InsertAsync(supplier); }
    public async Task UpdateAsync(ProductSupplier supplier) => await _repository.UpdateAsync(supplier);
    public async Task DeleteAsync(ProductSupplier supplier) => await _repository.DeleteAsync(supplier);
    public async Task<ProductSupplier> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
    public async Task<IList<ProductSupplier>> GetAllAsync() => await _repository.GetAllAsync(
    (Func<IQueryable<ProductSupplier>, IQueryable<ProductSupplier>>)(query => query));
    public async Task<IList<ProductSupplier>> GetAllSuppliers(string name, string email)
    {
        return await _repository.GetAllAsync(query =>
        {
            if (!string.IsNullOrEmpty(name))
                query = query.Where(x => x.Name.Contains(name));
            if (!string.IsNullOrEmpty(email))
                query = query.Where(x => x.Email.Contains(email));
            return query;
        });
    }
}
