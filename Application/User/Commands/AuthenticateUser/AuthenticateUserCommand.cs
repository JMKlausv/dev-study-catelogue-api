using Application.common.Exceptions;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;


namespace Application.User.Commands.AuthenticateUser
{
    public  class AuthenticateUserCommand : IRequest<Object>
    {
        public string? email { get; init; }
        public string? userName { get; init; }   
        public string password { get; init; }

}
public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand,Object >
{
    private readonly IIdentityService _identityService;

    public AuthenticateUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Object> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {

        var response = await _identityService.AuthenticateUserAsync(request.userName,request.email, request.password);
      
            if (!response.Result.Succeeded)
        {

            throw new AuthenticationException(string.Join(",", response.Result.Errors));
        }
     
        return new
        {
            tokenString=response.TokenString,
            likedCourses = response.LikedCoursses,
            dislikedCourses =response.DislikedCourses
        };



    }
}
}
