using BackendTechnicalTest.API.Dto.Transactions;

namespace BackendTechnicalTest.API.Interfaces.Transactions;

public interface ITransactionService
{
    Task<TransactionDto> DepositAsync(string accountNumber, decimal amount, string? description = null);
    Task<TransactionDto> WithdrawAsync(string accountNumber, decimal amount, string? description = null);
    Task<IEnumerable<TransactionDto>> GetTransactionsAsync(string accountNumber);

}