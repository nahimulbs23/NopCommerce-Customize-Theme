using System.ComponentModel.DataAnnotations;
using Nop.Core.Domain.Catalog;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.ProductSuppliers.Models;

/// <summary>
/// Represents a vendor model
/// </summary>
public partial record ProductSupplierModel : BaseNopEntityModel, ILocalizedModel<ProductSupplierLocalizedModel>
{
    #region Ctor

    public ProductSupplierModel()
    {
        if (PageSize < 1)
            PageSize = 5;
        ProductSupplierAttributes = new List<ProductSupplierAttributeModel>();
        Locales = new List<ProductSupplierLocalizedModel>();
    }

    #endregion

    #region Properties

    [NopResourceDisplayName("Admin.ProductSupplies.Fields.Name")]
    public string Name { get; set; }

    [DataType(DataType.EmailAddress)]
    [NopResourceDisplayName("Admin.ProductSupplies.Fields.Email")]
    public string Email { get; set; }

    [NopResourceDisplayName("Admin.ProductSupplies.Fields.Description")]
    public string Description { get; set; }

    [NopResourceDisplayName("Admin.ProductSupplies.Fields.AdminComment")]
    public string AdminComment { get; set; }

    [NopResourceDisplayName("Admin.ProductSupplies.Fields.Phone")]
    public string Phone { get; set; }
    [NopResourceDisplayName("Admin.ProductSupplies.Fields.Address")]
    public string Address { get; set; }

    [NopResourceDisplayName("Admin.ProductSupplies.Fields.Active")]
    public bool Active { get; set; }

    [NopResourceDisplayName("Admin.ProductSupplies.Fields.DisplayOrder")]
    public int DisplayOrder { get; set; }

    [NopResourceDisplayName("Admin.ProductSupplies.Fields.MetaDescription")]
    public string MetaDescription { get; set; }

    [NopResourceDisplayName("Admin.ProductSupplies.Fields.MetaTitle")]
    public string MetaTitle { get; set; }

    [NopResourceDisplayName("Admin.ProductSupplies.Fields.SeName")]
    public string SeName { get; set; }

    [NopResourceDisplayName("Admin.ProductSupplies.Fields.PageSize")]
    public int PageSize { get; set; }

    [NopResourceDisplayName("Admin.ProductSupplies.Fields.AllowCustomersToSelectPageSize")]
    public bool AllowCustomersToSelectPageSize { get; set; }

    [NopResourceDisplayName("Admin.ProductSupplies.Fields.PageSizeOptions")]
    public string PageSizeOptions { get; set; }

    public List<ProductSupplierAttributeModel> ProductSupplierAttributes { get; set; }

    public IList<ProductSupplierLocalizedModel> Locales { get; set; }
    //vendor notes


    #endregion

    #region Nested classes

    public partial record ProductSupplierAttributeModel : BaseNopEntityModel
    {
        public ProductSupplierAttributeModel()
        {
            Values = new List<ProductSupplierAttributeValueModel>();
        }

        public string Name { get; set; }

        public bool IsRequired { get; set; }

        /// <summary>
        /// Default value for textboxes
        /// </summary>
        public string DefaultValue { get; set; }


        public AttributeControlType AttributeControlType { get; set; }

        public IList<ProductSupplierAttributeValueModel> Values { get; set; }
    }

    public partial record ProductSupplierAttributeValueModel : BaseNopEntityModel
    {
        public string Name { get; set; }

        public bool IsPreSelected { get; set; }
    }

    #endregion
}

public partial record ProductSupplierLocalizedModel : ILocalizedLocaleModel
{
    public int LanguageId { get; set; }

    [NopResourceDisplayName("Admin.ProductSupplies.Fields.Name")]
    public string Name { get; set; }

    [NopResourceDisplayName("Admin.ProductSupplies.Fields.Description")]
    public string Description { get; set; }

    [NopResourceDisplayName("Admin.ProductSupplies.Fields.MetaKeywords")]
    public string MetaKeywords { get; set; }

    [NopResourceDisplayName("Admin.ProductSupplies.Fields.MetaDescription")]
    public string MetaDescription { get; set; }

    [NopResourceDisplayName("Admin.ProductSupplies.Fields.MetaTitle")]
    public string MetaTitle { get; set; }

    [NopResourceDisplayName("Admin.ProductSupplies.Fields.SeName")]
    public string SeName { get; set; }
}