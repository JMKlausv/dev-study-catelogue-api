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
        public int? MaxCount { get; set; }
        public string? UploadedByUser { get; set; }
        public string? UserDivision { get; set; }

    }
    public class GetCourseByDifficultyQueryHandler : IRequestHandler<GetCourseByDifficultyQuery, IEnumerable<CourseDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public GetCourseByDifficultyQueryHandler(IAppDbContext context , IMapper mapper , IIdentityService identityService)
        {
            _context = context;
            _mapper = mapper;
           _identityService = identityService;
        }
        public async  Task<IEnumerable<CourseDto>> Handle(GetCourseByDifficultyQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<CourseDto> course;
            var adminId = await _identityService.GetAdminId();

            if (request.UploadedByUser == "true")
            {
                if(request.UserDivision == "true")
                {
                    course = _context.Courses
                         .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
                         .Where(c => c.Difficulty == request.Difficulty && c.UploadedBy != adminId && c.Division == "user division")
                         .OrderByDescending(c => c.UpvoteCount - c.DownvoteCount);
                }
                else
                {
                    course = _context.Courses
                         .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
                         .Where(c => c.Difficulty == request.Difficulty && c.UploadedBy != adminId)
                         .OrderByDescending(c => c.UpvoteCount - c.DownvoteCount);
                }

            }
        else if (request.UploadedByUser =="false")
            {

                course = _context.Courses
                     .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
                     .Where(c => c.Difficulty == request.Difficulty && c.UploadedBy ==adminId)
                     .OrderByDescending(c => c.UpvoteCount - c.DownvoteCount);
            }
            else
            {
                if (request.UserDivision == "true")
                {
                    course = _context.Courses
                     .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
                     .Where(c => c.Difficulty == request.Difficulty && c.Division == "user division")
                     .OrderByDescending(c => c.UpvoteCount - c.DownvoteCount);
                }
                else
                {
                    course = _context.Courses
                     .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
                     .Where(c => c.Difficulty == request.Difficulty)
                     .OrderByDescending(c => c.UpvoteCount - c.DownvoteCount);
                }

            }



            var course1 = request.MaxCount != null 
                ? course.Take((int)request.MaxCount)
                : course;


            return course1;
        }
    }

}
