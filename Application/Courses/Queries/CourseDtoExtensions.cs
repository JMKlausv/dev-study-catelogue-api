using Application.Courses.Queries.GetAllCourses;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Courses.Queries
{
    public static  class CourseDtoExtensions
    {
        public static IEnumerable<CourseDto> reorder(this IEnumerable<CourseDto> courses , string orderBy)
        {
            switch (orderBy)
            {
                case "avgVote":
                    return courses.OrderByDescending(c => c.UpvoteCount - c.DownvoteCount);
                case "upvoteCount":
                case "UpvoteCount":
                    return courses.OrderByDescending(c => c.UpvoteCount);
                case "downvoteCount":
                case "DownvoteCount":
                    return courses.OrderByDescending(c => c.DownvoteCount);
                case "publishedDate":
                    return courses.OrderByDescending(c => c.PublishedDate);
                case "UploadedBy":
                case "uploadedBy":
                    return courses.OrderByDescending(c => c.UploadedBy);
                case "authorName":
                case "AuthorName":
                    return courses.OrderByDescending(c => c.AuthorName);
                case "Title":
                case "title":
                    return courses.OrderByDescending(c => c.Title);
                default:
                    return courses;

            }
        }
    }
}
