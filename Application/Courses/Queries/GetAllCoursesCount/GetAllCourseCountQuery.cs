using Application.Common.Interfaces;
using MediatR;

namespace Application.Courses.Queries.GetAllCoursesCount
{
    public class GetAllCourseCountQuery: IRequest<int>
    {

    }
    public class GetAllCourseCountQueryHandler : IRequestHandler<GetAllCourseCountQuery, int>
    {
        private readonly IAppDbContext _context;

        public GetAllCourseCountQueryHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(GetAllCourseCountQuery request, CancellationToken cancellationToken)
        {
            return   _context.Courses.Count();

        }
    }
}
