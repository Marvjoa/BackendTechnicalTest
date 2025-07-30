using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalTest.Models;

public abstract class Keyed
{
    [Key]
    [Required]
    public Guid Id { get; init; } = Guid.NewGuid();
}