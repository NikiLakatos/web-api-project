using System.Threading.Tasks;
using Repos.Modals;

namespace Repos.Context
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string userName, string password);
        Task<bool> UserExists(string userName);

    }
}