namespace Features.Clients.Models;

public class ClientFinanceAccount
{
    public int ClientId { get; set; }
    public Client? Client { get; set; }

    public int FinanceAccountId { get; set; }
    public FinanceAccount? FinanceAccount { get; set; }
}