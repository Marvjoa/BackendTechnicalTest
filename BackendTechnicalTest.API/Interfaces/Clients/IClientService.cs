using BackendTechnicalTest.Models.Clients;

namespace BackendTechnicalTest.API.Interfaces.Clients;

public interface IClientService
{
    Task<Client> CreateClientAsync(Client client);
    Task<Client?> GetClientByIdAsync(Guid id);
}