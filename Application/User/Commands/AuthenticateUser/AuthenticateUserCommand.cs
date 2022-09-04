using Application.common.Exceptions;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;


namespace Application.User.Commands.AuthenticateUser
{
    public  class AuthenticateUserCommand : IRequest<string>
    {
        public string? email { get; init; }
        public string? userName { get; init; }   
        public string password { get; init; }

}
public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, string>
{
    private readonly IIdentityService _identityService;

    public AuthenticateUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<string> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {

        var response = await _identityService.AuthenticateUserAsync(request.userName,request.email, request.password);
        if (!response.result.Succeeded)
        {

            throw new AuthenticationException(string.Join(",", response.result.Errors));
        }
        return response.tokenString;



    }
}
}
