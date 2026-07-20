using System.ComponentModel.DataAnnotations;

namespace FinancialProject.Web.Models;

public sealed class LoginViewModel
{
    [Required(ErrorMessage = "請輸入使用者ID")]
    [StringLength(20)]
    [Display(Name = "使用者ID")]
    public string UserID { get; set; } = string.Empty;
}
