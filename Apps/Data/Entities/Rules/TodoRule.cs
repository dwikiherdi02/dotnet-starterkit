using FluentValidation;

namespace Apps.Data.Entities.Rules
{
    public class TodoEntityQueryRule : AbstractValidator<TodoEntityQuery>
    {
        public TodoEntityQueryRule()
        {
            // RuleFor(q => q.Page)
            //     .GreaterThanOrEqualTo(1);
            // RuleFor(q => q.Search).Matches(@"^[a-z\d\-_\s]+$");
            RuleFor(q => q.Search)
                .Matches(@"^(?!.*[\t\r\n])(?!(?:.*--[^\r\n]*)|(?:.*\/\*[\w\W]*?\*\/)).*$");
            
        }
    }
}