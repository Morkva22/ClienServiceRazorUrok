namespace Features.Clients.Models;

public class Client
{
    public int Id { get; set; }
    public string Surname { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? Patronymic { get; set; }
    public string Email { get; set; } = null!;
    public DateOnly? BirthDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Address? Address { get; set; }
    public List<Phone> Phones { get; set; } = new();
}