using FinancialProject.Common.Exceptions;
using FinancialProject.Common.Interfaces.Services;
using FinancialProject.Web.Constants;
using FinancialProject.Web.Filters;
using FinancialProject.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialProject.Web.Controllers;

[ServiceFilter(typeof(SessionAuthorizeFilter))]
public sealed class FavoriteProductController : Controller
{
    private readonly IFavoriteProductService _favoriteProductService;
    private readonly IAuthService _authService;

    public FavoriteProductController(IFavoriteProductService favoriteProductService, IAuthService authService)
    {
        _favoriteProductService = favoriteProductService;
        _authService = authService;
    }

    private string CurrentUserId => HttpContext.Session.GetString(SessionKeys.UserId)!;

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var profile = await _authService.GetProfileAsync(CurrentUserId, ct);
        var items = await _favoriteProductService.GetListAsync(CurrentUserId, ct);

        var vm = new FavoriteProductIndexViewModel
        {
            UserName = profile!.UserName,
            Email = profile.Email,
            Account = profile.Account,
            Items = items.Select(i => i.ToListItemViewModel()).ToList(),
        };

        return View(vm);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new FavoriteProductFormViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(FavoriteProductFormViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await _favoriteProductService.CreateAsync(CurrentUserId, model.ToInputDto(), ct);
            return RedirectToAction(nameof(Index));
        }
        catch (ValidationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int sn, CancellationToken ct)
    {
        try
        {
            var item = await _favoriteProductService.GetByIdAsync(sn, CurrentUserId, ct);
            return View(item.ToFormViewModel());
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int sn, FavoriteProductFormViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await _favoriteProductService.UpdateAsync(sn, CurrentUserId, model.ToInputDto(), ct);
            return RedirectToAction(nameof(Index));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (ValidationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int sn, CancellationToken ct)
    {
        try
        {
            var item = await _favoriteProductService.GetByIdAsync(sn, CurrentUserId, ct);
            return View(item.ToDeleteViewModel());
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int sn, CancellationToken ct)
    {
        try
        {
            await _favoriteProductService.DeleteAsync(sn, CurrentUserId, ct);
            return RedirectToAction(nameof(Index));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
