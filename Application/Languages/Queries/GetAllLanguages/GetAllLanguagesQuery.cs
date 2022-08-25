using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;


namespace Application.Languages.Queries.GetAllLanguages
{
    public record GetAllLanguagesQuery : IRequest<IEnumerable<LanguageDto>>;

    public class GetAllLanguagesQueryHandler : IRequestHandler<GetAllLanguagesQuery, IEnumerable<LanguageDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetAllLanguagesQueryHandler(IAppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public  async Task<IEnumerable<LanguageDto>> Handle(GetAllLanguagesQuery request, CancellationToken cancellationToken)
        {
            return  _context.Languages
                .ProjectTo<LanguageDto>(_mapper.ConfigurationProvider)
                .ToList();
        }
    }
}
