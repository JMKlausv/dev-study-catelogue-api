using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Frameworks.Commands.CreateFramework
{
    public  class CreateFrameworkCommandValidation : AbstractValidator<CreateFrameworkCommand>   
    {
        private readonly IAppDbContext _context;

        public CreateFrameworkCommandValidation( IAppDbContext context)
        {
            _context = context;

            RuleFor(f => f.Name)
                .NotEmpty()
                .Must(BeUniqueName)
                .WithErrorCode("400")
                .WithMessage("framework with the requestes name exists, please enter unique framework name");
            RuleFor(f => f.LanguageId)
                .NotEmpty()
                .Must(BeValidLanguageId)
                .WithErrorCode("400")
                .WithMessage("language with the inputed id is not found");
           
            RuleFor(f => f.Type).NotEmpty();
            
        }

        private bool  BeValidLanguageId(int id)
        {
            return _context.Languages.Any(l => l.Id == id);
        }
        private  bool BeUniqueName(string name)
        {
            return !_context.Frameworks.Any(f=> f.Name == name); 
        }
    }
}
