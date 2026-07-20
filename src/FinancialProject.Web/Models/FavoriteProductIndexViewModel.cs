namespace FinancialProject.Web.Models;

/// <summary>查詢清單頁面：使用者資訊只顯示一次（頁首資訊卡），下方列出各筆喜好商品明細。</summary>
public sealed class FavoriteProductIndexViewModel
{
    public required string UserName { get; init; }
    public required string Email { get; init; }
    public required string Account { get; init; }
    public required IReadOnlyList<FavoriteProductListItemViewModel> Items { get; init; }
}
