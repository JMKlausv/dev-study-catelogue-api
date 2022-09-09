using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Courses.Queries.GetAllCourses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Courses.Queries.GetCourseById
{
    public record GetCourseByIdQuery : IRequest<CourseDto>
    {
       public int Id { get; set; }
    }
    public class GetCourseByIdQueryHand : IRequestHandler<GetCourseByIdQuery, CourseDto>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetCourseByIdQueryHand(IAppDbContext context , IMapper mapper)
        {
            _context = context;
           _mapper = mapper;
        }
        public async Task<CourseDto> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            CourseDto course = await _context.Courses
                 .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
                 .FirstAsync(c => c.Id == request.Id);
            if (course == null)
            {
                throw new NotFoundException("Course", new { id = request.Id });
            }
            return course;
        }
    }
}
