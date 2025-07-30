using BackendTechnicalTest.API.Interfaces.Clients;
using BackendTechnicalTest.Infrastructure.Context;
using BackendTechnicalTest.Models.Clients;

namespace BackendTechnicalTest.API.Services.Clients;

public class ClientService : IClientService
{
    private readonly BankDbContext _context;
    
    public ClientService(BankDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<Client> CreateClientAsync(Client client)
    {
        if (client == null) throw new ArgumentNullException(nameof(client));
        
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        
        return client;
    }

    public async Task<Client?> GetClientByIdAsync(Guid id)
    {
        if (id == Guid.Empty) throw new ArgumentException("El Id del cliente no puede estar vacio.", nameof(id));
        
        return await _context.Clients.FindAsync(id);
    }
}