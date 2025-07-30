using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackendTechnicalTest.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalTest.Models.Transactions;

public class Transaction : Keyed
{
    [Required]
    public DateTime Date { get; set; } = DateTime.UtcNow;
    
    [Required]
    public decimal Amount { get; set; }
    
    [Required]
    public TransactionType Type { get; set; }
    
    [Required]
    [Precision(19, 4)]
    public decimal Balance { get; set; }

    public string Description { get; set; } = null!;

    #region Foreign Keys

    [Required]
    [ForeignKey(nameof(AccountId))]
    public Guid AccountId { get; set; }

    public Account Account { get; set; } = null!;   
    
    #endregion
}