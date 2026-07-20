namespace FinancialProject.Common.Dtos;

public sealed class LikeListItemDto
{
    public required int Sn { get; init; }
    public required int ProductNo { get; init; }
    public required string ProductName { get; init; }
    public required decimal Price { get; init; }
    public required decimal FeeRate { get; init; }
    public required int Qty { get; init; }
    public required decimal TotalAmount { get; init; }
    public required decimal TotalFee { get; init; }
    public required string Account { get; init; }
    public required string Email { get; init; }
}
