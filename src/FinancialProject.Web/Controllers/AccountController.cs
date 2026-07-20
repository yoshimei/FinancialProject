using FinancialProject.Common.Interfaces.Services;
using FinancialProject.Web.Constants;
using FinancialProject.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialProject.Web.Controllers;

public sealed class AccountController : Controller
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (!string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeys.UserId)))
        {
            return RedirectToAction("Index", "FavoriteProduct");
        }

        return View(new LoginViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _authService.ValidateLoginAsync(model.UserID, ct);
        if (user is null)
        {
            ModelState.AddModelError(nameof(model.UserID), "使用者不存在，請確認輸入的使用者ID");
            return View(model);
        }

        HttpContext.Session.SetString(SessionKeys.UserId, user.UserID);
        return RedirectToAction("Index", "FavoriteProduct");
    }

    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Login));
    }
}
