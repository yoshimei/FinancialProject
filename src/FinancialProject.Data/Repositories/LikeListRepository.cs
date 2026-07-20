using System.Data;
using FinancialProject.Common.Dtos;
using FinancialProject.Common.Exceptions;
using FinancialProject.Common.Interfaces.Repositories;
using FinancialProject.Data.Infrastructure;
using Microsoft.Data.SqlClient;

namespace FinancialProject.Data.Repositories;

public sealed class LikeListRepository : ILikeListRepository
{
    /// <summary>對應 usp_LikeList_Create 內 THROW 51000（使用者不存在）。</summary>
    private const int UserNotFoundErrorNumber = 51000;

    /// <summary>對應 usp_LikeList_Update/Delete 內 THROW 51001（查無資料或無擁有權）。</summary>
    private const int OwnershipErrorNumber = 51001;

    private readonly ISqlConnectionFactory _connectionFactory;

    public LikeListRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IReadOnlyList<LikeListItemDto>> GetListByUserAsync(string userId, CancellationToken ct = default)
    {
        await using var conn = _connectionFactory.CreateConnection();
        await conn.OpenAsync(ct);

        await using var cmd = new SqlCommand(SpNames.LikeListGetListByUser, conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = userId;

        var items = new List<LikeListItemDto>();
        await using var reader = await cmd.ExecuteReaderAsync(ct);
        while (await reader.ReadAsync(ct))
        {
            items.Add(MapItem(reader));
        }

        return items;
    }

    public async Task<LikeListItemDto?> GetByIdAsync(int sn, string userId, CancellationToken ct = default)
    {
        await using var conn = _connectionFactory.CreateConnection();
        await conn.OpenAsync(ct);

        await using var cmd = new SqlCommand(SpNames.LikeListGetById, conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.Add("@SN", SqlDbType.Int).Value = sn;
        cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = userId;

        await using var reader = await cmd.ExecuteReaderAsync(ct);
        return await reader.ReadAsync(ct) ? MapItem(reader) : null;
    }

    public async Task<int> CreateAsync(string userId, FavoriteProductInputDto input, decimal totalAmount, decimal totalFee, CancellationToken ct = default)
    {
        await using var conn = _connectionFactory.CreateConnection();
        await conn.OpenAsync(ct);

        await using var cmd = new SqlCommand(SpNames.LikeListCreate, conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = userId;
        AddInputParameters(cmd, input, totalAmount, totalFee);

        try
        {
            await using var reader = await cmd.ExecuteReaderAsync(ct);
            await reader.ReadAsync(ct);
            return reader.GetInt32(reader.GetOrdinal("SN"));
        }
        catch (SqlException ex) when (ex.Number == UserNotFoundErrorNumber)
        {
            throw new NotFoundException(ex.Message);
        }
    }

    public async Task UpdateAsync(int sn, string userId, FavoriteProductInputDto input, decimal totalAmount, decimal totalFee, CancellationToken ct = default)
    {
        await using var conn = _connectionFactory.CreateConnection();
        await conn.OpenAsync(ct);

        await using var cmd = new SqlCommand(SpNames.LikeListUpdate, conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.Add("@SN", SqlDbType.Int).Value = sn;
        cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = userId;
        AddInputParameters(cmd, input, totalAmount, totalFee);

        try
        {
            await cmd.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex) when (ex.Number == OwnershipErrorNumber)
        {
            throw new NotFoundException(ex.Message);
        }
    }

    public async Task DeleteAsync(int sn, string userId, CancellationToken ct = default)
    {
        await using var conn = _connectionFactory.CreateConnection();
        await conn.OpenAsync(ct);

        await using var cmd = new SqlCommand(SpNames.LikeListDelete, conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.Add("@SN", SqlDbType.Int).Value = sn;
        cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = userId;

        try
        {
            await cmd.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex) when (ex.Number == OwnershipErrorNumber)
        {
            throw new NotFoundException(ex.Message);
        }
    }

    private static void AddInputParameters(SqlCommand cmd, FavoriteProductInputDto input, decimal totalAmount, decimal totalFee)
    {
        cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar, 100).Value = input.ProductName;
        cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = input.Price;
        cmd.Parameters.Add("@FeeRate", SqlDbType.Decimal).Value = input.FeeRate;
        cmd.Parameters.Add("@Qty", SqlDbType.Int).Value = input.Qty;
        cmd.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = totalAmount;
        cmd.Parameters.Add("@TotalFee", SqlDbType.Decimal).Value = totalFee;
    }

    private static LikeListItemDto MapItem(SqlDataReader reader) => new()
    {
        Sn = reader.GetInt32(reader.GetOrdinal("SN")),
        ProductNo = reader.GetInt32(reader.GetOrdinal("ProductNo")),
        ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
        Price = reader.GetDecimal(reader.GetOrdinal("Price")),
        FeeRate = reader.GetDecimal(reader.GetOrdinal("FeeRate")),
        Qty = reader.GetInt32(reader.GetOrdinal("Qty")),
        TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
        TotalFee = reader.GetDecimal(reader.GetOrdinal("TotalFee")),
        Account = reader.GetString(reader.GetOrdinal("Account")),
        Email = reader.GetString(reader.GetOrdinal("Email")),
    };
}
