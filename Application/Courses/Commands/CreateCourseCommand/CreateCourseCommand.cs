
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Courses.Commands.CreateCourseCommand
{
    public record CreateCourseCommand : IRequest<int>
    {
        public string Title { get; init; }
        public string AuthorName { get; init; }
        public string Description { get; init; }
        public string? ImageUrl { get; init; }
        public string ContentLink { get; init; }
        public DateTime PublishedDate { get; init; }
        public int FrameworkId { get; init; }
        public string Difficulty { get; init; }
        public string PlatformType { get; init; }
        public int UploadedBy { get; init; }
        public string Division { get; init; }

    }
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreateCourseCommandHandler(IAppDbContext context)
        {
           _context = context;
        }
        public async Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = new Course
            {
                Title = request.Title,
                AuthorName = request.AuthorName,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                ContentLink = request.ContentLink,  
                PublishedDate = request.PublishedDate,
                Difficulty = request.Difficulty,
                PlatformType = request.PlatformType,
                UploadedBy = request.UploadedBy,    
                Division = request.Division,     
            };
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync(cancellationToken);
            return course.Id;
           
        }
    }
}
