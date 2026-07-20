using FinancialProject.Common.Dtos;
using FinancialProject.Common.Interfaces.Repositories;
using FinancialProject.Common.Interfaces.Services;

namespace FinancialProject.Business.Services;

public sealed class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<UserDto?> ValidateLoginAsync(string userId, CancellationToken ct = default) =>
        _userRepository.ValidateLoginAsync(userId, ct);

    public Task<UserDto?> GetProfileAsync(string userId, CancellationToken ct = default) =>
        _userRepository.GetProfileAsync(userId, ct);
}
