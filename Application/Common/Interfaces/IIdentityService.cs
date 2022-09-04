using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<(Result result, string tokenString)> AuthenticateUserAsync(string userName, string email, string password);
        Task<string> GetUserNameAsync(string userId);
        Task<(Result result, string userId)> CreateUserAsync(string userName, string email, string password);
    }
}
