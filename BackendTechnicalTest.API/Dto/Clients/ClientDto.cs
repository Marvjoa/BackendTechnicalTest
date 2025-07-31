using BackendTechnicalTest.Models;

namespace BackendTechnicalTest.API.Dto.Clients;

public class ClientDto
{
    public string Name { get; set; } = null!;
    public DateTimeOffset Birthdate { get; set; }
    public Gender Gender { get; set; }
    public decimal Income { get; set; }
}