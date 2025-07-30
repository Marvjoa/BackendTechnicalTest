using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackendTechnicalTest.Models.Clients;
using BackendTechnicalTest.Models.Transactions;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalTest.Models.Accounts;

public class Account: Keyed
{
    [Required]
    public string AccountNumber { get; set; } = null!;
    
    [Required]
    [Precision(19, 4)]
    public decimal Balance { get; set; }

    #region Foreign Keys

    [Required]
    [ForeignKey(nameof(ClientId))]
    public Guid ClientId { get; set; }
    
    public Client Client { get; set; } = null!;

    public ICollection< Transaction> Transactions { get; set; } = new List<Transaction>();
    #endregion

}