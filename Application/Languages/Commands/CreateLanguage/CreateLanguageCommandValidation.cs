
using FluentValidation;


namespace Application.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommandValidation : AbstractValidator<CreateLanguageCommand>
    {
        public CreateLanguageCommandValidation()
        {
            RuleFor(l => l.Name)
                .NotEmpty();
        }
    }
}
