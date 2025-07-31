using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackendTechnicalTest.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalTest.Models.Transactions;

[Table("Transactions", Schema = "Transactions")]
public class Transaction(
    DateTime date,
    decimal amount,
    TransactionType type,
    decimal balance,
    string description,
    Guid accountId) : Keyed
{
    public Transaction() : this(DateTime.UtcNow, decimal.Zero, TransactionType.Deposit, decimal.Zero, string.Empty,
        Guid.Empty) { }

    [Required] public DateTime Date { get; set; } = date;

    [Required] public decimal Amount { get; set; } = amount;

    [Required] public TransactionType Type { get; set; } = type;

    [Required] [Precision(19, 4)] public decimal Balance { get; set; } = balance;

    public string Description { get; set; } = description;

    #region Foreign Keys

    [Required]
    [ForeignKey(nameof(AccountId))]
    public Guid AccountId { get; set; } = accountId;

    public Account Account { get; set; } = null!;

    #endregion
}