

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Courses.Queries.GetAllCourses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
namespace Application.Courses.Queries.GetCourseByFrameworkId
{
    public record GetCourseByFrameworkIdQuery : IRequest<IEnumerable<CourseDto>>
    {
        public int FrameworkId { get; set; }    
    }
    public class GetCourseByFrameworkIdQueryHandler : IRequestHandler<GetCourseByFrameworkIdQuery, IEnumerable<CourseDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetCourseByFrameworkIdQueryHandler(IAppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CourseDto>> Handle(GetCourseByFrameworkIdQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<CourseDto> course =  _context.Courses
                  .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
                  .Where(c => c.FrameworkId == request.FrameworkId);
            if (course == null)
            {
                throw new NotFoundException("Course", new { frameworkId = request.FrameworkId });
            }
            return course;
        }
    }
}
