using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries.GetUserPreferences
{
    public record GetUserPreferencesQuery : IRequest<UserPreference>
    {
        public string UserId { get; set; }  
    }
    public class GetUserPreferencesQueryHandler : IRequestHandler<GetUserPreferencesQuery, UserPreference>
    {
        private readonly IIdentityService _identityService;

        public GetUserPreferencesQueryHandler(IIdentityService identity)
        {
            _identityService = identity;
        }
        public async Task<UserPreference> Handle(GetUserPreferencesQuery request, CancellationToken cancellationToken)
        {
            return await _identityService.GetUserPreferences(request.UserId); 
        }
    }
}
