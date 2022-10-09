using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
      
        //Task<(Result result, string tokenString, int[] likedCourses, int[] dislikedCourses)> login(string userName, string email, string password);
       Task<LoginResponse> AuthenticateUserAsync(string userName, string email, string password);
        Task<string> GetUserNameAsync(string userId);
        Task<string> GetAdminId();
        Task<int> updateUserDislikes(string userId, int[] dislikedCourses);
        Task<int> updateUserLikes(string userId, int[] likedCourses);    
        Task<(Result result, string userId)> CreateUserAsync(string userName, string email, string password);
    }
}
