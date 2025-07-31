namespace BackendTechnicalTest.VIewModels.Transactions;

public record WithdrawViewModel
{
    public string AccountNumber { get; set; } = null!;
    public decimal Amount { get; set; }
    public string? Description { get; set; }
}