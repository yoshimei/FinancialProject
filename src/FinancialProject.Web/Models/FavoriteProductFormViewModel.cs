using System.ComponentModel.DataAnnotations;

namespace FinancialProject.Web.Models;

/// <summary>新增／修改喜好金融商品共用的表單模型。</summary>
public sealed class FavoriteProductFormViewModel
{
    public int Sn { get; set; }

    [Required(ErrorMessage = "請輸入產品名稱")]
    [StringLength(100)]
    [Display(Name = "產品名稱")]
    public string ProductName { get; set; } = string.Empty;

    [Required(ErrorMessage = "請輸入價格")]
    [Range(0.01, 999999999, ErrorMessage = "價格必須大於 0")]
    [Display(Name = "價格")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "請輸入手續費率")]
    [Range(0, 1, ErrorMessage = "手續費率請輸入 0 ~ 1 之間的小數（例如 0.1 代表 10%）")]
    [Display(Name = "手續費率")]
    public decimal FeeRate { get; set; }

    [Required(ErrorMessage = "請輸入購買數量")]
    [Range(1, 999999, ErrorMessage = "購買數量至少為 1")]
    [Display(Name = "購買數量")]
    public int Qty { get; set; }
}
