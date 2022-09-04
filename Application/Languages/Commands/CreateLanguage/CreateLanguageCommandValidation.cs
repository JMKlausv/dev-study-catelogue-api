
using Application.Common.Interfaces;
using FluentValidation;


namespace Application.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommandValidation : AbstractValidator<CreateLanguageCommand>
    {
        private readonly IAppDbContext _context;

        public CreateLanguageCommandValidation(IAppDbContext context)
        {
            _context = context;

            RuleFor(l => l.Name)
                .NotEmpty().Must(beUnique).WithMessage("name must be unique");
           
        }
       private bool beUnique(string name)
        {
            return !(_context.Languages.Any(l => l.Name == name));
        }
    }
}
