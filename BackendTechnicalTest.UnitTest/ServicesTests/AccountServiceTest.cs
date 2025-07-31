using BackendTechnicalTest.Infrastructure.Context;
using BackendTechnicalTest.Models.Accounts;
using BackendTechnicalTest.API.Services.Accounts;
using BackendTechnicalTest.API.Dto.Accounts;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class AccountServiceTests
{
    [Fact]
    public async Task CrearCuentaBancaria_DebeCrearCuentaCorrectamente()
    {
        var options = new DbContextOptionsBuilder<BankDbContext>()
            .UseInMemoryDatabase("TestDb_Accounts")
            .Options;

        await using var context = new BankDbContext(options);

        var service = new AccountService(context);

        var dto = new AccountDto
        {
            ClientId = Guid.NewGuid(),
            AccountNumber = "123-456",
            Balance = 1000
        };

        var result = await service.CreateAccountAsync(dto);

        Assert.NotNull(result);
        Assert.Equal(dto.AccountNumber, result.AccountNumber);
        Assert.Equal(dto.Balance, result.Balance);
    }

    [Fact]
    public async Task ConsultarSaldo_DebeRetornarSaldoCorrecto()
    {
        var options = new DbContextOptionsBuilder<BankDbContext>()
            .UseInMemoryDatabase("TestDb_Accounts")
            .Options;

        await using var context = new BankDbContext(options);

        var account = new Account
        {
            AccountNumber = "123-456",
            Balance = 1500,
            ClientId = Guid.NewGuid()
        };

        context.Accounts.Add(account);
        await context.SaveChangesAsync();

        var service = new AccountService(context);

        var balance = await service.GetBalanceAsync("123-456");

        Assert.Equal(1500, balance);
    }
    
    [Fact]
    public async Task AplicarInteres_DebeActualizarSaldo()
    {
        var options = new DbContextOptionsBuilder<BankDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Base limpia SIEMPRE
            .Options;

        await using var context = new BankDbContext(options);

        var account = new Account
        {
            AccountNumber = "123456",
            Balance = 1000,
            ClientId = Guid.NewGuid()
        };

        context.Accounts.Add(account);
        await context.SaveChangesAsync();

        var service = new AccountService(context);

        await service.ApplyInterestAsync("123456", 5); // Usa 5%

        var updated = await context.Accounts.FirstAsync();
        Assert.Equal(1050, updated.Balance); // 5% de 1000
    }

}