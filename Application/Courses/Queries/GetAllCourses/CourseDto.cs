using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Courses.Queries.GetAllCourses
{
    public class CourseDto : IMapFrom<Course>
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string ContentLink { get; set; }
        public DateTime PublishedDate { get; set; }
        public int FrameworkId { get; set; }
        public string Difficulty { get; set; }
        public string PlatformType { get; set; }
        public int UpvoteCount { get; set; } = 0;
        public int DownvoteCount { get; set; } = 0;
        public string UploadedBy { get; set; }
        public string Division { get; set; }
    }
}
