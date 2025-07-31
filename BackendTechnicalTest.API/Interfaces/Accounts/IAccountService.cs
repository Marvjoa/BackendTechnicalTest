using BackendTechnicalTest.API.Dto.Accounts;
using BackendTechnicalTest.Models.Accounts;

namespace BackendTechnicalTest.API.Interfaces.Accounts;

public interface IAccountService
{
    Task<Account> CreateAccountAsync(AccountDto dto);
    
    Task ApplyInterestAsync(string accountNumber, decimal ratePercentage);
    
    Task<decimal?> GetBalanceAsync(string accountNumber);
}