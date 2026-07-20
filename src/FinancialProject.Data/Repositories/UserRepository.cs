using System.Data;
using FinancialProject.Common.Dtos;
using FinancialProject.Common.Interfaces.Repositories;
using FinancialProject.Data.Infrastructure;
using Microsoft.Data.SqlClient;

namespace FinancialProject.Data.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public UserRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public Task<UserDto?> ValidateLoginAsync(string userId, CancellationToken ct = default) =>
        FindByIdAsync(SpNames.UserValidateLogin, userId, ct);

    public Task<UserDto?> GetProfileAsync(string userId, CancellationToken ct = default) =>
        FindByIdAsync(SpNames.UserGetProfile, userId, ct);

    private async Task<UserDto?> FindByIdAsync(string spName, string userId, CancellationToken ct)
    {
        await using var conn = _connectionFactory.CreateConnection();
        await conn.OpenAsync(ct);

        await using var cmd = new SqlCommand(spName, conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = userId;

        await using var reader = await cmd.ExecuteReaderAsync(ct);
        if (!await reader.ReadAsync(ct))
        {
            return null;
        }

        return new UserDto
        {
            UserID = reader.GetString(reader.GetOrdinal("UserID")),
            UserName = reader.GetString(reader.GetOrdinal("UserName")),
            Email = reader.GetString(reader.GetOrdinal("Email")),
            Account = reader.GetString(reader.GetOrdinal("Account")),
        };
    }
}
