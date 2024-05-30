namespace APBD10.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Account
{
    [Key]
    public int AccountId { get; set; }

    [Required]
    public int RoleId { get; set; }

    [ForeignKey("RoleId")]
    public Role Role { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(80)]
    [EmailAddress]
    public string Email { get; set; }

    [MaxLength(9)]
    [Phone]
    public string Phone { get; set; }

    public ICollection<ShoppingCart> ShoppingCarts { get; set; }
}
