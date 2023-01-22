using Domain;
using FluentValidation;

namespace Application.TableNames
{
    public class TableNameValidator : AbstractValidator<TableName>
    {
        public TableNameValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}