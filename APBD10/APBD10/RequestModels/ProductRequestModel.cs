using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD10.RequestModels;

public class ProductRequestModel
{
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
    public int [] CategoriesId { get; set; }
}