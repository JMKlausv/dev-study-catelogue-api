using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Courses.Commands.DeleteCourse
{
    public record DeleteCourseCommand : IRequest<int>
    {
        public int Id { get; set; } 
    }
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, int>
    {
        private readonly IAppDbContext _context;

        public DeleteCourseCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses.FindAsync(request.Id);
            if(course == null)
            {
                throw new NotFoundException("course", new { Id = request.Id });
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync(cancellationToken);
            return course.Id;   

        }
    }
}
