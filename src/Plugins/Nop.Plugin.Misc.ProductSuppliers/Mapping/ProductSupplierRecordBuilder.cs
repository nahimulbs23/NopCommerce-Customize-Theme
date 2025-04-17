using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Data.Mapping.Builders;
using Nop.Data.Extensions;
using System.Data;
using Nop.Plugin.Misc.ProductSuppliers.Domains;

namespace Nop.Plugin.Misc.ProductSuppliers.Mapping;

public class ProductSupplierRecordBuilder : NopEntityBuilder<ProductSupplier>
{
    /// <summary>
    /// Apply entity configuration
    /// </summary>
    /// <param name="table">Create table expression builder</param>
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        //map the primary key (not necessary if it is Id field)
        table.WithColumn(nameof(ProductSupplier.Id)).AsInt32().PrimaryKey().Identity()
        //map the additional properties as foreign keys
        //.WithColumn(nameof(ProductSupplier.Name)).AsInt32().ForeignKey<Product>(onDelete: Rule.Cascade)
        //.WithColumn(nameof(ProductSupplier.CustomerId)).AsInt32().ForeignKey<Customer>(onDelete: Rule.Cascade)
        ////avoiding truncation/failure
        //so we set the same max length used in the product name
        .WithColumn(nameof(ProductSupplier.Email)).AsString().NotNullable()
        .WithColumn(nameof(ProductSupplier.Website)).AsString().Nullable()
        .WithColumn(nameof(ProductSupplier.Name)).AsString().NotNullable()
        //not necessary if we don't specify any rules
        .WithColumn(nameof(ProductSupplier.Address)).AsString().Nullable()
        .WithColumn(nameof(ProductSupplier.Phone)).AsInt32().NotNullable()
        .WithColumn(nameof(ProductSupplier.Description)).AsString().NotNullable();
    }
}