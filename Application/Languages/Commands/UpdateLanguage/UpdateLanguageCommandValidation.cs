using Application.Common.Interfaces;
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

        RuleFor(l => l.Name)
            .NotEmpty().Must(beUnique).WithMessage("name must be unique");

    }
    private bool beUnique(string name)
    {
        return !(_context.Languages.Any(l => l.Name == name));
    }
}
}
