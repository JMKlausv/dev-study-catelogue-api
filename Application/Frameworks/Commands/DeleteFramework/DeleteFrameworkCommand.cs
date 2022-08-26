using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Frameworks.Commands.DeleteFramework
{
    public  record DeleteFrameworkCommand : IRequest<int>
    {
        public int Id { get; init; }
    }
    public class DeleteFrameworkCommandHandler : IRequestHandler<DeleteFrameworkCommand, int>
    {
        private readonly IAppDbContext _context;

        public DeleteFrameworkCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(DeleteFrameworkCommand request, CancellationToken cancellationToken)
        {
            var framework = await _context.Frameworks.FindAsync(request.Id);   
            if(framework == null)
            {
                throw new NotFoundException("framework", new{ Id= request.Id});
            }
            _context.Frameworks.Remove(framework);
            framework.AddDomainEvent(new FrameworkDeletedEvent(request.Id));
           await  _context.SaveChangesAsync(cancellationToken);
           return request.Id;   
        }
    }
}
