using Application.Core;
using MediatR;
using Persistence;

namespace Application.TableNames
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
                var tableName = await _context.TableNames.FindAsync(request.Id);

                if (tableName == null) return null;

                _context.Remove(tableName);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete the TableName");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}