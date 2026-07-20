using FinancialProject.Common.Dtos;
using FinancialProject.Common.Exceptions;

namespace FinancialProject.Common.Interfaces.Services;

public interface IFavoriteProductService
{
    Task<IReadOnlyList<LikeListItemDto>> GetListAsync(string userId, CancellationToken ct = default);

    /// <exception cref="NotFoundException">查無資料或不屬於該使用者。</exception>
    Task<LikeListItemDto> GetByIdAsync(int sn, string userId, CancellationToken ct = default);

    /// <exception cref="ValidationException">輸入資料違反商業規則。</exception>
    Task<int> CreateAsync(string userId, FavoriteProductInputDto input, CancellationToken ct = default);

    /// <exception cref="ValidationException">輸入資料違反商業規則。</exception>
    /// <exception cref="NotFoundException">查無資料或不屬於該使用者。</exception>
    Task UpdateAsync(int sn, string userId, FavoriteProductInputDto input, CancellationToken ct = default);

    /// <exception cref="NotFoundException">查無資料或不屬於該使用者。</exception>
    Task DeleteAsync(int sn, string userId, CancellationToken ct = default);
}
