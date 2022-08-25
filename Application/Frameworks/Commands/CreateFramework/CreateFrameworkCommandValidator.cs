using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Frameworks.Commands.CreateFramework
{
    public  class CreateFrameworkCommandValidator : AbstractValidator<CreateFrameworkCommand>   
    {
        private readonly IAppDbContext _context;

        public CreateFrameworkCommandValidator( IAppDbContext context)
        {
            _context = context;

            RuleFor(f => f.Name).NotEmpty();
            RuleFor(f => f.LanguageId)
                .NotEmpty()
                .Must(BeValidLanguageId).WithMessage("language with the inputed id is not found");
           
            RuleFor(f => f.Type).NotEmpty();
            
        }

        private bool  BeValidLanguageId(int id)
        {
            return _context.Languages.Any(l => l.Id == id);
        }
    }
}
