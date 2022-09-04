using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.CreateUser
{
    public class CreateUserCommandValidation: AbstractValidator<CreateUserCommand>  
    {
        public CreateUserCommandValidation()
        {
          RuleFor(u=>u.email)
                .NotEmpty()
                .EmailAddress();
          RuleFor(u => u.password)
                .NotEmpty();
         RuleFor(u=>u.userName).NotEmpty();
            
        }
    }
}
