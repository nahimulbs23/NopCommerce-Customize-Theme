
using Nop.Plugin.Misc.ProductSupplier.Factories;
using Nop.Plugin.Misc.ProductSuppliers.Models;
using Nop.Plugin.Misc.ProductSuppliers.Services;
using Nop.Web.Framework.Models.Extensions;
using Nop.Web.Framework.Models;
using Nop.Plugin.Misc.ProductSuppliers.Domains;
using Nop.Services.Seo;
using Nop.Web.Framework.Extensions;
using Nop.Web.Framework.Factories;
using MimeKit.Cryptography;
using AutoMapper;
using Nop.Services.Localization; 

using Nop.Services.Common;

namespace Nop.Plugin.Misc.ProductSuppliers.Factories;

public class ProductSupplierModelFactory : IProductSupplierModelFactory
{
    protected readonly IProductSupplierService _productSupplierService;
    protected readonly IUrlRecordService _urlRecordService;
    protected readonly IMapper _mapper;
    protected readonly ILocalizationService _localizationService;
    protected readonly ILocalizedModelFactory _localizedModelFactory;
    protected readonly IAddressService _addressService;
    public ProductSupplierModelFactory(IProductSupplierService productSupplierService, 
        IUrlRecordService urlRecordService,
        ILocalizationService localizationService,
        ILocalizedModelFactory localizedModelFactory,
        IAddressService addressService
        )
    {
        _productSupplierService = productSupplierService;
        _urlRecordService = urlRecordService;
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ProductSupplierEntity, ProductSupplierModel>();
            cfg.CreateMap<ProductSupplierModel, ProductSupplierEntity>();
        }).CreateMapper();
        _localizedModelFactory = localizedModelFactory;
        _localizationService = localizationService;
        _addressService = addressService;
    }

    public async Task<ProductSupplierListModel> PrepareProductSupplierListModelAsync(ProductSupplierSearchModel searchModel)
    {
        ArgumentNullException.ThrowIfNull(searchModel);

        //get vendors
        var suppliers = await _productSupplierService.GetAllProductSupplierAsync(showHidden: true,
            name: searchModel.SearchName,
            email: searchModel.SearchEmail,
            pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

        //prepare list model
        

        var model = await new ProductSupplierListModel().PrepareToGridAsync(searchModel, suppliers, () =>
        {
            //fill in model values from the entity
            return suppliers.SelectAwait(async supplier =>
            {
                var productSupplierModel = _mapper.Map<ProductSupplierModel>(supplier);

                productSupplierModel.SeName = await _urlRecordService.GetSeNameAsync(supplier, 0, true, false);

                return productSupplierModel;
            });
        });

        return model;
    }


    public Task<ProductSupplierSearchModel> PrepareSupplierSearchModelAsync(ProductSupplierSearchModel searchModel)
    {
        ArgumentNullException.ThrowIfNull(searchModel);

        //prepare page parameters
        searchModel.SetGridPageSize();

        return Task.FromResult(searchModel);
    }

    public virtual async Task<ProductSupplierModel> PrepareProductSupplierModelAsync(ProductSupplierModel model, ProductSupplierEntity productSupplier, bool excludeProperties = false)
    {
        Func<ProductSupplierLocalizedModel, int, Task> localizedModelConfiguration = null;

        if (productSupplier != null)
        {
            //fill in model values from the entity
            if (model == null)
            {
                model = _mapper.Map<ProductSupplierModel>(productSupplier);
                model.SeName = await _urlRecordService.GetSeNameAsync(productSupplier, 0, true, false);
            }

            //define localized model configuration action
            localizedModelConfiguration = async (locale, languageId) =>
            {
                locale.Name = await _localizationService.GetLocalizedAsync(productSupplier, entity => entity.Name, languageId, false, false);
                locale.Description = await _localizationService.GetLocalizedAsync(productSupplier, entity => entity.Description, languageId, false, false);
                //locale.MetaKeywords = await _localizationService.GetLocalizedAsync(productSupplier, entity => entity.MetaKeywords, languageId, false, false);
                locale.MetaDescription = await _localizationService.GetLocalizedAsync(productSupplier, entity => entity.MetaDescription, languageId, false, false);
                locale.MetaTitle = await _localizationService.GetLocalizedAsync(productSupplier, entity => entity.MetaTitle, languageId, false, false);
                locale.SeName = await _urlRecordService.GetSeNameAsync(productSupplier, languageId, false, false);
            };

            //prepare associated customers
            //await PrepareAssociatedCustomerModelsAsync(model.AssociatedCustomers, productSupplier);

            //if (productSupplier.PmCustomerId > 0)
            //{
            //    var pmCustomer = await _customerService.GetCustomerByIdAsync(productSupplier.PmCustomerId.Value);
            //    model.PmCustomerInfo = pmCustomer.Email;
            //}

            ////prepare nested search models
            //PrepareVendorNoteSearchModel(model.VendorNoteSearchModel, productSupplier);
        }

        //set default values for the new model
        if (productSupplier == null)
        {
            model.PageSize = 6;
            model.Active = true;
            model.AllowCustomersToSelectPageSize = true;
          //model.PageSizeOptions = _vendorSettings.DefaultVendorPageSizeOptions;
        }

        //model.PrimaryStoreCurrencyCode = (await _currencyService.GetCurrencyByIdAsync(_currencySettings.PrimaryStoreCurrencyId)).CurrencyCode;

        //prepare localized models
        if (!excludeProperties)
            model.Locales = await _localizedModelFactory.PrepareLocalizedModelsAsync(localizedModelConfiguration);

        //prepare model productSupplier attributes
      //  await PrepareVendorAttributeModelsAsync(model.VendorAttributes, productSupplier);

        //prepare address model
        //var address = await _addressService.GetAddressByIdAsync(productSupplier?.AddressId ?? 0);
        //if (!excludeProperties && address != null)
        //    model.Address = address.ToModel(model.Address);
        //await _addressModelFactory.PrepareAddressModelAsync(model.Address, address);

        return model;
    }
}