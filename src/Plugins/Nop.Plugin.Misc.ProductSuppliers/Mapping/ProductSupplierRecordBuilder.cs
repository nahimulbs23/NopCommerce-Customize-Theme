using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.ProductSuppliers.Domains;

namespace Nop.Plugin.Misc.ProductSuppliers.Mapping
{
    public class ProductSupplierRecordBuilder : NopEntityBuilder<ProductSupplierEntity>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(ProductSupplierEntity.Id)).AsInt32().PrimaryKey().Identity()
                .WithColumn(nameof(ProductSupplierEntity.Name)).AsString(255).NotNullable()
                .WithColumn(nameof(ProductSupplierEntity.Email)).AsString(255).NotNullable()
                .WithColumn(nameof(ProductSupplierEntity.Description)).AsString(int.MaxValue).Nullable()
                .WithColumn(nameof(ProductSupplierEntity.Phone)).AsString(50).Nullable()
                .WithColumn(nameof(ProductSupplierEntity.Address)).AsString(500).Nullable()
                .WithColumn(nameof(ProductSupplierEntity.AdminComment)).AsString(int.MaxValue).Nullable()
                .WithColumn(nameof(ProductSupplierEntity.Active)).AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn(nameof(ProductSupplierEntity.DisplayOrder)).AsInt32().NotNullable().WithDefaultValue(0)
                .WithColumn(nameof(ProductSupplierEntity.MetaDescription)).AsString(4000).Nullable()
                .WithColumn(nameof(ProductSupplierEntity.MetaTitle)).AsString(400).Nullable()
                .WithColumn(nameof(ProductSupplierEntity.SeName)).AsString(255).Nullable()
                .WithColumn(nameof(ProductSupplierEntity.PageSize)).AsInt32().NotNullable().WithDefaultValue(10)
                .WithColumn(nameof(ProductSupplierEntity.AllowCustomersToSelectPageSize)).AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn(nameof(ProductSupplierEntity.PageSizeOptions)).AsString(200).Nullable();
        }
    }
}
