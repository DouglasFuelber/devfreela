using DevFreela.Application.Commands.CreateComment;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(p => p.Content).NotEmpty()
                                   .NotNull()
                                   .WithMessage("Content is required!");

            RuleFor(p => p.IdProject).NotNull()
                                     .WithMessage("Project ID is required!");

            RuleFor(p => p.IdUser).NotNull()
                                  .WithMessage("User ID is required!");
        }
    }
}
