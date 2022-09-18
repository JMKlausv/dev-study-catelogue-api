using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Courses.Queries.GetUserCourseCount
{
    public  class GetUserCourseCountQuery : IRequest<int>
    {
    }
    public class GetUserCourseCountQueryHandler : IRequestHandler<GetUserCourseCountQuery, int>
    {
        private readonly IAppDbContext _context;
        private readonly IIdentityService _identityService;

        public GetUserCourseCountQueryHandler(IAppDbContext context , IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }
        public async Task<int> Handle(GetUserCourseCountQuery request, CancellationToken cancellationToken)
        {
           // var adminId = await _identityService.GetAdminId();
            return _context.Courses
                .Where(c => c.Division == "user division").ToList()    
                .Count();
           
        }
    }
}
