using ClientServiceRazor.Data;
using Features.Clients.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ClientServiceRazor.Tests;

public class ClientTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public void CreateClient_ShouldSaveAndRetrieveCorrectly()
    {
        using var context = GetDbContext();

        var client = new Client
        {
            Surname = "Smith",
            FirstName = "John",
            Patronymic = "Michael",
            Email = "john.smith@example.com",
            BirthDate = new DateOnly(1992, 8, 15)
        };

        context.Clients.Add(client);
        context.SaveChanges();

        var savedClient = context.Clients.Single();

        Assert.Equal(1, context.Clients.Count());
        Assert.Equal("Smith", savedClient.Surname);
        Assert.Equal("John", savedClient.FirstName);
        Assert.Equal("Michael", savedClient.Patronymic);
        Assert.Equal("john.smith@example.com", savedClient.Email);
        Assert.Equal(new DateOnly(1992, 8, 15), savedClient.BirthDate);
    }

    [Fact]
    public void GetAllClients_ShouldReturnAllSavedClients()
    {
        using var context = GetDbContext();

        context.Clients.Add(new Client { Surname = "Johnson", FirstName = "Emma", Email = "emma.johnson@example.com" });
        context.Clients.Add(new Client { Surname = "Williams", FirstName = "Liam", Email = "liam.williams@company.com" });
        context.Clients.Add(new Client { Surname = "Brown", FirstName = "Olivia", Email = "olivia.brown@test.org" });
        context.SaveChanges();

        var clients = context.Clients.ToList();

        Assert.Equal(3, clients.Count);
        Assert.Contains(clients, c => c.Surname == "Johnson");
        Assert.Contains(clients, c => c.Surname == "Williams");
        Assert.Contains(clients, c => c.Surname == "Brown");
    }

    [Fact]
    public void DeleteClient_ShouldRemoveFromDatabase()
    {
        using var context = GetDbContext();

        var client = new Client { Surname = "Test", FirstName = "User", Email = "test.user@example.com" };
        context.Clients.Add(client);
        context.SaveChanges();

        context.Clients.Remove(client);
        context.SaveChanges();

        Assert.Equal(0, context.Clients.Count());
        Assert.Null(context.Clients.FirstOrDefault());
    }

    [Fact]
    public void UpdateClient_ShouldChangeDataCorrectly()
    {
        using var context = GetDbContext();

        var client = new Client
        {
            Surname = "Davis",
            FirstName = "Sarah",
            Email = "sarah.davis@oldmail.com"
        };
        context.Clients.Add(client);
        context.SaveChanges();

        client.Surname = "Wilson";
        client.Email = "sarah.wilson@newmail.com";
        context.SaveChanges();

        var updated = context.Clients.Single();

        Assert.Equal("Wilson", updated.Surname);
        Assert.Equal("sarah.wilson@newmail.com", updated.Email);
        Assert.Equal("Sarah", updated.FirstName);
    }

    [Fact]
    public void ClientWithPhones_ShouldSaveAndLoadPhonesCorrectly()
    {
        using var context = GetDbContext();

        var client = new Client
        {
            Surname = "Taylor",
            FirstName = "James",
            Email = "james.taylor@company.com"
        };

        client.Phones.Add(new Phone { Number = "+12025550123" });
        client.Phones.Add(new Phone { Number = "+447911123456" });

        context.Clients.Add(client);
        context.SaveChanges();

        var loadedClient = context.Clients
            .Include(c => c.Phones)
            .Single();

        Assert.Equal(2, loadedClient.Phones.Count);
        Assert.Contains(loadedClient.Phones, p => p.Number == "+12025550123");
        Assert.Contains(loadedClient.Phones, p => p.Number == "+447911123456");

        Assert.All(loadedClient.Phones, p => Assert.Equal(loadedClient.Id, p.ClientId));
    }
}