namespace APBD10.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class ProductCategories
{
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
