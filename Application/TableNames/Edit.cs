using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.TableNames
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public TableName TableName { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.TableName).SetValidator(new TableNameValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var tableName = await _context.TableNames.FindAsync(request.TableName.Id);
               
                if (tableName == null) return null;

                _mapper.Map(request.TableName, tableName);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update TableName = ");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}