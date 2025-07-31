using System.ComponentModel.DataAnnotations;
using BackendTechnicalTest.Models;

namespace BackendTechnicalTest.VIewModels.Clients;

public record CreateClientViewModel
{
    [Required(ErrorMessage = "El nombr es requerido")]
    [StringLength(128, ErrorMessage = "El nombre no puede tener mas de 128 caracteres.")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
    public DateTimeOffset BirthDate { get; set; }
    
    [Required(ErrorMessage = "El género es requerido")]
    public Gender Gender { get; set; }
    
    [Required(ErrorMessage = "El ingreso es requerido")]
    public decimal Income { get; set; }
}