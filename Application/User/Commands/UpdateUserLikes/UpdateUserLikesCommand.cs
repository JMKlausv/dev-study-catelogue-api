using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.UpdateUserLikes
{
    public record  UpdateUserLikesCommand : IRequest<string>
    {
      public  string UserId { get; set; }
      public  int[]? LikedCourses { get; set; }
      public int[]? DislikedCourses { get; set; }
    }
    public class UpdateUserLikesCommandHandler : IRequestHandler<UpdateUserLikesCommand, string>
    {
        private readonly IIdentityService _identityService;

        public UpdateUserLikesCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<string> Handle(UpdateUserLikesCommand request, CancellationToken cancellationToken)
        {
            int result = 0;
            if(request.LikedCourses != null)
            {
                 result = await _identityService.updateUserLikes(request.UserId, request.LikedCourses);
            }
            if(request.DislikedCourses != null)
            {
                result = await _identityService.updateUserDislikes(request.UserId, request.DislikedCourses);
            }
           
            if (result < 0)
            {
                throw new NotFoundException($"user with id {request.UserId} not found");
            }
            else if(result == 0)
            {
                throw new ValidationException("Invalid Input: both liked and disliked courses cannot be null ");
            }
            
            return request.UserId;
        }
    }
}
