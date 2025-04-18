using Nop.Plugin.Misc.ProductSuppliers.Domains;
using Nop.Data;
using Nop.Core;

namespace Nop.Plugin.Misc.ProductSuppliers.Services;
public class ProductSuppliersService : IProductSupplierService
{

    private readonly IRepository<ProductSupplierEntity> _repository;

    public ProductSuppliersService(IRepository<ProductSupplierEntity> repository)
    {
        _repository = repository;
    }

    public async Task InsertAsync(ProductSupplierEntity supplier) {
         await _repository.InsertAsync(supplier); }
    public async Task UpdateAsync(ProductSupplierEntity supplier) => await _repository.UpdateAsync(supplier);
    public async Task DeleteAsync(ProductSupplierEntity supplier) => await _repository.DeleteAsync(supplier);
    public async Task<ProductSupplierEntity> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
    public async Task<IList<ProductSupplierEntity>> GetAllAsync() => await _repository.GetAllAsync(
    (Func<IQueryable<ProductSupplierEntity>, IQueryable<ProductSupplierEntity>>)(query => query));
    public async Task<IList<ProductSupplierEntity>> GetAllSuppliers(string name, string email, int pageIndex, int p)
    {
        return await _repository.GetAllAsync(query =>
        {
            if (!string.IsNullOrEmpty(name))
                query = query.Where(x => x.Name.Contains(name));
            if (!string.IsNullOrEmpty(email))
                query = query.Where(x => x.Email.Contains(email));
            if (pageIndex > 0)
                query = query.Skip((pageIndex - 1) * p).Take(p);

           
            return query;
        });
    }
    public virtual async Task<IPagedList<ProductSupplierEntity>> GetAllProductSupplierAsync(string name = "", string email = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
    {
        var suppliers = await _repository.GetAllPagedAsync(query =>
        {
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(v => v.Name.Contains(name));

            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(v => v.Email.Contains(email));

            if (!showHidden)
                query = query.Where(v => v.Active);

            
           query = query.OrderBy(v => v.DisplayOrder).ThenBy(v => v.Name).ThenBy(v => v.Email);

            return query;
        }, pageIndex, pageSize);

        return suppliers;
    }
}
