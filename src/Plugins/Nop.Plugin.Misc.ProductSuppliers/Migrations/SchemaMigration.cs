using FluentMigrator;
using Microsoft.IdentityModel.Tokens;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.ProductSuppliers.Domains;

namespace Nop.Plugin.Misc.ProductSuppliers.Migrations;

[NopSchemaMigration("2025/03/27 08:40:55:1687541", "Misc.ProductSuppliers base schema", MigrationProcessType.Installation)]
public class SchemaMigration : ForwardOnlyMigration
{
    public override void Up()
    {
        //create table

        var productSupplier = nameof(ProductSupplier);
        if (!Schema.Table(productSupplier).Exists())
        {
          Create.TableFor<ProductSupplier>();
        }
        if (!Schema.Table(productSupplier).Column(nameof(ProductSupplier.Description)).Exists())
            Alter.Table(productSupplier)
                .AddColumn(nameof(ProductSupplier.Description)).AsString(255).Nullable();

    }

}