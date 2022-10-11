using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Courses.Commands.PatchCourse
{
    public record PatchCourseCommand : IRequest<int>
    {
        public int Id { get; init; }
        public JsonPatchDocument<Course> coursePatch { get; init; }
    }
    public class PatchCourseCommandHandler : IRequestHandler<PatchCourseCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<PatchCourseCommandHandler> _logger;

        public PatchCourseCommandHandler(IAppDbContext context , ILogger<PatchCourseCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<int> Handle(PatchCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses.FindAsync(request.Id);
            if(course == null)
            {
                throw new NotFoundException("Course", new { Id = request.Id });
            }
            try
            {
                request.coursePatch.ApplyTo(course);
              request.coursePatch.ApplyTo(course );
                _logger.LogCritical(course.UpvoteCount.ToString());

              await _context.SaveChangesAsync(cancellationToken); 
                return request.Id;  
            }
            catch (Exception ex)
            {

                throw new Exception("could not perform Patch : "+ ex.Message);
            }
        }
    }
}
