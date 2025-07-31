using BackendTechnicalTest.Infrastructure.Context;
using BackendTechnicalTest.Models.Accounts;
using BackendTechnicalTest.API.Services.Transactions;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class TransactionServiceTests
{
    [Fact]
    public async Task Deposito_DebeIncrementarSaldo()
    {
        var options = new DbContextOptionsBuilder<BankDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // BD única
            .Options;

        await using var context = new BankDbContext(options);

        var account = new Account { AccountNumber = "123-456", Balance = 500, ClientId = Guid.NewGuid() };
        context.Accounts.Add(account);
        await context.SaveChangesAsync();

        var service = new TransactionService(context);

        var tx = await service.DepositAsync("123-456", 200);

        Assert.Equal(700, tx.Balance);
    }

    [Fact]
    public async Task Retiro_DebeDisminuirSaldo()
    {
        var options = new DbContextOptionsBuilder<BankDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // BD única
            .Options;

        await using var context = new BankDbContext(options);

        var account = new Account { AccountNumber = "123-456", Balance = 500, ClientId = Guid.NewGuid() };
        context.Accounts.Add(account);
        await context.SaveChangesAsync();

        var service = new TransactionService(context);

        var tx = await service.WithdrawAsync("123-456", 200);

        Assert.Equal(300, tx.Balance);
    }

    [Fact]
    public async Task Retiro_SinFondos_DebeLanzarExcepcion()
    {
        var options = new DbContextOptionsBuilder<BankDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // BD única
            .Options;

        await using var context = new BankDbContext(options);

        var account = new Account { AccountNumber = "123-456", Balance = 100, ClientId = Guid.NewGuid() };
        context.Accounts.Add(account);
        await context.SaveChangesAsync();

        var service = new TransactionService(context);

        var ex = await Assert.ThrowsAsync<Exception>(() => service.WithdrawAsync("123-456", 200));

        Assert.Equal("Fondos insuficientes para realizar el retiro", ex.Message);
    }
}
