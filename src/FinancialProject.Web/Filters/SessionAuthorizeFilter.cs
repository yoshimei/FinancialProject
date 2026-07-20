using FinancialProject.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinancialProject.Web.Filters;

/// <summary>檢查 Session 內是否有已登入的 UserID，未登入則導回登入頁。</summary>
public sealed class SessionAuthorizeFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var userId = context.HttpContext.Session.GetString(SessionKeys.UserId);
        if (string.IsNullOrEmpty(userId))
        {
            context.Result = new RedirectToActionResult("Login", "Account", routeValues: null);
            return;
        }

        await next();
    }
}
