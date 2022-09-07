using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Frameworks.Queries.GetAllFrameworks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Frameworks.Queries.GetFrameworksByLanguageId
{
    public record GetFrameworksByLanguageIdQuery : IRequest<IEnumerable<FrameworkDto>>
    {
        public int LanguageId { get; init; }
    }
    public class GetFrameworksByLanguageIdQueryHandler : IRequestHandler<GetFrameworksByLanguageIdQuery, IEnumerable<FrameworkDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetFrameworksByLanguageIdQueryHandler(IAppDbContext context, IMapper mapper)
        {
             _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<FrameworkDto>> Handle(GetFrameworksByLanguageIdQuery request, CancellationToken cancellationToken)
        {
            if(!(_context.Frameworks.Any(f =>f.LanguageId == request.LanguageId)))
            {
                throw new NotFoundException("language", new { Id = request.LanguageId });
            }
            return  _context.Frameworks
                .ProjectTo<FrameworkDto>(_mapper.ConfigurationProvider)
                .Where(f => f.Language.Id== request.LanguageId
          );
        }
    }
}
