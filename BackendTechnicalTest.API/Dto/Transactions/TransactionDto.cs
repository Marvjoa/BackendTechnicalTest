using BackendTechnicalTest.Models;

namespace BackendTechnicalTest.API.Dto.Transactions;

public class TransactionDto
{
    public Guid Id { get; set; }          
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public decimal Balance { get; set; }
    public string Description { get; set; } = string.Empty;

    public Guid AccountId { get; set; }  
    
    public string? AccountNumber { get; set; } 
}