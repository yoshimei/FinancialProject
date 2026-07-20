using FinancialProject.Common.Dtos;

namespace FinancialProject.Common.Interfaces.Services;

public interface IAuthService
{
    /// <returns>UserID 存在時回傳其使用者資料，否則回傳 null。</returns>
    Task<UserDto?> ValidateLoginAsync(string userId, CancellationToken ct = default);

    /// <returns>目前使用者的資料（如表單預帶扣款帳號、頁面顯示使用者資訊），UserID 不存在時回傳 null。</returns>
    Task<UserDto?> GetProfileAsync(string userId, CancellationToken ct = default);
}
