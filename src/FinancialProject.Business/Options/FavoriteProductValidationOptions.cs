namespace FinancialProject.Business.Options;

/// <summary>喜好金融商品的商業規則驗證範圍，由設定檔（appsettings.json）綁定，不寫死於程式碼中。</summary>
public sealed class FavoriteProductValidationOptions
{
    public const string SectionName = "FavoriteProductValidation";

    public decimal MinPrice { get; init; } = 0m;
    public decimal MinFeeRate { get; init; } = 0m;
    public decimal MaxFeeRate { get; init; } = 1m;
    public int MinQty { get; init; } = 1;
}
