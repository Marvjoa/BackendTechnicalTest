// TransactionsController.cs

using BackendTechnicalTest.API.Interfaces.Transactions;
using BackendTechnicalTest.VIewModels.Transactions;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit([FromBody] DepositViewModel vm)
    {
        var result = await _transactionService.DepositAsync(vm.AccountNumber, vm.Amount, vm.Description);
        return Ok(result);
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] WithdrawViewModel vm)
    {
        var result = await _transactionService.WithdrawAsync(vm.AccountNumber, vm.Amount, vm.Description);
        return Ok(result);
    }

    [HttpGet("{accountNumber}/history")]
    public async Task<IActionResult> GetHistory(string accountNumber)
    {
        var result = await _transactionService.GetTransactionsAsync(accountNumber);
        return Ok(result);
    }
}