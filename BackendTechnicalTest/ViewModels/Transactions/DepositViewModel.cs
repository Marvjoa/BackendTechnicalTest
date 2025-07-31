namespace BackendTechnicalTest.VIewModels.Transactions;

public record DepositViewModel
{
    public string AccountNumber { get; set; } = null!;
    public decimal Amount { get; set; }
    public string? Description { get; set; }
}