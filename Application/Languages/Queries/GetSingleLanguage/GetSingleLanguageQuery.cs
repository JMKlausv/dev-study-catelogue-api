

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Languages.Queries.GetAllLanguages;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;

namespace Application.Languages.Queries.GetSingleLanguage
{
    public record GetSingleLanguageQuery :IRequest<LanguageDto>
    {
        public int Id { get; set; } 
    }
    public class GetSingleLanguageQueryHandler : IRequestHandler<GetSingleLanguageQuery, LanguageDto>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetSingleLanguageQueryHandler(IAppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<LanguageDto> Handle(GetSingleLanguageQuery request, CancellationToken cancellationToken)
        {
            var language = await _context.Languages.FindAsync(request.Id);
            var languageDto = _mapper.Map<LanguageDto>(language);
            if(language == null)
            {
                throw new NotFoundException("language",new {Id=request.Id});
            }
            return languageDto;
        }
    }
}
