using FinancialProject.Common.Dtos;

namespace FinancialProject.Web.Models;

internal static class FavoriteProductMappingExtensions
{
    public static FavoriteProductListItemViewModel ToListItemViewModel(this LikeListItemDto dto) => new()
    {
        Sn = dto.Sn,
        ProductName = dto.ProductName,
        Price = dto.Price,
        FeeRate = dto.FeeRate,
        Qty = dto.Qty,
        TotalAmount = dto.TotalAmount,
        TotalFee = dto.TotalFee,
    };

    public static FavoriteProductFormViewModel ToFormViewModel(this LikeListItemDto dto) => new()
    {
        Sn = dto.Sn,
        ProductName = dto.ProductName,
        Price = dto.Price,
        FeeRate = dto.FeeRate,
        Qty = dto.Qty,
    };

    public static FavoriteProductDeleteViewModel ToDeleteViewModel(this LikeListItemDto dto) => new()
    {
        Sn = dto.Sn,
        ProductName = dto.ProductName,
        Price = dto.Price,
        FeeRate = dto.FeeRate,
        Qty = dto.Qty,
        TotalAmount = dto.TotalAmount,
        TotalFee = dto.TotalFee,
        Account = dto.Account,
    };

    public static FavoriteProductInputDto ToInputDto(this FavoriteProductFormViewModel vm) => new()
    {
        ProductName = vm.ProductName,
        Price = vm.Price,
        FeeRate = vm.FeeRate,
        Qty = vm.Qty,
    };
}
