using System.ComponentModel.DataAnnotations;

namespace DOCOsoft.Api.Entities;

public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public required string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public required string LastName { get; set; }
    public DateTime Birthday { get; set; }

    [EmailAddress]
    [StringLength(50)]
    public required string Email { get; set; }
    public bool Active { get; set; } = true;
}
