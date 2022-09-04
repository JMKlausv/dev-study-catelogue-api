using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Languages.Commands.UpdateLanguage
{
    public  record UpdateLanguageCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; init; }   
    }
    public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, int>
    {
        private readonly IAppDbContext _context;

        public UpdateLanguageCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
           if(!_context.Languages.Any(l=>l.Id == request.Id))
            {
                throw new NotFoundException("language", new { Id = request.Id });
            }
            var language = new Language()
            {
                Name = request.Name,    
                Id = request.Id,
            };
            _context.Languages.Update(language);
            await _context.SaveChangesAsync(cancellationToken);
            return language.Id;
        }
    }
}
