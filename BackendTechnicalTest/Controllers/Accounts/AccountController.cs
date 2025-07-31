using AutoMapper;
using BackendTechnicalTest.API.Dto.Accounts;
using BackendTechnicalTest.API.Interfaces.Accounts;
using BackendTechnicalTest.VIewModels.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace BackendTechnicalTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public AccountsController(IAccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountViewModel viewModel)
    {
        if (viewModel == null)
            return BadRequest("Datos de cuenta requeridos.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var dto = _mapper.Map<AccountDto>(viewModel);
        dto.AccountNumber = Guid.NewGuid().ToString();
        dto.Balance = viewModel.InitialBalance;
        var result = await _accountService.CreateAccountAsync(dto);
        return Ok(result);
    }

    [HttpGet("{accountNumber}/balance")]
    public async Task<IActionResult> GetBalance(string accountNumber)
    {
        var balance = await _accountService.GetBalanceAsync(accountNumber);
        if (balance == null)
            return NotFound("Cuenta no encontrada.");

        return Ok(balance);
    }
}