using System.ComponentModel.DataAnnotations.Schema;

namespace APBD10.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [Column(TypeName = "decimal(5,2)")]
    public decimal Weight { get; set; }

    [Required]
    [Column(TypeName = "decimal(5,2)")]
    public decimal Width { get; set; }

    [Required]
    [Column(TypeName = "decimal(5,2)")]
    public decimal Height { get; set; }

    [Required]
    [Column(TypeName = "decimal(5,2)")]
    public decimal Depth { get; set; }

    public ICollection<ProductCategories> ProductsCategories { get; set; }
    public ICollection<ShoppingCart> ShoppingCarts { get; set; }
}
