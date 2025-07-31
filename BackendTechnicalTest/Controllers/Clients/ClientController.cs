using AutoMapper;
using BackendTechnicalTest.API.Dto.Clients;
using BackendTechnicalTest.API.Interfaces.Clients;
using BackendTechnicalTest.VIewModels.Clients;
using Microsoft.AspNetCore.Mvc;

namespace BackendTechnicalTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly IMapper _mapper;

    public ClientController(IClientService clientService, IMapper mapper)
    {
        _clientService = clientService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] CreateClientViewModel viewModel)
    {
        if (viewModel == null)
        {
            return BadRequest("Client data is required.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var dto = _mapper.Map<ClientDto>(viewModel);
        var result = await _clientService.CreateClientAsync(dto);
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetClient(Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest("Client ID is required.");
        }

        var client = await _clientService.GetClientByIdAsync(id);
        if (client == null)
        {
            return NotFound("Cliente no encontrado.");
        }

        return Ok(client);
    }
}