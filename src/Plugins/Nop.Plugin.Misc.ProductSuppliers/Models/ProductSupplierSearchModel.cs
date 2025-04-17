using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Misc.ProductSuppliers.Domains;

namespace Nop.Plugin.Misc.ProductSuppliers.Models;
public class ProductSupplierSearchModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public IList<ProductSupplier> Suppliers { get; set; } = new List<ProductSupplier>();
}
