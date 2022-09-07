using Application.Common.Interfaces;
using Domain.Entities;
using FluentValidation;


namespace Application.Frameworks.Commands.UpdateFramework
{
    public class UpdateFrameworkCommandValidation : AbstractValidator<UpdateFrameworkCommand>
    {
        private readonly IAppDbContext _context;

        public UpdateFrameworkCommandValidation(IAppDbContext context)
        {
            _context = context;

            RuleFor(f => f)
                .Must(HaveUniqueName)
                .WithErrorCode("400")
                .WithMessage("framework with the name exists, please enter unique framework name");
            RuleFor(f => f.Name).NotNull();
            RuleFor(f => f.LanguageId)
                .NotEmpty()
                .Must(BeValidLanguageId)
                .WithErrorCode("400")
                .WithMessage("language with the inputed id is not found");
            RuleFor(f => f.Type).NotEmpty();

        }

        private bool BeValidLanguageId(int id)
        {
            return _context.Languages.Any(l => l.Id == id);
        }
        private bool HaveUniqueName(UpdateFrameworkCommand editedFramework)
        {
            return _context.Frameworks
                .All(framework => framework.Id.Equals(editedFramework.Id) 
                || framework.Name != editedFramework.Name);
        }
    }
}
