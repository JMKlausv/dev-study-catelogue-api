using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Text;


namespace Infrastructure.Identity
{



    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<IdentityService> _logger;

        public IdentityService(UserManager<ApplicationUser> userManager, IConfiguration configuration , ILogger<IdentityService> logger)
        {
            _userManager = userManager;
            _configuration = configuration;
           _logger = logger;
        }
        public async Task<LoginResponse> AuthenticateUserAsync(string userName, string email, string password)
        {
            ApplicationUser user;
            if (userName != null && !userName.Equals(""))
            {
                user = await _userManager.FindByNameAsync(userName);
            }
            else
            {
                user = await _userManager.FindByEmailAsync(email);
            }

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {

                var tokenString = await generateToken(user);
                var response = new LoginResponse()
                {
                    Result = Result.Success(),
                    TokenString = tokenString,
                    LikedCoursses = user.LikedCoursesId,
                    DislikedCourses = user.DislikedCoursesId,
                };
                _logger.LogCritical(response.TokenString);

                return response;

                //  return tokenString; 

            }
            string[] errors = new string[] { "Invalid login" };
            var response2 = new LoginResponse()
            {
                Result = Result.Success(),
                TokenString = String.Empty,
                LikedCoursses = String.Empty,
                DislikedCourses = String.Empty,
            };
            return response2;
        }

            public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<int> updateUserLikes(string userId , int[] likedCourses)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null)
            {
                return -1;
               
            }
            user.LikedCourses = likedCourses;
            await _userManager.UpdateAsync(user);
            return 1;
        }
        public async Task<int> updateUserDislikes(string userId, int[] dislikedCourses)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return -1;
            }
            user.DislikedCourses = dislikedCourses;
            await _userManager.UpdateAsync(user);
            return 1;    
        }

        public async Task<(Result result, string userId)> CreateUserAsync(string userName,string email, string password)
        {
            var existingEmail = await _userManager.FindByEmailAsync(email) ;
           var  existingUserName = await _userManager.FindByNameAsync(userName);
            if (existingUserName != null)
            {

                return (Result.Failure(new string[] { "User Name already in use" }), string.Empty);
            }
            if(existingEmail != null)
            {
                return (Result.Failure(new string[] { "Email already in use" }), string.Empty);
            }
            int[] arr = { 8};
            var user = new ApplicationUser
            {
                Email = email,
                UserName = userName,
                LikedCourses = arr,
                DislikedCourses = Array.Empty<int>()
            };

            var result = await _userManager.CreateAsync(user, password);
            _logger.LogCritical("this is result........"+result.ToString());
            if (!result.Succeeded)
            {
                return (Result.Failure(new string[] { result.ToString() }), string.Empty);
            }
           var addRoleResult =  await _userManager.AddToRoleAsync(user, "user");
            _logger.LogWarning(addRoleResult.ToString());

        
            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<string> GetAdminId()
        {
            var admins = await _userManager.GetUsersInRoleAsync("admin");
            return admins.First().Id;
        }

        public async Task<UserPreference> GetUserPreferences(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                throw new NotFoundException("User", new { Id = userId });
            }
            return new UserPreference { LikedCourses = user.LikedCourses, DislikedCourses = user.DislikedCourses, };
        }


        private async Task<string> generateToken(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {

               new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
               new Claim(ClaimTypes.Email, user.Email),
               new Claim(ClaimTypes.Name,user.UserName), 
           };
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));

            }
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                    algorithm: SecurityAlgorithms.HmacSha256)
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }

    }
}