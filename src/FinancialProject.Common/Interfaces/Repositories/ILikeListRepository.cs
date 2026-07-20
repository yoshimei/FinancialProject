using FinancialProject.Common.Dtos;
using FinancialProject.Common.Exceptions;

namespace FinancialProject.Common.Interfaces.Repositories;

public interface ILikeListRepository
{
    Task<IReadOnlyList<LikeListItemDto>> GetListByUserAsync(string userId, CancellationToken ct = default);

    Task<LikeListItemDto?> GetByIdAsync(int sn, string userId, CancellationToken ct = default);

    /// <returns>新建立喜好清單的流水序號(SN)。</returns>
    Task<int> CreateAsync(string userId, FavoriteProductInputDto input, decimal totalAmount, decimal totalFee, CancellationToken ct = default);

    /// <exception cref="NotFoundException">查無資料或不屬於該使用者。</exception>
    Task UpdateAsync(int sn, string userId, FavoriteProductInputDto input, decimal totalAmount, decimal totalFee, CancellationToken ct = default);

    /// <exception cref="NotFoundException">查無資料或不屬於該使用者。</exception>
    Task DeleteAsync(int sn, string userId, CancellationToken ct = default);
}
