namespace Features.Clients.Models;

public class Phone
{
    public int Id { get; set; }
    public string Number { get; set; } = null!;
    
    public int ClientId { get; set; }              
    
    public Client Client { get; set; } = null!;     

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}