namespace FinancialProject.Common.Dtos;

public sealed class UserDto
{
    public required string UserID { get; init; }
    public required string UserName { get; init; }
    public required string Email { get; init; }
    public required string Account { get; init; }
}
