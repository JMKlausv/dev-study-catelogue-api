using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.Frameworks.Commands.CreateFramework
{
    public record CreateFrameworkCommand : IRequest<int>
    {
       public string Name { get; init; }
       public string Type { get; init; }    
       public int LanguageId { get; init; }    
    }
    public class CreateFrameworkCommandHandler : IRequestHandler<CreateFrameworkCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreateFrameworkCommandHandler(IAppDbContext context)
        {
           _context = context;
        }
        public async Task<int> Handle(CreateFrameworkCommand request, CancellationToken cancellationToken)
        {
            var framework = new Framework()
            {
                Name = request.Name,
                Type = request.Type,
                LanguageId = request.LanguageId,
            };
            _context.Frameworks.Add(framework);
            framework.AddDomainEvent(new FrameworkCreatedEvent(framework));
           await  _context.SaveChangesAsync(cancellationToken);
            return framework.Id;
        }
    }
}
