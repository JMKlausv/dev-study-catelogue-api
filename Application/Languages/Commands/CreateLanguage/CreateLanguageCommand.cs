using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;

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
          using(IDbContextTransaction transaction = _context.database.BeginTransaction())
            {
                try
                {
                    var language = new Language
                    {
                        Name = request.Name
                    };
                    language.AddDomainEvent(new LanguageCreatedEvent(language));
                    _context.Languages.Add(language);

                    await _context.SaveChangesAsync(cancellationToken);
                    var pureFrameWork = new Framework
                    {
                        Name = "pure " + language.Name,
                        Type = "pure",
                        LanguageId = language.Id,
                    };
                    _context.Frameworks.Add(pureFrameWork);
                    await _context.SaveChangesAsync(cancellationToken);

                    transaction.Commit();

                    return language.Id;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }


            }
        }
    }
}
