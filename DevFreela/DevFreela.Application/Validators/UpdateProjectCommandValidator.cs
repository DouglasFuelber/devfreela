using DevFreela.Application.Commands.UpdateProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(p => p.Title).NotEmpty()
                                 .NotNull()
                                 .WithMessage("Title is required!");

            RuleFor(p => p.Title).MaximumLength(30)
                                 .WithMessage("Title must have 30 characters max!");

            RuleFor(p => p.Description).NotEmpty()
                                       .NotNull()
                                       .WithMessage("Description is required!");

            RuleFor(p => p.Description).MaximumLength(255)
                                       .WithMessage("Description must have 255 characters max!");
        }
    }
}
