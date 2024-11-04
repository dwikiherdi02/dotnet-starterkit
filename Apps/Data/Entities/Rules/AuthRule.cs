using FluentValidation;

namespace Apps.Data.Entities.Rules
{
    public class AuthEntityLoginBodyRule : AbstractValidator<AuthEntityLoginBody>
    {
        public AuthEntityLoginBodyRule()
        {
            RuleFor(b => b.Email).NotEmpty();
            // RuleFor(b => b.Email).EmailAddress();

            RuleFor(b => b.Password).NotEmpty();
        }
    }
}