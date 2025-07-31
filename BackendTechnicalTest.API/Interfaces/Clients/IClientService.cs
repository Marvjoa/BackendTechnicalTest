using BackendTechnicalTest.API.Dto.Clients;
using BackendTechnicalTest.Models.Clients;

namespace BackendTechnicalTest.API.Interfaces.Clients;

public interface IClientService
{
    Task<Client> CreateClientAsync(ClientDto dto);
    Task<Client?> GetClientByIdAsync(Guid id);
}