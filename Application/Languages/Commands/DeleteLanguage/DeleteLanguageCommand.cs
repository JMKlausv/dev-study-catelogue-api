using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Events;
using MediatR;

namespace Application.Languages.Commands.DeleteLanguage
{
    public record DeleteLanguageCommand : IRequest<int>
    {
        public int Id { get; init; } 
    }
    public class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand, int>
    {
        private readonly IAppDbContext _context;

        public DeleteLanguageCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = await _context.Languages.FindAsync(request.Id);
            if(language == null)
            {
                throw new NotFoundException("Language", new { Id = request.Id });
            }
             _context.Languages.Remove(language);
            language.AddDomainEvent(new LanguageDeletedEvent(request.Id));
            await _context.SaveChangesAsync(cancellationToken);
            return request.Id;  
        }
    }
}
