

using FluentValidation;

namespace Application.User.Commands.AuthenticateUser
{
    public class AuthenticateUserCommandValidation : AbstractValidator<AuthenticateUserCommand> 
    {
    
        public AuthenticateUserCommandValidation()
        {
            RuleFor(u => u.email).EmailAddress();
            RuleFor(u => u.password).NotEmpty();
         
         
        }
    

    }
}
