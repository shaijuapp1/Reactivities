using Domain;
using MediatR;
using Persistence;

namespace Application.ActionTasks
{
    public class Create
    {
        public class Command : IRequest
        {
            public ActionTask item { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.ActionTasks.Add(request.item);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}