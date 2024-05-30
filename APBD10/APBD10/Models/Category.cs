namespace APBD10.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public ICollection<ProductCategories> ProductsCategories { get; set; }
}
