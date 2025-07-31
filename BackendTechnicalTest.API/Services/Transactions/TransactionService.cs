using BackendTechnicalTest.API.Dto.Transactions;
using BackendTechnicalTest.API.Interfaces.Transactions;
using BackendTechnicalTest.Infrastructure.Context;
using BackendTechnicalTest.Models;
using BackendTechnicalTest.Models.Transactions;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalTest.API.Services.Transactions;

public class TransactionService : ITransactionService
{
    private readonly BankDbContext _context;

    public TransactionService(BankDbContext context)
    {
        _context = context;
    }

    public async Task<TransactionDto> DepositAsync(string accountNumber, decimal amount, string? description = null)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

        if (account == null)
            throw new Exception("Cuenta no encontrada");

        account.Balance += amount;

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            AccountId = account.Id,
            Amount = amount,
            Type = TransactionType.Deposit,
            Balance = account.Balance,
            Description = description ?? "Depósito"
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return new TransactionDto
        {
            Id = transaction.Id,
            AccountId = transaction.AccountId,
            AccountNumber = account.AccountNumber,
            Amount = transaction.Amount,
            Type = transaction.Type,
            Balance = transaction.Balance,
            Description = transaction.Description,
            Date = transaction.Date
        };
    }

    public async Task<TransactionDto> WithdrawAsync(string accountNumber, decimal amount, string? description = null)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

        if (account == null)
            throw new Exception("Cuenta no encontrada");

        if (account.Balance < amount)
            throw new Exception("Fondos insuficientes para realizar el retiro");

        account.Balance -= amount;

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            AccountId = account.Id,
            Amount = amount,
            Type = TransactionType.Withdrawal,
            Balance = account.Balance,
            Description = description ?? "Retiro"
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return new TransactionDto
        {
            Id = transaction.Id,
            AccountId = transaction.AccountId,
            AccountNumber = account.AccountNumber,
            Amount = transaction.Amount,
            Type = transaction.Type,
            Balance = transaction.Balance,
            Description = transaction.Description,
            Date = transaction.Date
        };
    }

    public async Task<IEnumerable<TransactionDto>> GetTransactionsAsync(string accountNumber)
    {
        var transactions = await _context.Transactions
            .Include(t => t.Account)
            .Where(t => t.Account.AccountNumber == accountNumber)
            .OrderByDescending(t => t.Date)
            .Select(t => new TransactionDto
            {
                Id = t.Id,
                AccountId = t.AccountId,
                AccountNumber = t.Account.AccountNumber,
                Amount = t.Amount,
                Type = t.Type,
                Balance = t.Balance,
                Description = t.Description,
                Date = t.Date
            })
            .ToListAsync();

        return transactions;
    }
}
