using DevFreela.Application.Commands.CreateProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
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

            RuleFor(p => p.IdClient).NotNull()
                                    .WithMessage("Client ID is required!");

            RuleFor(p => p.IdFreelancer).NotNull()
                                        .WithMessage("Freelancer ID is required!");
        }
    }
}
