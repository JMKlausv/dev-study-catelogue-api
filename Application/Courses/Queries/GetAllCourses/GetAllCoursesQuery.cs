using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Courses.Queries.GetAllCourses
{
    public record GetAllCoursesQuery : IRequest<IEnumerable<CourseDto>>
    {
        public int? MaxCount { get; set; }
        public string? UploadedByUser { get; set; }
        public string? UserDivision { get; set; }
        public string? OrderBy { get; set; }
    }
    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, IEnumerable<CourseDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public GetAllCoursesQueryHandler(IAppDbContext context , IMapper mapper , IIdentityService identityService)
        {
            _context = context;
            _mapper = mapper;
            _identityService = identityService;
        }
        public async Task<IEnumerable<CourseDto>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var adminId = await _identityService.GetAdminId();
            IEnumerable<CourseDto> course;
            System.Diagnostics.Debug.WriteLine(request.UploadedByUser);
            if (request.UploadedByUser == "true")
            {
                if(request.UserDivision == "true")
                {
                    course = await _context.Courses.Include(c => c.Framework)
                                       .Where(c => c.UploadedBy != adminId && c.Division == "user division")
                                       .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
                                       .ToListAsync();
                }
                else
                {
                    course = await _context.Courses.Include(c => c.Framework)
                                      .Where(c => c.UploadedBy != adminId)
                                      .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync();
                }
              

            }
            else if (request.UploadedByUser == "false")
            {
                course = await _context.Courses.Include(c => c.Framework)
                   .Where(c => c.UploadedBy == adminId)
                   .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
                   .ToListAsync();
            }
            else
            {
                if (request.UserDivision == "true")
                {
                    course = await _context.Courses.Include(c => c.Framework)
                     .Where(c => c.Division == "user division")
                    .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                }
                else
                {
                    course = await _context.Courses.Include(c => c.Framework)
                        .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
                        .ToListAsync();
                }

            }
            if(request.OrderBy != null)
            {
                course.reorder(request.OrderBy);
            }
           var course1 = request.MaxCount!=null? course.Take((int)request.MaxCount):course;
            return course1;


        }

       
    }
    
}
