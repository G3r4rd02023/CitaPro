using CP.Shared.DTOs;
using System.Threading.Tasks;

namespace CP.Client.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> Login(LoginRequest loginRequest);
        Task<AuthResponse?> Register(RegisterRequest registerRequest);
        Task Logout();
    }
}
