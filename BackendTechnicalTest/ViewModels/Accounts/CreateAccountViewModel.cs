using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalTest.VIewModels.Accounts;

public record CreateAccountViewModel
{
    [Required(ErrorMessage = "El id  del cliente es requerido.")]
    public Guid ClientId { get; set; }
    
    [Required(ErrorMessage = "El balance inicial es requerido.")]
    public decimal InitialBalance { get; set; }
}