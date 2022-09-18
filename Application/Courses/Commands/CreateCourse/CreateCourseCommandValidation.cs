using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandValidation : AbstractValidator<CreateCourseCommand> 
    {
        public CreateCourseCommandValidation()
        {
            RuleFor(c => c.Title)
                .NotNull();
            RuleFor(c => c.Difficulty)
               .NotNull();
            RuleFor(c => c.ContentLink)
               .NotNull();
            RuleFor(c => c.PlatformType)
               .NotNull();
            RuleFor(c => c.UploadedBy)
               .NotNull();
            RuleFor(c => c.Division)
               .NotNull();
            RuleFor(c => c.FrameworkId)
               .NotNull();
        }
    }
}
