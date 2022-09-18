using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Frameworks.Commands.UpdateFramework
{
    public record UpdateFrameworkCommand: IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public string Type { get; init; }
        public int LanguageId { get; init;}
    }
    public class UpdateFrameworkCommandHandler : IRequestHandler<UpdateFrameworkCommand, int>
    {
        private readonly IAppDbContext _context;

        public UpdateFrameworkCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(UpdateFrameworkCommand request, CancellationToken cancellationToken)
        {
           if(!_context.Frameworks.Any(f=>f.Id == request.Id))
            {
                throw new NotFoundException("Framework", new { id = request.Id });
            }
            var updatedFramework = new Framework()
            {
                Id = request.Id,    
                Name = request.Name,
                Type = request.Type,
                LanguageId = request.LanguageId,
            };
             _context.Frameworks.Update(updatedFramework);
             await _context.SaveChangesAsync(cancellationToken);   
            return updatedFramework.Id;
        }
    }
}
