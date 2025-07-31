using BackendTechnicalTest.API.Dto.Clients;
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
    
    public async Task<Client> CreateClientAsync(ClientDto dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        
        var client = new Client
        {
            Name = dto.Name,
            Birthdate = dto.Birthdate,
            Gender = dto.Gender,
            Income = dto.Income
        };

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