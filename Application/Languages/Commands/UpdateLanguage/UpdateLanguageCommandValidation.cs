using Application.Common.Interfaces;
using Application.Frameworks.Commands.UpdateFramework;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Languages.Commands.UpdateLanguage
{
    public  class UpdateLanguageCommandValidation :  AbstractValidator<UpdateLanguageCommand>
    {
        private readonly IAppDbContext _context;

    public UpdateLanguageCommandValidation(IAppDbContext context)
    {
        _context = context;

        RuleFor(l => l)
            .Must(HaveUniqueName)
            .WithMessage("name must be unique");
        RuleFor(l => l.Name)
                .NotNull();


    }
        private bool HaveUniqueName(UpdateLanguageCommand editedLanguage)
        {
            return _context.Languages
                .All(language => language.Id.Equals(editedLanguage.Id)
                || language.Name != editedLanguage.Name);
        }
    }
}
