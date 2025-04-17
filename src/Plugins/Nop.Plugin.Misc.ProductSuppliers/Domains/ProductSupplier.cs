using Nop.Core;
using System;
using System.ComponentModel.DataAnnotations;
namespace Nop.Plugin.Misc.ProductSuppliers.Domains;
public class ProductSupplier : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    public string Description { get; set; }

    public string Address { get; set; }

    public string Website { get; set; }
}
