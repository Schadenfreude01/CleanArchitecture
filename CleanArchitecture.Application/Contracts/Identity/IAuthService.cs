using CleanArchitecture.Application.Models.Identity;

namespace CleanArchitecture.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> LogIn(AuthRequest request);
        Task<RegistrationResponse> Register(RegistrationRequest request);
    }
}
