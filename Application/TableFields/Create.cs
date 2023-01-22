using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.TableFields
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public TableField TableField { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.TableField).SetValidator(new TableFieldValidator());
                }
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                

                request.TableField.TableName = await _context.TableNames
                    .Where(x => x.Id == request.TableField.TableName.Id ).FirstOrDefaultAsync();

                _context.TableFields.Add(request.TableField);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create TableField");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}