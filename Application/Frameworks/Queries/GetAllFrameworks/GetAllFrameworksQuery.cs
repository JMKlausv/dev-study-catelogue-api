using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
 

namespace Application.Frameworks.Queries.GetAllFrameworks
{
    public record GetAllFrameworksQuery :IRequest<IEnumerable<FrameworkDto>>
    {
    }
    public class GetAllFrameworksQueryHandler : IRequestHandler<GetAllFrameworksQuery, IEnumerable<FrameworkDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetAllFrameworksQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<FrameworkDto>> Handle(GetAllFrameworksQuery request, CancellationToken cancellationToken)
        {
            return  _context.Frameworks
                .Include(f=>f.language)
                .ProjectTo<FrameworkDto>(_mapper.ConfigurationProvider)
                .ToList();
        }
    }

}
