using BackendTechnicalTest.API.Interfaces.Accounts;
using BackendTechnicalTest.Infrastructure.Context;
using BackendTechnicalTest.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalTest.API.Services.Accounts;

public class AccountService : IAccountService
{
    private readonly BankDbContext _context;

    public AccountService(BankDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Account> CreateAccountAsync(Account account)
    {
        if (account == null)
        {
            throw new ArgumentNullException(nameof(account));
        }

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
        return account;
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