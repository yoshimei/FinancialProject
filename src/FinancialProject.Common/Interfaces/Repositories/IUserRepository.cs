using FinancialProject.Common.Dtos;

namespace FinancialProject.Common.Interfaces.Repositories;

public interface IUserRepository
{
    /// <summary>登入時驗證 UserID 是否存在。</summary>
    Task<UserDto?> ValidateLoginAsync(string userId, CancellationToken ct = default);

    /// <summary>取得目前使用者資料（如表單預帶扣款帳號、頁面顯示使用者資訊）。</summary>
    Task<UserDto?> GetProfileAsync(string userId, CancellationToken ct = default);
}
