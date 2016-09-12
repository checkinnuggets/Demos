using FluentValidation;
using ShortlistManager.Web.Models;

namespace ShortlistManager.Web.Infrastructure.Validation
{
    public class LoginModelValidator : AbstractValidator<LogInModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}