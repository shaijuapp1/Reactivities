using Domain;
using FluentValidation;

namespace Application.AppConfigTypes
{
    public class AppConfigTypeValidator : AbstractValidator<AppConfigType>
    {
        public AppConfigTypeValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}