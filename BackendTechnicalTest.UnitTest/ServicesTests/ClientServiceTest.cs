using BackendTechnicalTest.API.Dto.Clients;
using BackendTechnicalTest.API.Services.Clients;
using BackendTechnicalTest.Infrastructure.Context;
using BackendTechnicalTest.Models;
using BackendTechnicalTest.Models.Clients;
using Microsoft.EntityFrameworkCore;
using Xunit;


public class ClientServiceTests
{
    [Fact]
    public async Task CrearCliente_DebeCrearClienteCorrectamente()
    {
        var options = new DbContextOptionsBuilder<BankDbContext>()
            .UseInMemoryDatabase("TestDb_Clients")
            .Options;

        await using var context = new BankDbContext(options);

        var service = new ClientService(context);

        var dto = new ClientDto
        {
            Name = "Juan Pérez",
            Birthdate = new DateTime(1990, 5, 20),
            Gender = Gender.Masculino,
            Income = 1500
        };

        var client = await service.CreateClientAsync(dto);

        Assert.NotNull(client);
        Assert.Equal(dto.Name, client.Name);
        Assert.Equal(dto.Birthdate, client.Birthdate);
        Assert.Equal(dto.Gender, client.Gender);
        Assert.Equal(dto.Income, client.Income);
    }
}