using FinancialProject.Business.Options;
using FinancialProject.Common.Dtos;
using FinancialProject.Common.Exceptions;
using FinancialProject.Common.Interfaces.Repositories;
using FinancialProject.Common.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace FinancialProject.Business.Services;

public sealed class FavoriteProductService : IFavoriteProductService
{
    private readonly ILikeListRepository _likeListRepository;
    private readonly FavoriteProductValidationOptions _validationOptions;

    public FavoriteProductService(ILikeListRepository likeListRepository, IOptions<FavoriteProductValidationOptions> validationOptions)
    {
        _likeListRepository = likeListRepository;
        _validationOptions = validationOptions.Value;
    }

    public Task<IReadOnlyList<LikeListItemDto>> GetListAsync(string userId, CancellationToken ct = default) =>
        _likeListRepository.GetListByUserAsync(userId, ct);

    public async Task<LikeListItemDto> GetByIdAsync(int sn, string userId, CancellationToken ct = default) =>
        await _likeListRepository.GetByIdAsync(sn, userId, ct)
        ?? throw new NotFoundException($"找不到編號 {sn} 的喜好清單，或無權限存取");

    public Task<int> CreateAsync(string userId, FavoriteProductInputDto input, CancellationToken ct = default)
    {
        Validate(input);
        var (totalAmount, totalFee) = CalculateAmounts(input);
        return _likeListRepository.CreateAsync(userId, input, totalAmount, totalFee, ct);
    }

    public Task UpdateAsync(int sn, string userId, FavoriteProductInputDto input, CancellationToken ct = default)
    {
        Validate(input);
        var (totalAmount, totalFee) = CalculateAmounts(input);
        return _likeListRepository.UpdateAsync(sn, userId, input, totalAmount, totalFee, ct);
    }

    public Task DeleteAsync(int sn, string userId, CancellationToken ct = default) =>
        _likeListRepository.DeleteAsync(sn, userId, ct);

    private void Validate(FavoriteProductInputDto input)
    {
        if (input.Price < _validationOptions.MinPrice)
        {
            throw new ValidationException($"價格必須大於等於 {_validationOptions.MinPrice}");
        }

        if (input.FeeRate < _validationOptions.MinFeeRate || input.FeeRate > _validationOptions.MaxFeeRate)
        {
            throw new ValidationException($"手續費率必須介於 {_validationOptions.MinFeeRate} 到 {_validationOptions.MaxFeeRate} 之間");
        }

        if (input.Qty < _validationOptions.MinQty)
        {
            throw new ValidationException($"購買數量必須大於等於 {_validationOptions.MinQty}");
        }
    }

    private static (decimal TotalAmount, decimal TotalFee) CalculateAmounts(FavoriteProductInputDto input)
    {
        var totalAmount = Math.Round(input.Price * input.Qty, 2, MidpointRounding.AwayFromZero);
        var totalFee = Math.Round(totalAmount * input.FeeRate, 2, MidpointRounding.AwayFromZero);
        return (totalAmount, totalFee);
    }
}
