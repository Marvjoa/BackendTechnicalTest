using BackendTechnicalTest.API.Dto.Accounts;
using BackendTechnicalTest.API.Interfaces.Accounts;
using BackendTechnicalTest.Infrastructure.Context;
using BackendTechnicalTest.Models;
using BackendTechnicalTest.Models.Accounts;
using BackendTechnicalTest.Models.Transactions;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalTest.API.Services.Accounts;

public class AccountService : IAccountService
{
    private readonly BankDbContext _context;

    public AccountService(BankDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Account> CreateAccountAsync(AccountDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        var account = new Account
        {
            AccountNumber = dto.AccountNumber,
            Balance = dto.Balance
        };
        
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
        return account;
    }

    public async Task ApplyInterestAsync(string accountNumber, decimal ratePercentage)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

        if (account == null)
            throw new Exception("Cuenta no encontrada.");
        
        var interestAmount = account.Balance * (ratePercentage / 100);
        
        account.Balance += interestAmount;
        var transaction = new Transaction
        {
            AccountId = account.Id,
            Amount = interestAmount,
            Type = TransactionType.Deposit, 
            Balance = account.Balance,
            Description = $"Interés aplicado ({ratePercentage}%)",
            Date = DateTime.UtcNow
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
    }

    
    public async Task<decimal?> GetBalanceAsync(string accountNumber)
    {
        
        if (string.IsNullOrWhiteSpace(accountNumber))
        {
            throw new ArgumentException("El numero de cuenta no puede ser nulo o vacio", nameof(accountNumber));
        }

        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

        return account?.Balance;
    }
}