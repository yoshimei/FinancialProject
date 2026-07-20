using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FinancialProject.Web.Models;

namespace FinancialProject.Web.Controllers;

/// <summary>僅保留全域錯誤處理頁面；一般功能入口為 AccountController/FavoriteProductController。</summary>
public sealed class HomeController : Controller
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
