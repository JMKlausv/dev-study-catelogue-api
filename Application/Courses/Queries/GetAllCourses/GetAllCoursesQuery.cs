using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Courses.Queries.GetAllCourses
{
    public record GetAllCoursesQuery : IRequest<IEnumerable<CourseDto>>
    {
    }
    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, IEnumerable<CourseDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCoursesQueryHandler(IAppDbContext context , IMapper mapper)
        {
           _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CourseDto>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Courses.ProjectTo<CourseDto>(_mapper.ConfigurationProvider).ToListAsync();
            
        }
    }
}
