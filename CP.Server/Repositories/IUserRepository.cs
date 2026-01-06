using System.Threading.Tasks;
using CP.Shared.Entities;

namespace CP.Server.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> CreateUserAsync(User user);
        Task<bool> UserExistsAsync(string email);
    }
}
