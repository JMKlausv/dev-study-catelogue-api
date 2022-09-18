

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Courses.Queries.GetAllCourses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Courses.Queries.GetCourseByFrameworkId
{
    public record GetCourseByFrameworkIdQuery : IRequest<IEnumerable<CourseDto>>
    {
        public int FrameworkId { get; set; }   
        public int? MaxCount { get; set; }
        public string? UploadedByUser { get; set; }
        public string? userDivision { get; set; }
    }
    public class GetCourseByFrameworkIdQueryHandler : IRequestHandler<GetCourseByFrameworkIdQuery, IEnumerable<CourseDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public GetCourseByFrameworkIdQueryHandler(IAppDbContext context , IMapper mapper , IIdentityService identityService)
        {
            _context = context;
            _mapper = mapper;
            _identityService = identityService;
        }
        public async Task<IEnumerable<CourseDto>> Handle(GetCourseByFrameworkIdQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<CourseDto> course;
            var adminId = await _identityService.GetAdminId();

            System.Diagnostics.Debug.WriteLine(request.UploadedByUser);
                /// user only courses//
            if (request.UploadedByUser == "true")
            {
                if(request.userDivision == "true")
                {
                    course = _context.Courses
                              .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
                              .Where(c => c.Framework.Id == request.FrameworkId && c.UploadedBy != adminId && c.Division == "user division")
                              .OrderByDescending(c => c.UpvoteCount - c.DownvoteCount);
                }
                else
                {
                    course = _context.Courses
                              .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
                              .Where(c => c.Framework.Id == request.FrameworkId && c.UploadedBy != adminId)
                              .OrderByDescending(c => c.UpvoteCount - c.DownvoteCount);

                }

            }
            /// admin only courses ///
            else if(request.UploadedByUser == "false")
                {
                course = _context.Courses
                    .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
                    .Where(c => c.Framework.Id == request.FrameworkId && c.UploadedBy == adminId)
                    .OrderByDescending(c => c.UpvoteCount - c.DownvoteCount);
                       
                }   
             /// all courses///
                else 
                {
                if (request.userDivision == "true")
                {
                    course = _context.Courses
                        .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
                        .Where(c => c.Framework.Id == request.FrameworkId && c.Division == "user division")
                        .OrderByDescending(c => c.UpvoteCount - c.DownvoteCount);
                }
                else
                {
                    course = _context.Courses
                        .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
                        .Where(c => c.Framework.Id == request.FrameworkId)
                        .OrderByDescending(c => c.UpvoteCount - c.DownvoteCount);
                }

            }
           

               var course2 = request.MaxCount !=null
                ?course.Take((int)request.MaxCount)
                :course;
            

           
            return course2;
        }
    }
}
