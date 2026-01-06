using CP.Shared.DTOs;
using System.Threading.Tasks;

namespace CP.Server.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> LoginAsync(LoginRequest request);
        Task<AuthResponse?> RegisterAsync(RegisterRequest request);
    }
}
