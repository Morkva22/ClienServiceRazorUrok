using System.ComponentModel.DataAnnotations;

namespace Features.Users.Models;

public class Status
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    public List<User> Users { get; set; } = new();
}