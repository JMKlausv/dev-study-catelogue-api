using FluentValidation;

namespace Application.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandValidation : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidation()
        {
            RuleFor(c => c.Title)
               .NotNull();
            RuleFor(c => c.AuthorName)
               .NotNull();
            RuleFor(c => c.Difficulty)
               .NotNull();
            RuleFor(c => c.PublishedDate)
               .NotNull();
            RuleFor(c => c.ContentLink)
               .NotNull();
            RuleFor(c => c.Description)
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
