namespace FinancialProject.Common.Dtos;

/// <summary>新增與修改喜好金融商品共用的輸入資料。扣款帳號屬於使用者個人資料，不在此表單內變更。</summary>
public sealed class FavoriteProductInputDto
{
    public required string ProductName { get; init; }
    public required decimal Price { get; init; }
    public required decimal FeeRate { get; init; }
    public required int Qty { get; init; }
}
