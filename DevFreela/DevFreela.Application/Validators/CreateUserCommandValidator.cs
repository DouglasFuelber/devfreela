using DevFreela.Application.Commands.CreateUser;
using FluentValidation;
using System.Text.RegularExpressions;

namespace DevFreela.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(u => u.Email).EmailAddress()
                                 .WithMessage("Not valid email!");

            RuleFor(u => u.Password).Must(ValidPassword)
                                    .WithMessage("Passwword must have at least eight characters, at least one uppercase letter, one lowercase letter, one number and one special character");

            RuleFor(u => u.Fullname).NotNull()
                                    .NotEmpty()
                                    .WithMessage("Name is required!");
        }

        public bool ValidPassword(string password)
        {
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");

            return regex.IsMatch(password);
        }
    }
}
