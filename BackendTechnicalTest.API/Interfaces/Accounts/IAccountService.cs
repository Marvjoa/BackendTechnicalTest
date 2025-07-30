using BackendTechnicalTest.Models.Accounts;

namespace BackendTechnicalTest.API.Interfaces.Accounts;

public interface IAccountService
{
    Task<Account> CreateAccountAsync(Account account);
    Task<decimal?> GetBalanceAsync(string accountNumber);
}