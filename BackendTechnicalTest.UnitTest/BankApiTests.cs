using BackendTechnicalTest.API.Dto.Transactions;
using BackendTechnicalTest.API.Interfaces.Transactions;
using BackendTechnicalTest.Models;
using Moq;
using Xunit;

namespace BackendTechnicalTest.UnitTest;

public class BankApiTests
{
    [Fact]
    public async Task Deposito_DebeIncrementarSaldo()
    {
        var mockService = new Mock<ITransactionService>();

        var dto = new TransactionDto
        {
            Id = Guid.NewGuid(),
            AccountId = Guid.NewGuid(),
            AccountNumber = "123-456",
            Amount = 500,
            Type = TransactionType.Deposit,
            Balance = 1500,
            Description = "Depósito test",
            Date = DateTime.UtcNow
        };

        mockService.Setup(s => s.DepositAsync("123-456", 500, null)).ReturnsAsync(dto);

        var result = await mockService.Object.DepositAsync("123-456", 500);

        Assert.Equal(1500, result.Balance);
        Assert.Equal(TransactionType.Deposit, result.Type);
        Assert.Equal("123-456", result.AccountNumber);
    }

    [Fact]
    public async Task Retiro_DebeDisminuirSaldo()
    {
        var mockService = new Mock<ITransactionService>();

        var dto = new TransactionDto
        {
            Id = Guid.NewGuid(),
            AccountId = Guid.NewGuid(),
            AccountNumber = "123-456",
            Amount = 200,
            Type = TransactionType.Withdrawal,
            Balance = 800,
            Description = "Retiro test",
            Date = DateTime.UtcNow
        };

        mockService.Setup(s => s.WithdrawAsync("123-456", 200, null)).ReturnsAsync(dto);

        var result = await mockService.Object.WithdrawAsync("123-456", 200);

        Assert.Equal(800, result.Balance);
        Assert.Equal(TransactionType.Withdrawal, result.Type);
        Assert.Equal("123-456", result.AccountNumber);
    }

    [Fact]
    public async Task Retiro_SinFondos_DebeLanzarExcepcion()
    {
        var mockService = new Mock<ITransactionService>();

        mockService
            .Setup(s => s.WithdrawAsync("123-456", 2000, null))
            .ThrowsAsync(new Exception("Fondos insuficientes para realizar el retiro"));

        await Assert.ThrowsAsync<Exception>(() => mockService.Object.WithdrawAsync("123-456", 2000));
    }

    [Fact]
    public async Task ConsultarHistorialTransacciones_DebeRetornarTransacciones()
    {
        var mockService = new Mock<ITransactionService>();

        var lista = new List<TransactionDto>
        {
            new TransactionDto
            {
                Id = Guid.NewGuid(),
                AccountId = Guid.NewGuid(),
                AccountNumber = "123-456",
                Amount = 500,
                Type = TransactionType.Deposit,
                Balance = 1500,
                Description = "Depósito",
                Date = DateTime.UtcNow
            },
            new TransactionDto
            {
                Id = Guid.NewGuid(),
                AccountId = Guid.NewGuid(),
                AccountNumber = "123-456",
                Amount = 200,
                Type = TransactionType.Withdrawal,
                Balance = 1300,
                Description = "Retiro",
                Date = DateTime.UtcNow
            }
        };

        mockService.Setup(s => s.GetTransactionsAsync("123-456")).ReturnsAsync(lista);

        var result = await mockService.Object.GetTransactionsAsync("123-456");

        Assert.Equal(2, result.Count());
        Assert.Contains(result, t => t.Type == TransactionType.Deposit);
        Assert.Contains(result, t => t.Type == TransactionType.Withdrawal);
    }
}
