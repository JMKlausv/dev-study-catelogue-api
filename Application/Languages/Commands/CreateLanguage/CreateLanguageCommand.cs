using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommand: IRequest<int>
    {
        public string Name { get; init; }
    }
    public record CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreateLanguageCommandHandler(IAppDbContext context)
        {
           _context = context;
        }
        public async Task<int> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = new Language
            {
                Name = request.Name
            };
            language.AddDomainEvent(new LanguageCreatedEvent(language));
            _context.Languages.Add(language); 
            
           await  _context.SaveChangesAsync(cancellationToken);
            return language.Id;
        }
    }
}
