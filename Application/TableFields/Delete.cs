using Application.Core;
using MediatR;
using Persistence;

namespace Application.TableFields
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var tableField = await _context.TableFields.FindAsync(request.Id);

                if (tableField == null) return null;

                _context.Remove(tableField);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete the TableField");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}