using Domain;
using FluentValidation;

namespace Application.TableFields
{
    public class TableFieldValidator : AbstractValidator<TableField>
    {
        public TableFieldValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}