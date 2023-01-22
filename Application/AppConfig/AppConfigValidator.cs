using Domain;
using FluentValidation;

namespace Application.AppConfigs
{
    public class AppConfigValidator : AbstractValidator<AppConfig>
    {
        public AppConfigValidator()
        {
            RuleFor(x => x.ConfigType.Id).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Det1).NotEmpty();
        }
    }
}