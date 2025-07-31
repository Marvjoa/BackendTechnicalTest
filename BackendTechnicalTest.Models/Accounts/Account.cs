using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackendTechnicalTest.Models.Clients;
using BackendTechnicalTest.Models.Transactions;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalTest.Models.Accounts;

[Table("Accounts", Schema = "Accounts")]
public class Account(string accountNumber, decimal balance, Guid clientId) : Keyed
{
    public Account() : this(string.Empty, decimal.Zero, Guid.Empty) { }

    [Required]
    public string AccountNumber { get; set; } = accountNumber;

    [Required]
    [Precision(19, 4)]
    public decimal Balance { get; set; } = balance;

    #region Foreign Keys

    [Required]
    [ForeignKey(nameof(ClientId))]
    public Guid ClientId { get; set; } = clientId;

    public Client Client { get; set; } = null!;

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    #endregion
  
}