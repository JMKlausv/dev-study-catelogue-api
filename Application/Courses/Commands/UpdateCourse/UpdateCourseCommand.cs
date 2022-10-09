using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using MediatR;


namespace Application.Courses.Commands.UpdateCourse
{
    public record UpdateCourseCommand : IRequest<int> ,  IMapFrom<Course>
    {
        public int Id { get; set; }
        public string Title { get; init; }
        public string AuthorName { get; init; }
        public string Description { get; init; }
        public string? ImageUrl { get; init; }
        public string ContentLink { get; init; }
        public DateTime PublishedDate { get; init; }
        public int FrameworkId { get; init; }
        public string Difficulty { get; init; }
        public string PlatformType { get; init; }
        public string UploadedBy { get; init; }
        public string Division { get; init; }
        public int? UpvoteCount { get; init; }
        public int? DownVoteCount {  get; init; }
    }
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCourseCommandHandler(IAppDbContext context , IMapper mapper)
        {
           _context = context;
          _mapper = mapper;
        }
        public async Task<int> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
           if(!_context.Courses.Any(c=>c.Id == request.Id))
            {
                throw new NotFoundException("Course", new { id = request.Id });
            }
            var course = _mapper.Map<Course>(request);
            _context.Courses.Update(course);
            await _context.SaveChangesAsync(cancellationToken);
            return course.Id;   

        }
    }
}
