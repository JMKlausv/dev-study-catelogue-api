using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Courses.Queries.GetAllCourses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Courses.Queries.GetCourseByDifficulty
{
    public record GetCourseByDifficultyQuery : IRequest<IEnumerable<CourseDto>>
    {
        public string Difficulty { get; set; }  
    }
    public class GetCourseByDifficultyQueryHandler : IRequestHandler<GetCourseByDifficultyQuery, IEnumerable<CourseDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetCourseByDifficultyQueryHandler(IAppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async  Task<IEnumerable<CourseDto>> Handle(GetCourseByDifficultyQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<CourseDto> course = _context.Courses
             .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
             .Where(c => c.Difficulty == request.Difficulty);
            if (course == null)
            {
                throw new NotFoundException("Course", new { Difficulty  = request.Difficulty });
            }
            return course;
        }
    }

}
