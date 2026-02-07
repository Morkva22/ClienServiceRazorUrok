using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Features.Users.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Login { get; set; } = null!;

    [Required]
    [MaxLength(255)]  
    public string Password { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = null!;

    [Column(TypeName = "timestamptz")]
    public DateTime CreatedAt { get; set; }

    [Column(TypeName = "timestamptz")]
    public DateTime? UpdatedAt { get; set; }

    public int? StatusId { get; set; }
    public Status? Status { get; set; }

    public int? RoleId { get; set; }
    public Role? Role { get; set; }
}