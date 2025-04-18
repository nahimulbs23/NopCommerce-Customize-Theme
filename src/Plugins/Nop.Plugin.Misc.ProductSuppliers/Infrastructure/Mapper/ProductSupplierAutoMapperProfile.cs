using AutoMapper;
using Nop.Core.Infrastructure.Mapper;
using Nop.Plugin.Misc.ProductSuppliers.Domains;
using Nop.Plugin.Misc.ProductSuppliers.Models;


namespace Nop.Plugin.Misc.ProductSuppliers.Infrastructure.Mapper;

/// <summary>
/// Represents AutoMapper configuration for plugin models
/// </summary>
public class ProductSupplierAutoMapperProfile : Profile, IOrderedMapperProfile
{
    #region Ctor

    public ProductSupplierAutoMapperProfile()
    {
        CreateMap<ProductSupplierEntity, ProductSupplierModel>();
        CreateMap<ProductSupplierModel, ProductSupplierEntity>();
    }

    #endregion

    #region Properties

    /// <summary>
    /// Order of this mapper implementation
    /// </summary>
    public int Order => 1;

    #endregion
}