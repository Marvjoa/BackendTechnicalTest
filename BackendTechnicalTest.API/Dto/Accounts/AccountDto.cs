// AccountDto actualizado
using System;
using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalTest.API.Dto.Accounts;

public class AccountDto
{
    [Required]
    public Guid ClientId { get; set; }

    [Required]
    public string AccountNumber { get; set; } = null!;

    [Range(0, double.MaxValue)]
    public decimal Balance { get; set; }
}