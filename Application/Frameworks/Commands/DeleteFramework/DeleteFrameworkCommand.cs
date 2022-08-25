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
        public Task<int> Handle(DeleteFrameworkCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
