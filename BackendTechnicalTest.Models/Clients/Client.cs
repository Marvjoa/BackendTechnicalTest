using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackendTechnicalTest.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalTest.Models.Clients;

[Table( "Clients", Schema = "Clients" )]
public class Client(string name, DateTimeOffset birthdate, Gender gender, decimal income ) : Keyed
{
    
    public Client() : this(string.Empty, new DateTimeOffset(), Gender.Masculino, decimal.Zero) { }

    [Required] 
    [MaxLength(128)] 
    public string Name { get; init; } = name;

    [Required] 
    public DateTimeOffset Birthdate { get; init; } = birthdate;

    [Required] 
    public Gender Gender { get; init; } = gender;

    [Required] 
    [Precision(19, 4)]
    public decimal Income { get; init; } = income;

    #region Foreign Keys

    public ICollection<Account> Accounts { get; init; } = new List<Account>();

    #endregion
}